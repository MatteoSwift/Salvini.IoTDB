using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using Salvini.IoTDB.Data;
using static TSIService;

namespace Salvini.IoTDB;

/// <summary>
/// 时序数据库客户端，基于 Apache IoTDB 实现
/// </summary>
public sealed class TimeSeriesClient : IDisposable
{
    /// <summary>
    ///    原始值补齐，以确保在指定时刻有数据记录，根据阶梯/方波方式计算
    /// </summary>
    /// <param name="source">原数据集合</param>
    /// <param name="begin">开始时间</param>
    /// <param name="end">结束时间</param>
    /// <param name="interval">数据采样间隔,毫秒</param>
    public static List<(DateTime Time, double Value)> Fill(List<(DateTime Time, double Value)> source, DateTime begin, DateTime end, double interval = 1000)
    {
        var rows = new List<(DateTime Time, double Value)>();
        if (source.All(x => double.IsNaN(x.Value))) return rows;
        if (source.Count > 1)
        {

            var s0 = source[0];
            var t = begin;

            while (t < s0.Time)
            {
                rows.Add((t, s0.Value)); //向前拉直线
                t = t.AddMilliseconds(interval);
            }
            rows.Add((t, s0.Value));
            t = t.AddMilliseconds(interval);
            for (var i = 1; i < source.Count; i++)
            {
                while (t < source[i].Time)
                {
                    rows.Add((t, source[i - 1].Value));
                    t = t.AddMilliseconds(interval);
                }
            }
            while (t <= end)
            {
                rows.Add((t, source[^1].Value));
                t = t.AddMilliseconds(interval);
            }
        }
        else if (source.Count == 1)
        {
            var s0 = source[0];
            var t = begin;
            while (t < s0.Time)
            {
                rows.Add((t, s0.Value));
                t = t.AddMilliseconds(interval);
            }
            rows.Add((t, s0.Value));
            t = t.AddMilliseconds(interval);
            while (t <= end)
            {
                rows.Add((t, s0.Value));
                t = t.AddMilliseconds(interval);
            }
        }
        return rows;
    }

    private readonly static DateTime __UTC_TICKS__ = new DateTime(1970, 01, 01).Add(TimeZoneInfo.Local.BaseUtcOffset);
    private static long UTC_MS(DateTime time) => (long)(time - __UTC_TICKS__).TotalMilliseconds;

    /// <summary>
    /// 创建 TimeSeriesClient 实例
    /// </summary>
    /// <param name="url">连接字符串</param> 
    public static TimeSeriesClient CreateInstance(string url)
    {
        if (url.StartsWith("iotdb://")) return new TimeSeriesClient(url);
        throw new Exception($"not support connection url=>{url}");
    }

    private readonly Session session;
    private readonly string database;

    /// <summary>
    /// 连接字符串
    /// </summary>
    public string Url { get; }

    /// <summary>
    /// 是否打开连接
    /// </summary>
    public bool IsOpen => session.IsOpen;

    /// <summary>
    /// 数据库名称
    /// </summary>
    public string DatabaseName => database;

    /// <summary>
    /// 线程池数量
    /// </summary>
    public int ThreadCount => session.ThreadCount;

    /// <summary>
    /// 服务端版本
    /// </summary> 
    public Version Version { get; }

