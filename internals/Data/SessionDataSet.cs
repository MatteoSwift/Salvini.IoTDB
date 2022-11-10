using Thrift;

namespace Salvini.IoTDB.Data;

internal class SessionDataSet : System.IDisposable
{
    private readonly long _queryId;
    private readonly string _sql;
    private readonly List<string> _columnNames;
    private readonly Dictionary<string, int> _columnNameIndexMap;
    private readonly Dictionary<int, int> _duplicateLocation;
    private readonly List<string> _columnTypeLst;
    private TSQueryDataSet _queryDataset;
    private readonly byte[] _currentBitmap;
    private readonly int _columnSize;
    private List<ByteBuffer> _valueBufferLst, _bitmapBufferLst;
    private ByteBuffer _timeBuffer;
    private readonly ClientPool _clientQueue;
    private int _rowIndex;
    private bool _hasCatchedResult;
    private RowRecord _cachedRowRecord;
    private readonly bool _isClosed = false;

    private string TimestampStr => "Time";
    private int StartIndex => 2;
    private int Flag => 0x80;
    private int DefaultTimeout => 10000;

    public int FetchSize { get; set; }

    public SessionDataSet(string sql, TSExecuteStatementResp resp, ClientPool clientQueue)
    {
        _clientQueue = clientQueue;
        _sql = sql;
        _queryDataset = resp.QueryDataSet;
        _queryId = resp.QueryId;
        _columnSize = resp.Columns.Count;
        _currentBitmap = new byte[_columnSize];
        _columnNames = new List<string>();
        _timeBuffer = new ByteBuffer(_queryDataset.Time);
        _columnNameIndexMap = new Dictionary<string, int>();
        _columnTypeLst = new List<string>();
        _duplicateLocation = new Dictionary<int, int>();
        _valueBufferLst = new List<ByteBuffer>();
        _bitmapBufferLst = new List<ByteBuffer>();
        // some internal variable
        _hasCatchedResult = false;
        _rowIndex = 0;
        if (resp.ColumnNameIndexMap != null)
        {
            for (var index = 0; index < resp.Columns.Count; index++)
            {
                _columnNames.Add("");
                _columnTypeLst.Add("");
            }

            for (var index = 0; index < resp.Columns.Count; index++)
            {
                var name = resp.Columns[index];
                _columnNames[resp.ColumnNameIndexMap[name]] = name;
                _columnTypeLst[resp.ColumnNameIndexMap[name]] = resp.DataTypeList[index];
            }
        }
        else
        {
            _columnNames = resp.Columns;
            _columnTypeLst = resp.DataTypeList;
        }

        for (int index = 0; index < _columnNames.Count; index++)
        {
            var columnName = _columnNames[index];
            if (_columnNameIndexMap.ContainsKey(columnName))
            {
                _duplicateLocation[index] = _columnNameIndexMap[columnName];
            }
            else
            {
                _columnNameIndexMap[columnName] = index;
            }

            _valueBufferLst.Add(new ByteBuffer(_queryDataset.ValueList[index]));
            _bitmapBufferLst.Add(new ByteBuffer(_queryDataset.BitmapList[index]));
        }
    }

    public bool HasNext()
    {
        if (_hasCatchedResult)
        {
            return true;
        }

        // we have consumed all current data, fetch some more
        if (!_timeBuffer.HasRemaining())
        {
            if (!FetchResults())
            {
                return false;
            }
        }

        ConstructOneRow();
        _hasCatchedResult = true;
        return true;
    }

    public RowRecord Next()
    {
        if (!_hasCatchedResult)
        {
            if (!HasNext())
            {
                return null;
            }
        }

        _hasCatchedResult = false;
        return _cachedRowRecord;
    }

    public dynamic[,] ReadAsMatrix()
    {
        var data = new List<dynamic>();
        var title = new List<string>();
        var y = _columnNames.Count + 1;
        var x = _timeBuffer.Length / 8 + 1;
        var matrix = new dynamic[x, y];
        matrix[0, 0] = "Timestamp";
        for (var j = 1; j < y; j++)
        {
            matrix[0, j] = _columnNames[j - 1];
        }
        var i = 1;
        while (HasNext())
        {
            var row = Next();
            matrix[i, 0] = row.GetDateTime();
            for (int j = 0; j < row.Values.Count; j++)
            {
                matrix[i, j + 1] = row.Values[j];
            }
            i++;
        }
        return matrix;
    }

