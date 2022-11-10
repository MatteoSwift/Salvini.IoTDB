using Thrift;

namespace Salvini.IoTDB.Data;

/*
* A tablet data of one device, the tablet contains multiple measurements of this device that share
* the same time column.
*
* for example:  device root.sg1.d1
*
* time, m1, m2, m3
*    1,  1,  2,  3
*    2,  1,  2,  3
*    3,  1,  2,  3
* 
* From 0.13 IoTDB Server, tablet could have NULL cell
*
*/
internal class Tablet
{
    private List<long> _timestamps;
    private List<List<dynamic>> _values;
    private List<DataType> _dataTypes;
    private BitMap[] _bitmaps;
    public string DeviceId { get; private set; }
    public List<string> Measurements { get; private set; }
    public int RowNumber => _timestamps.Count;
    public int ColNumber => Measurements.Count;

    public Tablet(string deviceId, List<string> measurements, List<List<dynamic>> values, List<DateTime> timestamps)
    {
        var utc = new DateTime(1970, 01, 01).Add(TimeZoneInfo.Local.BaseUtcOffset);
        Init(deviceId, measurements, values, timestamps.Select(x => (long)(x - utc).TotalMilliseconds).ToList());
    }

    public Tablet(string deviceId, List<string> measurements, List<List<dynamic>> values, List<long> timestamps)
    {
        Init(deviceId, measurements, values, timestamps);
    }

    private void Init(string deviceId, List<string> measurements, List<List<dynamic>> values, List<long> timestamps)
    {
        if (values.Count != timestamps.Count)
        {
            throw new Exception($"Input error. Timestamps.Count({timestamps.Count}) does not equal to Values.Count({values.Count}).", null);
        }

        if (values.Any(x => x.Count != measurements.Count))
        {
            throw new Exception($"Input error. Measurements.Count({measurements.Count}) does not equal to Values.Count({values.Select(x => x.Count).Max()}).", null);
        }

        var tuple = measurements.Select((x, i) => new { name = x, type = (DataType)GetDataType(timestamps.Select((_, j) => values[j][i]).FirstOrDefault(x => x != null)?.GetType() as Type) }).Where(x => x.type != DataType.NONE).ToList();

        DeviceId = deviceId;
        _timestamps = timestamps;
        _values = tuple.Count == measurements.Count ? values : timestamps.Select((_, j) => tuple.Select((_, i) => values[j][i]).ToList<dynamic>()).ToList();
        _dataTypes = tuple.Select(x => x.type).ToList();
        Measurements = tuple.Count == measurements.Count ? measurements : tuple.Select(x => x.name).ToList();

        if (_bitmaps != null)
        {
            foreach (var bitmap in _bitmaps)
            {
                if (bitmap != null)
                {
                    bitmap.reset();
                }
            }
        }
    }

    private DataType GetDataType(Type type)
    {
        var typeName = type?.Name;
        if (typeof(double).Name == typeName) return (DataType.DOUBLE);
        else if (typeof(float).Name == typeName) return (DataType.FLOAT);
        else if (typeof(int).Name == typeName) return (DataType.INT32);
        else if (typeof(long).Name == typeName) return (DataType.INT64);
        else if (typeof(bool).Name == typeName) return (DataType.BOOLEAN);
        else if (typeof(string).Name == typeName) return (DataType.TEXT);
        else return DataType.NONE;
    }

    public byte[] GetBinaryTimestamps()
    {
        var buffer = new ByteBuffer(new byte[] { });

        foreach (var timestamp in _timestamps)
        {
            buffer.AddLong(timestamp);
        }

        return buffer.GetBuffer();
    }

    public List<int> GetDataTypes() => _dataTypes.ConvertAll(x => (int)x);

    private int EstimateBufferSize()
    {
        var estimateSize = 0;

        foreach (var value in _dataTypes)
        {
            switch (value)
            {
                case DataType.BOOLEAN:
                    estimateSize += (_timestamps.Count * 1);
                    break;
                case DataType.INT32:
                    estimateSize += (_timestamps.Count * 4);
                    break;
                case DataType.INT64:
                    estimateSize += (_timestamps.Count * 8);
                    break;
                case DataType.FLOAT:
                    estimateSize += (_timestamps.Count * 4);
                    break;
                case DataType.DOUBLE:
                    estimateSize += (_timestamps.Count * 8);
                    break;
                case DataType.TEXT:
                    estimateSize += _timestamps.Select((_, j) => Measurements.Select((_, i) => ((string)_values[j][i])?.Length ?? 0).Sum()).Sum();
                    break;
                default:
                    estimateSize += (_timestamps.Count * 4);
                    break;
            }
        }
        return estimateSize;
    }

    public byte[] GetBinaryValues()
    {
        var estimateSize = EstimateBufferSize();
        var buffer = new ByteBuffer(estimateSize);

        for (var i = 0; i < ColNumber; i++)
        {
            var dataType = _dataTypes[i];

            for (var j = 0; j < RowNumber; j++)
            {
                var value = _values[j][i];
                if (value == null)
                {
                    if (_bitmaps == null)
                    {
                        _bitmaps = new BitMap[ColNumber];
                    }
                    if (_bitmaps[i] == null)
                    {
                        _bitmaps[i] = new BitMap(RowNumber);
                    }
                    _bitmaps[i].mark(j);
                }
            }

            switch (dataType)
            {
                case DataType.BOOLEAN:
                    {
                        for (int j = 0; j < RowNumber; j++)
                        {
                            var value = _values[j][i];
                            buffer.AddBool(value != null ? (bool)value : false);
                        }

                        break;
                    }
                case DataType.INT32:
                    {
                        for (int j = 0; j < RowNumber; j++)
                        {
                            var value = _values[j][i];
                            buffer.AddInt(value != null ? (int)value : int.MinValue);
                        }
                        break;
                    }
                case DataType.INT64:
                    {
                        for (int j = 0; j < RowNumber; j++)
                        {
                            var value = _values[j][i];
                            buffer.AddLong(value != null ? (long)value : long.MinValue);
                        }
                        break;
                    }
                case DataType.FLOAT:
                    {
                        for (int j = 0; j < RowNumber; j++)
                        {
                            var value = _values[j][i];
                            buffer.AddFloat(value != null ? (float)value : float.MinValue);
                        }
                        break;
                    }
                case DataType.DOUBLE:
                    {
                        for (int j = 0; j < RowNumber; j++)
                        {
                            var value = _values[j][i];
                            buffer.AddDouble(value != null ? (double)value : double.MinValue);
                        }
                        break;
                    }
                case DataType.TEXT:
                    {
                        for (int j = 0; j < RowNumber; j++)
                        {
                            var value = _values[j][i];
                            buffer.AddStr(value != null ? (string)value : string.Empty);
                        }
                        break;
                    }
                default:
                    throw new TException($"Unsupported data type {dataType}", null);

            }
        }
        if (_bitmaps != null)
        {
            foreach (var bitmap in _bitmaps)
            {
                bool columnHasNull = bitmap != null && !bitmap.isAllUnmarked();
                buffer.AddBool((bool)columnHasNull);
                if (columnHasNull)
                {
                    var bytes = bitmap.getByteArray();
                    for (int i = 0; i < RowNumber / 8 + 1; i++)
                    {
                        buffer.AddByte(bytes[i]);
                    }
                }
            }
        }

        return buffer.GetBuffer();
    }
}
