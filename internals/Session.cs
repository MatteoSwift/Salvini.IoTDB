using System.Net.Sockets;
using Thrift;
using Thrift.Protocol;
using Thrift.Transport;
using Thrift.Transport.Client;
using Salvini.IoTDB.Data;

namespace Salvini.IoTDB;

 
internal class Session
{
    private static int SuccessCode => 200;
    private static readonly TSProtocolVersion ProtocolVersion = TSProtocolVersion.IOTDB_SERVICE_PROTOCOL_V3;
    private readonly string _username;
    private readonly string _password;
    private readonly bool _enableRpcCompression;
    private string _zoneId;
    private readonly string _host;
    private readonly int _port;
    private readonly int _fetchSize;
    private readonly int _poolSize;
    private bool _isClose = true;
    private readonly ClientPool _pool = new();

    public bool IsOpen => !_isClose;
    public int ThreadCount => _poolSize;

    private int VerifyStatus(TSStatus status, int successCode)
    {
        if (status?.__isset.subStatus == true)
        {
            if (status.SubStatus.Any(subStatus => VerifyStatus(subStatus, successCode) != 0))
            {
                return -1;
            }
            return 0;
        }
        if (status?.Code == successCode)
        {
            return 0;
        }
        return -1;
    }

    private async Task<Client> CreateClientAsync()
    {
        var tcpClient = new TcpClient(_host, _port);

        var transport = new TFramedTransport(new TSocketTransport(tcpClient, null));

        if (!transport.IsOpen)
        {
            await transport.OpenAsync(new CancellationToken());
        }

        var client = _enableRpcCompression ? new TSIService.Client(new TCompactProtocol(transport)) : new TSIService.Client(new TBinaryProtocol(transport));

        var openReq = new TSOpenSessionReq(ProtocolVersion, _zoneId) { Username = _username, Password = _password };

        try
        {
            var openResp = await client.openSessionAsync(openReq);

            if (openResp.ServerProtocolVersion != ProtocolVersion)
            {
                throw new TException($"Protocol Differ, Client version is {ProtocolVersion} but Server version is {openResp.ServerProtocolVersion}", null);
            }

            if (openResp.ServerProtocolVersion == 0)
            {
                throw new TException("Protocol not supported", null);
            }

            var sessionId = openResp.SessionId;
            var statementId = await client.requestStatementIdAsync(sessionId);
            _isClose = false;
            return new Client(client, sessionId, statementId, transport);

        }
        catch (Exception)
        {
            transport.Close();
            throw;
        }
    }

    /// <summary>
    /// 创建IoTDB连接会话
    /// </summary>
    public Session(string host = "127.0.0.1", int port = 6667, string username = "root", string password = "admin#123", int fetchSize = 3600, int? poolSize = null, bool enableRpcCompression = false)
    {
        _host = host;
        _port = port;
        _username = username;
        _password = password;
        _zoneId = TimeZoneInfo.Local.DisplayName.Split(' ')[0][1..].Replace(")", "");
        _fetchSize = Math.Max(1000, Math.Abs(fetchSize));
        _poolSize = Math.Max(poolSize ?? Environment.ProcessorCount, 4);
        _enableRpcCompression = enableRpcCompression;
    }

    public async Task OpenAsync(bool force = false)
    {
        if (force) await this.CloseAsync();

        if (_isClose)
        {
            for (var i = 0; i < _poolSize; i++) _pool.Add(await CreateClientAsync());
        }
    }

    public async Task CloseAsync()
    {
        if (_isClose) return;
        await _pool.Clear();
        _isClose = true;
    }
 
    public async Task<int> InsertRecordAsync(string deviceId, RowRecord record, bool isAligned = true)
    {
        var client = _pool.Take();
        var req = new TSInsertRecordReq(client.SessionId, deviceId, record.Measurements, record.ToBytes(), record.Timestamp) { IsAligned = isAligned };
        try
        {
            var status = await client.ServiceClient.insertRecordAsync(req);
            return VerifyStatus(status, SuccessCode);
        }
        catch (TException)
        {
            await Task.Delay(100);
            await OpenAsync();
            client = _pool.Take();
            req.SessionId = client.SessionId;
            try
            {
                var status = await client.ServiceClient.insertRecordAsync(req);
                return VerifyStatus(status, SuccessCode);
            }
            catch (TException ex)
            {
                throw new TException("Error occurs when inserting record", ex);
            }
        }
        finally
        {
            _pool.Add(client);
        }
    }