    public System.Data.DataTable ReadAsTable()
    {
        var data = new System.Data.DataTable("TABLE");
        while (HasNext())
        {
            var row = Next();
            if (data.Columns.Count == 0)
            {
                data.Columns.Add("Timestamp", typeof(DateTime));
                for (var i = 0; i < row.Measurements.Count; i++)
                {
                    data.Columns.Add(row.Measurements[i], typeof(object));
                }
            }
            var item = data.NewRow();
            item[0] = row.GetDateTime();
            for (var i = 0; i < row.Measurements.Count; i++)
            {
                item[i + 1] = row.Values[i];
            }
            data.Rows.Add(item);
        }
        return data;
    }

    private DataType GetDataTypeFromStr(string str)
    {
        return str switch
        {
            "BOOLEAN" => DataType.BOOLEAN,
            "INT32" => DataType.INT32,
            "INT64" => DataType.INT64,
            "FLOAT" => DataType.FLOAT,
            "DOUBLE" => DataType.DOUBLE,
            "TEXT" => DataType.TEXT,
            "NULLTYPE" => DataType.NONE,
            _ => DataType.TEXT
        };
    }

    private void ConstructOneRow()
    {
        List<object> fieldLst = new List<object>();

        for (int i = 0; i < _columnSize; i++)
        {
            if (_duplicateLocation.ContainsKey(i))
            {
                var field = fieldLst[_duplicateLocation[i]];
                fieldLst.Add(field);
            }
            else
            {
                var columnValueBuffer = _valueBufferLst[i];
                var columnBitmapBuffer = _bitmapBufferLst[i];

                if (_rowIndex % 8 == 0)
                {
                    _currentBitmap[i] = columnBitmapBuffer.GetByte();
                }

                object localField;
                if (!IsNull(i, _rowIndex))
                {
                    var columnDataType = GetDataTypeFromStr(_columnTypeLst[i]);


                    switch (columnDataType)
                    {
                        case DataType.BOOLEAN:
                            localField = columnValueBuffer.GetBool();
                            break;
                        case DataType.INT32:
                            localField = columnValueBuffer.GetInt();
                            break;
                        case DataType.INT64:
                            localField = columnValueBuffer.GetLong();
                            break;
                        case DataType.FLOAT:
                            localField = columnValueBuffer.GetFloat();
                            break;
                        case DataType.DOUBLE:
                            localField = columnValueBuffer.GetDouble();
                            break;
                        case DataType.TEXT:
                            localField = columnValueBuffer.GetStr();
                            break;
                        default:
                            string err_msg = "value format not supported";
                            throw new TException(err_msg, null);
                    }

                    fieldLst.Add(localField);
                }
                else
                {
                    localField = null;
                    fieldLst.Add("NULL");
                }
            }
        }

        long timestamp = _timeBuffer.GetLong();
        _rowIndex += 1;
        _cachedRowRecord = new RowRecord(timestamp, fieldLst, _columnNames);
    }

    private bool IsNull(int loc, int row_index)
    {
        byte bitmap = _currentBitmap[loc];
        int shift = row_index % 8;
        return ((Flag >> shift) & bitmap) == 0;
    }

    private bool FetchResults()
    {
        _rowIndex = 0;
        var myClient = _clientQueue.Take();
        var req = new TSFetchResultsReq(myClient.SessionId, _sql, FetchSize, _queryId, true)
        {
            Timeout = DefaultTimeout
        };
        try
        {
            var task = myClient.ServiceClient.fetchResultsAsync(req);
            task.Wait();
            var resp = task.Result;

            if (resp.HasResultSet)
            {
                _queryDataset = resp.QueryDataSet;
                // reset buffer
                _timeBuffer = new ByteBuffer(resp.QueryDataSet.Time);
                _valueBufferLst = new List<ByteBuffer>();
                _bitmapBufferLst = new List<ByteBuffer>();
                for (int index = 0; index < _queryDataset.ValueList.Count; index++)
                {
                    _valueBufferLst.Add(new ByteBuffer(_queryDataset.ValueList[index]));
                    _bitmapBufferLst.Add(new ByteBuffer(_queryDataset.BitmapList[index]));
                }

                // reset row index
                _rowIndex = 0;
            }

            return resp.HasResultSet;
        }
        catch (TException e)
        {
            throw new TException("Cannot fetch result from server, because of network connection", e);
        }
        finally
        {
            _clientQueue.Add(myClient);
        }
    }

    private async Task Close()
    {
        if (!_isClosed)
        {
            var myClient = _clientQueue.Take();
            var req = new TSCloseOperationReq(myClient.SessionId)
            {
                QueryId = _queryId
            };

            try
            {
                await myClient.ServiceClient.closeOperationAsync(req);
            }
            catch (TException e)
            {
                throw new TException("Operation Handle Close Failed", e);
            }
            finally
            {
                _clientQueue.Add(myClient);
            }
        }
    }

    ~SessionDataSet()
    {
        this.Dispose();
    }

    public void Dispose()
    {
        this.Close().Wait();
    }
}