    /// <summary>
    /// 初始化 TimeSeriesClient
    /// </summary>
    /// <param name="url"></param>
    public TimeSeriesClient(string url)
    {
        this.Url = url;
        //url = iotdb://root:admin#123@127.0.0.1:6667/?database=kylin&appName=iTSDB&fetchSize=1800
        var match_host = new Regex(@"@((2(5[0-5]|[0-4]\d))|[0-1]?\d{1,2})(\.((2(5[0-5]|[0-4]\d))|[0-1]?\d{1,2})){3}:").Match(url);
        var match_port = new Regex(@":(\d{1,5})/?").Match(url);
        var match_user = new Regex(@"iotdb://(\w+):").Match(url);
        var match_pwd = new Regex(@":(\w+\S+){1}(@)").Match(url);
        var match_fetch = new Regex(@"fetchSize=(\d)+").Match(url);
        var match_pool = new Regex(@"poolSize=(\d)+").Match(url);
        var match_db = new Regex(@"database=(\w)+").Match(url);
        var host = match_host.Success ? match_host.Value[1..^1] : "127.0.0.1";
        var port = match_port.Success ? int.Parse(match_port.Value[1..].Replace("/", string.Empty)) : 6667;
        var username = match_user.Success ? match_user.Value[8..^1] : "root";
        var password = match_pwd.Success ? match_pwd.Value[1..^1] : "admin#123";
        var fetchSize = match_fetch.Success ? int.Parse(match_fetch.Value[10..]) : 1800;
        var poolSize = match_pool.Success ? int.Parse(match_pool.Value[9..]) : Environment.ProcessorCount;

        database = match_db.Success ? match_db.Value[9..] : "db";
        session = new Session(host, port, username, password, fetchSize, poolSize);
        session.OpenAsync().Wait(TimeSpan.FromSeconds(5));
        if (session.IsOpen) Version = new Version((string)this.session.ExecuteQueryStatementAsync("show version").Result.Next()?.Values[0]);

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"IoTDB>>Version:{Version};Open:{session.IsOpen};Host:{host};Port:{port};User:{username};Database:{database};ThreadCount:{session.ThreadCount}");
        Console.ResetColor();
    }

    private string BuildTagName(string tag) => (this.Version.Major == 0 && this.Version.Minor <= 12) || tag.All(x => x != '-') ? tag : $"`{tag}`";

    /// <summary>
    /// 获取测点快照数据
    /// </summary> 
    /// <param name="tags">测点集合</param>
    public async Task<List<(string Tag, DateTime Time, double Value)>> SnapshotAsync(List<string> tags)
    {
        var len = database.Length + 6;
        var sql = $"select last {string.Join(",", tags.Select(this.BuildTagName))} from root.{database}";
        using var query = await session.ExecuteQueryStatementAsync(sql);
        var data = new List<(string Tag, DateTime Time, double Value)>();
        while (query.HasNext())
        {
            var next = query.Next();
            var values = next.Values;
            var id = ((string)values[0])[len..];
            var time = next.GetDateTime();
            var value = values[1] == null ? double.NaN : double.Parse((string)values[1]);
            data.Add((id, time, value));
        }
        return data;
    }

    /// <summary>
    /// 获取测点归档数据,返回<time,value>
    /// </summary>
    /// <param name="device">所属设备或数据库</param>
    /// <param name="tag">测点</param>
    /// <param name="begin">开始时间</param>
    /// <param name="end">结束时间</param>
    /// <param name="digits">数据精度,默认6位小数</param> 
    public async Task<List<(DateTime Time, double Value)>> ArchiveAsync(string tag, DateTime begin, DateTime end, int digits = 6)
    {
        var sql = $"select {this.BuildTagName(tag)} from root.{database} where time>={begin:yyyy-MM-dd HH:mm:ss.fff} and time<={end:yyyy-MM-dd HH:mm:ss.fff} align by device";
        using var query = await session.ExecuteQueryStatementAsync(sql);
        var data = new List<(DateTime Time, double Value)>();
        while (query.HasNext())
        {
            var next = query.Next();
            var values = next.Values;
            var time = next.GetDateTime();
            if ("NULL".Equals(values[1])) continue;
            var value = Math.Round((double)values[1], digits);
            data.Add((time, value));
        }
        return data;
    }

    /// <summary>
    /// 获取测点归档数据,返回<time,value>
    /// </summary>
    /// <param name="tag">测点</param>
    /// <param name="begin">开始时间</param>
    /// <param name="end">结束时间</param>
    /// <param name="digits">数据精度,默认6位小数</param> 
    public async Task<Dictionary<string, List<(DateTime Time, double Value)>>> ArchiveAsync(List<string> tags, DateTime begin, DateTime end, int digits = 6)
    {
        var sql = $"select {string.Join(",", tags.Select(this.BuildTagName))} from root.{database} where time>={begin:yyyy-MM-dd HH:mm:ss.fff} and time<={end:yyyy-MM-dd HH:mm:ss.fff} align by device";
        using var query = await session.ExecuteQueryStatementAsync(sql);
        var data = tags.ToDictionary(kv => kv, kv => new List<(DateTime Time, double Value)>());
        while (query.HasNext())
        {
            var next = query.Next();
            var values = next.Values;
            var pts = next.Measurements;
            var time = next.GetDateTime();
            for (var i = 1; i < values.Count; i++)
            {
                if ("NULL".Equals(values[i])) continue;
                var value = Math.Round((double)values[i], digits);
                data[pts[i]].Add((time, value));
            }
        }
        return data;
    }

    /// <summary>
    /// 获取测点归档数据,返回<time,value>
    /// </summary> 
    /// <param name="tag">测点集合</param>
    /// <param name="begin">开始时间</param>
    /// <param name="end">结束时间</param>
    /// <param name="digits">数据精度,默认6位小数</param> 
    public async Task<List<(DateTime Time, double Value)>> HistoryAsync(string tag, DateTime begin, DateTime end, int digits = 6, int ms = 1000)
    {
        var @break = false;
        var data = new List<(DateTime Time, double Value)>();
        if ((end - begin).TotalHours > 4)//4小时以内可以不检测数据是否存在
        {
            var sql = $"select count({this.BuildTagName(tag)}) as exist from root.{database} where time >= {begin:yyyy-MM-dd HH:mm:ss} and time < {end:yyyy-MM-dd HH:mm:ss}";
            using var query = await session.ExecuteQueryStatementAsync(sql);
            @break = query.HasNext() && (long)query.Next().Values[0] == 0;
        }
        if (!@break)
        {
            var sql = $"select last_value({this.BuildTagName(tag)}) as {this.BuildTagName(tag)} from root.{database} group by ([{begin:yyyy-MM-dd HH:mm:ss},{end.AddMilliseconds(ms):yyyy-MM-dd HH:mm:ss}), {ms}ms) fill(double[previous])";
            using var query = await session.ExecuteQueryStatementAsync(sql);
            while (query.HasNext())
            {
                var next = query.Next();
                var values = next.Values;
                var time = next.GetDateTime();
                var value = "NULL".Equals(values[0]) ? double.NaN : Math.Round((double)values[0], digits);
                data.Add((time, value));
            }
        }
        return data;
    }

    /// <summary>
    /// 获取测点历史数据,等间隔采样,返回<time,value>
    /// </summary>
    /// <param name="tags">测点</param>
    /// <param name="begin">开始时间</param>
    /// <param name="end">结束时间</param>
    /// <param name="digits">数据精度,默认6位小数</param> 
    /// <param name="ms">采样间隔,单位毫秒,默认1秒</param>
    public async Task<Dictionary<string, List<(DateTime Time, double Value)>>> HistoryAsync(List<string> tags, DateTime begin, DateTime end, int digits = 6, int ms = 1000)
    {
        var @break = false;
        var data = tags.ToDictionary(kv => kv, kv => new List<(DateTime Time, double Value)>());
        if ((end - begin).TotalHours > 4)//4小时以内可以不检测数据是否存在
        {
            var sql = $"select count({this.BuildTagName(tags[0])}) as exist from root.{database} where time >= {begin:yyyy-MM-dd HH:mm:ss} and time < {end:yyyy-MM-dd HH:mm:ss}";
            using var query = await session.ExecuteQueryStatementAsync(sql);
            @break = query.HasNext() && (long)query.Next().Values[0] == 0;
        }
        if (!@break)
        {
            var sql = $"select {string.Join(",", tags.Select(this.BuildTagName).Select(tag => $"last_value({tag}) as {tag}"))} from root.{database} group by ([{begin:yyyy-MM-dd HH:mm:ss},{end.AddMilliseconds(ms):yyyy-MM-dd HH:mm:ss}), {ms}ms) fill(double[previous])";
            using var query = await session.ExecuteQueryStatementAsync(sql);
            while (query.HasNext())
            {
                var next = query.Next();
                var values = next.Values;
                var pts = next.Measurements;
                var time = next.GetDateTime();
                for (var i = 0; i < values.Count; i++)
                {
                    var value = "NULL".Equals(values[i]) ? double.NaN : Math.Round((double)values[i], digits);
                    data[pts[i]].Add((time, value));
                }
            }
        }
        return data;

    }

    /// <summary>
    /// 获取测点绘图数据,返回<time,value>
    /// </summary>
    /// <param name="tag">测点</param>
    /// <param name="begin">开始时间</param>
    /// <param name="end">结束时间</param>
    /// <param name="digits">数据精度,默认6位小数</param> 
    /// <param name="px">屏幕像素,默认1200</param>
    public async Task<List<(DateTime Time, double Value)>> PlotAsync(string tag, DateTime begin, DateTime end, int digits = 6, int px = 1200)
    {
        var raw = await ArchiveAsync(tag, begin, end, digits);
        var ts = end - begin;
        if (raw.Count > px && ts.TotalHours > 1)
        {
            return ByPx();
        }
        else
        {
            return raw;
        }
        List<(DateTime Time, double Value)> ByPx()
        {
            var plot = new List<(DateTime Time, double Value)>();
            if (raw.Any())
            {
                var span = Math.Floor(ts.TotalSeconds / px);
                Enumerable.Range(1, px).AsParallel().ForAll(i =>
                {
                    var items = raw.Where(x => x.Time > begin.AddSeconds((i - 1) * span) && x.Time <= begin.AddSeconds(i * span)).ToList();
                    var min = items.OrderBy(x => x.Value).FirstOrDefault();
                    var max = items.OrderByDescending(x => x.Value).FirstOrDefault();
                    var lst = items.LastOrDefault();
                    if (lst.Time != DateTime.MinValue) plot.Add(lst);
                    if (max != lst && max.Time != DateTime.MinValue) plot.Add(max);
                    if (min != lst && min.Time != DateTime.MinValue) plot.Add(min);
                });
                plot = plot.OrderBy(x => x.Time).ToList();
                if (plot[plot.Count - 1].Time != end) plot.Add(raw[raw.Count - 1]);
                if (plot[0] != raw[0]) plot.Insert(0, raw[0]);
            }
            return plot;
        }
    }

    /// <summary>
    /// 单测点历史数据
    /// </summary>
    /// <param name="tag">测点</param>
    /// <param name="data">数据集合</param>
    public async Task BulkWriteAsync(string tag, List<(DateTime Time, double Value)> data)
    {
        var matrix = new dynamic[data.Count + 1, 2];
        matrix[0, 0] = "Timestamp";
        matrix[0, 1] = tag;
        for (int i = 0; i < data.Count; i++)
        {
            matrix[i + 1, 0] = data[i].Time;
            matrix[i + 1, 1] = data[i].Value;
        }
        await BulkWriteAsync(matrix);
    }

    /// <summary>
    /// 多测点单时刻数据
    /// </summary>
    /// <param name="time">时间戳</param> 
    /// <param name="data">测点数据</param>
    public async Task BulkWriteAsync(DateTime time, List<(string Tag, double Value)> data)
    {
        var matrix = new dynamic[2, data.Count + 1];
        matrix[0, 0] = "Timestamp";
        matrix[1, 0] = time;
        for (int j = 0; j < data.Count; j++)
        {
            matrix[0, j + 1] = data[j].Tag;
            matrix[1, j + 1] = data[j].Value;
        }
        await BulkWriteAsync(matrix);
    }

    /// <summary>
    /// 多测点单时刻数据
    /// </summary>
    /// <param name="time">时间戳</param> 
    /// <param name="data">测点数据</param>
    public async Task BulkWriteAsync(DateTime time, Dictionary<string, double> data)
    {
        var matrix = new dynamic[2, data.Count + 1];
        matrix[0, 0] = "Timestamp";
        matrix[1, 0] = time;
        for (int j = 0; j < data.Count; j++)
        {
            matrix[0, j + 1] = data.ElementAt(j).Key;
            matrix[1, j + 1] = data.ElementAt(j).Value;
        }
        await BulkWriteAsync(matrix);
    }

    /// <summary>
    /// 获取测点快照数据
    /// </summary> 
    /// <param name="tags">测点集合</param>
    public async Task BulkWriteAsync(Dictionary<string, List<(DateTime Time, double Value)>> data)
    {
        foreach (var kv in data)
        {
            await this.BulkWriteAsync(kv.Key, kv.Value);
        }
    }

    /// <summary>
    /// 写入数据
    /// </summary>
    /// <param name="matrix"></param>
    public async Task BulkWriteAsync(dynamic[,] matrix)
    {
        var rows = matrix.GetUpperBound(0) + 1;
        var columns = matrix.GetUpperBound(1) + 1;
        if (rows != 0)
        {
            var cols = Enumerable.Range(1, columns - 1).ToList();
            var measurements = cols.Select((j) => ((string)matrix[0, j]).Replace("root.", string.Empty)).ToList();
            if (rows == 2)
            {
                var values = cols.Select((j) => matrix[1, j]).ToList();
                var record = new RowRecord(UTC_MS(matrix[1, 0]), values, measurements);
                var effect = await session.InsertRecordAsync($"root.{database}", record, false);
            }
            else
            {
                var block = 3600;
                var batch = Math.Ceiling(1.0 * rows / block);
                for (var b = 0; b < batch; b++)
                {
                    var timestamps = new List<DateTime>();
                    var values = new List<List<dynamic>>();
                    var count = Math.Min(rows, (b + 1) * 3600);
                    for (var i = 1 + b * 3600; i < count; i++)
                    {
                        timestamps.Add(matrix[i, 0]);
                        values.Add(cols.Select(j => matrix[i, j]).ToList());
                    }
                    var tablet = new Tablet($"root.{database}", measurements, values, timestamps);
                    var effect = await session.InsertTabletAsync(tablet, false);
                }

            }
        }
    }

    /// <summary>
    /// 初始化测点信息,所有测点均按照双精度浮点存储
    /// </summary>
    /// <param name="measurements">测点列表</param> 
    /// <param name="console">是否控制台输出日志</param>
    public async Task InitializeAsync(List<(string Tag, string Type, string Unit, string Desc)> measurements, bool console = true)
    {
        var exist = await PointsAsync();
        foreach (var point in measurements)
        {
            var _id = point.Tag;
            string sql;
            if (exist.Any(x => x.Tag == _id))
            {
                sql = $"alter timeseries root.{database}.`{_id}` upsert tags (t='{point.Type}', u='{point.Unit}', d='{point.Desc?.Replace(',', ';')}')";
            }
            else
            {
                sql = $"create timeseries root.{database}.`{_id}` with datatype=DOUBLE tags ( t='{point.Type}', u='{point.Unit}', d='{point.Desc?.Replace(',', ';')}')";
            }
            var effect = await session.ExecuteNonQueryStatementAsync(sql);
            if (console)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"IoTDB>>{(effect == 0 ? "Success" : "Failed")}->{sql}");
                Console.ResetColor();
            }
        }
    }

    /// <summary>
    /// 连接数据库
    /// </summary>
    public async Task<bool> OpenAsync()
    {
        if (!session.IsOpen) await session.OpenAsync();
        return session.IsOpen;
    }

    /// <summary>
    /// 关闭数据库
    /// </summary>
    public async Task<bool> CloseAsync()
    {
        await session.CloseAsync();
        return true;
    }

    /// <summary>
    /// 搜索测点
    /// </summary>
    /// <param name="device">所属设备或数据库</param>
    /// <param name="keywords">关键字</param> 
    public async Task<List<(string Tag, string? Type, string? Unit, string? Desc)>> PointsAsync(string keywords = "")
    {
        if (keywords?.StartsWith("/") == true) keywords = keywords[1..];
        if (keywords?.EndsWith("/") == true) keywords = keywords[0..^1];
        if (keywords?.EndsWith("/i") == true) keywords = keywords[0..^2];
        static JsonObject DeserializeObject(string json)
        {
            if (!string.IsNullOrEmpty(json) && json != "NULL")
            {
                try
                {
                    return JsonSerializer.Deserialize<JsonObject>(json.Replace("\\", "/"));
                }
                catch (System.Exception ex)
                {

                }
            }
            return new JsonObject();
        }
        using var query = await session.ExecuteQueryStatementAsync($"show timeseries root.{database}");
        var points = new List<(string Tag, string? Type, string? Desc, string? Unit)>();
        var len = database.Length + 6;
        var reg = new Regex(keywords ?? "", RegexOptions.IgnoreCase);
        while (query.HasNext())
        {
            var next = query.Next();
            var values = next.Values;
            var id = ((string)values[0])[len..];
            if (!reg.IsMatch(id)) continue;
            var tags = DeserializeObject((string)values[^2]) ?? new JsonObject();
            points.Add((id, (string)tags["t"], (string)tags["u"], (string)tags["d"]));
        }
        return points;
    }

    /// <summary>
    /// 测点名[tag|_id],类型[type],描述[desc]
    /// </summary>
    /// <param name="measurements">测点列表</param>
    /// <param name="fileName">文件名</param>
    public async Task PointsToCsvAsync(string fileName)
    {
        var measurements = await this.PointsAsync();
        var directory = Path.GetDirectoryName(fileName);
        if (!Directory.Exists(directory) && !string.IsNullOrEmpty(directory)) Directory.CreateDirectory(directory);
        await File.WriteAllTextAsync(fileName, "点名[tag|_id],类型[type],单位[unit],描述[desc]", System.Text.Encoding.UTF8);
        foreach (var row in measurements)
        {
            await File.AppendAllTextAsync(fileName, $"\r\n{row.Tag},{row.Type},{row.Unit},{row.Desc?.Replace(',', ';')}", System.Text.Encoding.UTF8);
        }
    }

    /// <summary>
    /// 数据导出CSV
    /// </summary>
    /// <param name="start">开始时间</param>
    /// <param name="end">截止时间</param>
    /// <param name="tags">测点列表，不指定时则导出所有测点</param> 
    public async Task DataToCsvAsync(DateTime start, DateTime end, List<string> tags = null)
    {
        if (Directory.Exists("csv"))
        {
            Directory.Delete("csv", true);
        }
        Directory.CreateDirectory("csv");
        if (tags == null)
        {
            tags =(await this.PointsAsync()).OrderBy(x => x.Tag).Select(x => x.Tag).ToList();
        }

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"准备数据导出, 测点数量: {tags.Count}, 时间: {start:yyyy-MM-dd} ~ {end:yyyy-MM-dd}");
        Console.ResetColor();
        var time = start;
        while (time < end)
        {
            var fn = $"csv/{start:yyyy-MM-dd}.csv";
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 读取数据 {time:yyyy-MM-dd} +({(time.Hour + 1).ToString().PadLeft(2, '0')}H)");
            Console.ResetColor();
            var hist = this.HistoryAsync(tags, time, time.AddHours(1).AddSeconds(-1)).Result;
            if (!File.Exists(fn))
            {
                File.WriteAllText(fn, $"Timestamp,{string.Join(",", tags)}{Environment.NewLine}", System.Text.Encoding.UTF8);
            }
            if (hist.Any())
            {
                var times = hist[tags[0]].Select(x => $"{x.Time:yyyy-MM-dd HH:mm:ss}").ToList();
                var lines = new List<string>();
                for (var i = 0; i < times.Count; i++)
                {
                    var ln = $"{times[i]},{string.Join(",", hist.Select(kv => kv.Value[i].Value.ToString()))}";
                    lines.Add(ln);
                }
                File.AppendAllLines(fn, lines, System.Text.Encoding.UTF8);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} 数据写入文件 `{fn}`");
                Console.ResetColor();
            }
            time = time.AddHours(1);
        }
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("数据导出完毕！");
    }

    /// <summary>
    /// 释放连接资源
    /// </summary>
    public void Dispose()
    {
        this.CloseAsync().Wait();
    }
}