# Apache IoTDB Client - just like PI or eDNA

IoTDB C# 客户端，提供 PI\eDNA 类似的访问接口，与原生的 session 完全不同，比较适用于有传统实时历史数据库操作经验者。

以前，有封装 MongoDB 提供类似的实时历史数据库访问，但应用场景更多是注重历史性能，接触 IoTDB 之后果断放弃 MongoDB，完全兼顾实时历史性能。

> 提示：0.13 进行了 SQL 语法的改动，不使用反引号括起的标识符中仅能包含如下字符，否则需要使用反引号括起。 [0-9 a-z A-Z _ : @ # $ { }] （字母，数字，部分特殊字符）

只能呵呵了, 不知道是想什么呢, 竟然修改 `path` 的规则, 要知道查询数据完全依赖SQL, 升级时竟然还对点名 `path` 进行规则修改!!! 怎么想的呢???

> 想想看, 还是对 session 封装之后比较好, 不对上层暴露 SQL 查询, 谁知道以后 SQL 还有哪些不兼容呢, 那些上层业务要敢升级数据库...被玩死算了

## [Apache-IoTDB-Client-CSharp](https://github.com/eedalong/Apache-IoTDB-Client-CSharp)

> 这是最接近官方的 Apache IoTDB Client - C#版客户端，完全原生接口。由于不太符合本人的使用习惯，故提供此库。

IoTDB-SQL 着实令人恼火，其内部人员还美其名曰要走自己的路形成规范，学生啊太自信了，而本身就没规范可言，如 where 和 group by 语义冲突...

> 期盼：iotdb server 端可以支持 plot 查询（降采样，但保留特征值的趋势曲线，如果能做到 1 秒查询 1 周趋势就厉害了！）

### Apache Thrift ™

- 下载 [rpc.thrift](https://github.com/apache/iotdb/blob/master/thrift/src/main/thrift/rpc.thrift)
- 下载 [thrift-0.18.1](http://www.apache.org/dyn/closer.cgi?path=/thrift/0.18.1/thrift-0.18.1.exe)

- 生成 RPC 调用代码

```
thrif-0.18.1.exe -r -gen netstd rpc.thrift
```

# TimeSeries Client

- 采用 Apache IoTDB 时序数据库，提供类似 PI、eDNA 传统实时历史数据库访问 API
- 采用 MongoDB 连接字符串格式，如 `iotdb://root:admin#123@127.0.0.1:6667/database=dbname&fetchSize=1800&poolSize=8`

## 关键概念

| Name                 | Description                                                                                        |
| -------------------- | -------------------------------------------------------------------------------------------------- |
| 快照值 snapshot      | 测点的最新值，时间戳不一定相同                                                                     |
| 原始值 raw/archive   | 被记录的测点值，由传感器真实采集而来                                                               |
| 历史值 history/timed | 与`原始值`相比，给定时间必然有数值，但不一定是设备实际采集而来，可通过取值规则决定。               |
| 绘图值 plot          | 趋势绘图值，能够真实反映测点的历史趋势曲线，根据显示像素 px 划分区间降采样，不丢失特征值（极值）。 |

## 调用样例

```C#
using Salvini.IoTDB;

class Program
{
    static async Task Main(string[] args)
    {
        using var client1 = TimeSeriesClient.CreateInstance("iotdb://root:admin#123@192.168.145.120:6667/database=mydb1");
        using var client2 = TimeSeriesClient.CreateInstance("iotdb://root:admin#123@192.168.145.120:6667/database=mydb2");

        await client2.InitializeAsync(new List<(string Tag, string Type, string Unit, string Desc)> { ("MW", "AI", "兆瓦", "机组负荷") });
        var archive1 = await client1.HistoryAsync(new List<string> { "RATE_PV" }, new DateTime(2022, 8, 4).AddHours(14), new DateTime(2022, 8, 4).AddHours(15), 4);
        await client2.BulkWriteAsync("RATE_PV", archive1["RATE_PV"]);
        var archive2 = await client2.HistoryAsync(new List<string> { "RATE_PV" }, new DateTime(2022, 8, 4).AddHours(14), new DateTime(2022, 8, 4).AddHours(15), 4);
    }
}

```

## Version 1.0.23.310
+ 解决 `BulkWriteAsync` 测点路径带有连字符(-)问题
 
## Version 1.0.23.306
+ 解决 `PointAsync` 向下兼容问题, show timeseries `root.device.**`
+ 解决 `BulkWriteAsync` 测点路径带有连字符(-)问题
- 增强 `HistoryAsync` 实现，分批测点读取数据
- 增加 `DataFromCsvAsync` 实现，导入历史数据


## Version 1.0.23.216
+ 版本命名变化，跟随 iotdb 主版本.子版本.yy.mmdd
+ 解决 DataToCsvAsync 方法BUG，当无测点时，导致程序异常。


## Version 2.13.1110
+ 解决 TimeSeriesClient 实例化时控制台输出 Version 错误BUG
- 增加 DataToCsvAsync 方法，支持将数据导出至 CSV 文件，注意：按 History 方式导出数据


## Version 2.13.985
- 增强 `BulkWriteAsync` 实现，支持大数据写入


## Version 2.13.980
- 增加 `BulkWriteAsync` 重载
- 增加 `Vision` 属性,显示服务端版本号
- 解决测点名带有(-)连字符问题(path不完全兼容,0.13版以后要使用(\`)反引号括起来非法点名)


## Version 2.13.970.817
- 实现`ArchiveAsync`批量数据查询功能
- 解决`HistoryAsync`批量查询时测点与数据不对应的BUG


## Version 2.13.970.0
- 解决 `InitializeAsync` 无法更新测点信息 BUG
- 去掉对 ApacheIoTDB 访问接口的直接公开(原生 `session` 接口)，仅公开 `TimeSeriesClient` 对象