    public async Task<int> InsertTabletAsync(Tablet tablet, bool isAligned = true)
    {
        var client = _pool.Take();
        var req = new TSInsertTabletReq(client.SessionId, tablet.DeviceId, tablet.Measurements, tablet.GetBinaryValues(), tablet.GetBinaryTimestamps(), tablet.GetDataTypes(), tablet.RowNumber) { IsAligned = isAligned };
        try
        {
            var status = await client.ServiceClient.insertTabletAsync(req);
            return VerifyStatus(status, SuccessCode);
        }
        catch (TException)
        {
            await Task.Delay(100);
            await OpenAsync();
            client = _pool.Take();
            req.SessionId = client.SessionId;
            try
            {
                var status = await client.ServiceClient.insertTabletAsync(req);
                return VerifyStatus(status, SuccessCode);
            }
            catch (TException ex)
            {
                throw new TException("Error occurs when inserting tablet", ex);
            }
        }
        finally
        {
            _pool.Add(client);
        }
    }

    public async Task<int> InsertRecordsOfOneDeviceAsync(string deviceId, List<RowRecord> rowRecords, bool isAligned = true)
    {
        var client = _pool.Take();
        var req = new TSInsertRecordsOfOneDeviceReq(client.SessionId, deviceId, rowRecords.Select(x => x.Measurements).ToList(), rowRecords.Select(x => x.ToBytes()).ToList(), rowRecords.Select(x => x.Timestamp).ToList()) { IsAligned = isAligned };
        try
        {
            var status = await client.ServiceClient.insertRecordsOfOneDeviceAsync(req);
            return VerifyStatus(status, SuccessCode);
        }
        catch (TException)
        {
            await Task.Delay(100);
            await OpenAsync();
            client = _pool.Take();
            req.SessionId = client.SessionId;
            try
            {
                var status = await client.ServiceClient.insertRecordsOfOneDeviceAsync(req);
                return VerifyStatus(status, SuccessCode);
            }
            catch (TException ex)
            {
                throw new TException("Error occurs when inserting records of one device", ex);
            }
        }
        finally
        {
            _pool.Add(client);
        }
    }

    public async Task<SessionDataSet> ExecuteQueryStatementAsync(string sql)
    {
        TSExecuteStatementResp resp;
        TSStatus status;
        var client = _pool.Take();
        var req = new TSExecuteStatementReq(client.SessionId, sql, client.StatementId) { FetchSize = _fetchSize };
        try
        {
            resp = await client.ServiceClient.executeQueryStatementAsync(req);
            status = resp.Status;
        }
        catch (TException)
        {
            await Task.Delay(100);
            await OpenAsync();
            client = _pool.Take();
            req.SessionId = client.SessionId;
            req.StatementId = client.StatementId;
            try
            {
                resp = await client.ServiceClient.executeQueryStatementAsync(req);
                status = resp.Status;
            }
            catch (TException ex)
            {
                throw new TException("Error occurs when executing query statement", ex);
            }
        }
        finally
        {
            _pool.Add(client);
        }

        if (VerifyStatus(status, SuccessCode) == -1)
        {
            throw new TException($"execute query failed, {status.Message}", null);
        }
        var sessionDataset = new SessionDataSet(sql, resp, _pool) { FetchSize = _fetchSize };
        return sessionDataset;
    }

    public async Task<int> ExecuteNonQueryStatementAsync(string sql)
    {
        var client = _pool.Take();
        var req = new TSExecuteStatementReq(client.SessionId, sql, client.StatementId);

        try
        {
            var resp = await client.ServiceClient.executeUpdateStatementAsync(req);
            var status = resp.Status;
            return VerifyStatus(status, SuccessCode);
        }
        catch (TException)
        {
            await Task.Delay(100);
            await OpenAsync();
            client = _pool.Take();
            req.SessionId = client.SessionId;
            req.StatementId = client.StatementId;
            try
            {
                var resp = await client.ServiceClient.executeUpdateStatementAsync(req);
                var status = resp.Status;
                return VerifyStatus(status, SuccessCode);
            }
            catch (TException ex)
            {
                throw new TException("Error occurs when executing non-query statement", ex);
            }
        }
        finally
        {
            _pool.Add(client);
        }
    }
}