using Thrift.Transport;

namespace Salvini.IoTDB;

/// <summary>
/// Apache/IoTDB C# 客户端
/// </summary>
internal class Client
{
    public TSIService.Client ServiceClient { get; }
    public long SessionId { get; }
    public long StatementId { get; }
    public TFramedTransport Transport { get; }

    public Client(TSIService.Client client, long sessionId, long statementId, TFramedTransport transport)
    {
        ServiceClient = client;
        SessionId = sessionId;
        StatementId = statementId;
        Transport = transport;
    }
}
