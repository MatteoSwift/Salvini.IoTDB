using Thrift;

namespace Salvini.IoTDB.Data;

internal class RowRecord
{
    public long Timestamp { get; private set; }
    public List<dynamic> Values { get; private set; }
    public List<string> Measurements { get; private set; }

    public RowRecord(DateTime timestamp, List<dynamic> values, List<string> measurements)
    {
        var utc = new DateTime(1970, 01, 01).Add(TimeZoneInfo.Local.BaseUtcOffset);
        Init((long)(timestamp - utc).TotalMilliseconds, values, measurements);
    }
    
    public RowRecord(long timestamp, List<dynamic> values, List<string> measurements)
    {
        Init(timestamp, values, measurements);
    }

    private void Init(long timestamp, List<dynamic> values, List<string> measurements)
    {
        if (values.Count != measurements.Count)
        {
            throw new Exception($"Input error. Values.Count({values.Count}) does not equal to Measurements.Count({measurements.Count}).");
        }
        Timestamp = timestamp;
        if (values.Any(x => x == null))
        {
            Values = values.Where(x => x != null).ToList();
            Measurements = values.Select((_, i) => _ == null ? string.Empty : measurements[i]).Where(x => !string.IsNullOrEmpty(x)).ToList();
        }
        else
        {
            Values = values;
            Measurements = measurements;
        }
    }
    public DateTime GetDateTime()
    {
        return DateTimeOffset.FromUnixTimeMilliseconds(Timestamp).DateTime.ToLocalTime();
    }

    public override string ToString()
    {
        //new DateTime(1970, 01, 01).Add(TimeZoneInfo.Local.BaseUtcOffset).AddMilliseconds(Timestamps)
        return $"TimeStamp,{string.Join(',', Measurements)}{System.Environment.NewLine}{GetDateTime()},{string.Join(',', Values)}";
    }

    public List<int> GetDataTypes()
    {
        var dataTypeValues = new List<int>();

        foreach (var valueType in Values.Select(value => value))
        {
            switch (valueType)
            {
                case bool _:
                    dataTypeValues.Add((int)DataType.BOOLEAN);
                    break;
                case int _:
                    dataTypeValues.Add((int)DataType.INT32);
                    break;
                case long _:
                    dataTypeValues.Add((int)DataType.INT64);
                    break;
                case float _:
                    dataTypeValues.Add((int)DataType.FLOAT);
                    break;
                case double _:
                    dataTypeValues.Add((int)DataType.DOUBLE);
                    break;
                case string _:
                    dataTypeValues.Add((int)DataType.TEXT);
                    break;
            }
        }

        return dataTypeValues;
    }

    public byte[] ToBytes()
    {
        var buffer = new ByteBuffer(Values.Count * 8);

        foreach (var value in Values)
        {
            switch (value)
            {
                case bool b:
                    buffer.AddByte((byte)DataType.BOOLEAN);
                    buffer.AddBool(b);
                    break;
                case int i:
                    buffer.AddByte((byte)DataType.INT32);
                    buffer.AddInt(i);
                    break;
                case long l:
                    buffer.AddByte((byte)DataType.INT64);
                    buffer.AddLong(l);
                    break;
                case double d:
                    buffer.AddByte((byte)DataType.DOUBLE);
                    buffer.AddDouble(d);
                    break;
                case float f:
                    buffer.AddByte((byte)DataType.FLOAT);
                    buffer.AddFloat(f);
                    break;
                case string s:
                    buffer.AddByte((byte)DataType.TEXT);
                    buffer.AddStr(s);
                    break;
                default:
                    throw new TException($"Unsupported data type:{value.GetType()}", null);
            }
        }

        return buffer.GetBuffer(); ;
    }
}
