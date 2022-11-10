namespace Salvini.IoTDB.Data;

/// <summary>
/// IoTDB数据类型枚举
/// </summary>
internal enum DataType
{
    BOOLEAN,
    INT32,
    INT64,
    FLOAT,
    DOUBLE,
    TEXT,

    // default value must be 0
    NONE
}

/// <summary>
/// IoTDB数据编码枚举
/// </summary>
internal enum Encoding
{
    PLAIN,
    PLAIN_DICTIONARY,
    RLE,
    DIFF,
    TS_2DIFF,
    BITMAP,
    GORILLA_V1,
    REGULAR,
    GORILLA,

    // default value must be 0
    NONE
}

/// <summary>
/// 压缩方式枚举
/// </summary>
internal enum Compressor
{
    UNCOMPRESSED,
    SNAPPY,
    GZIP,
    LZO,
    SDT,
    PAA,
    PLA,
    LZ4
}

internal enum TemplateQueryType
{
    COUNT_MEASUREMENTS,
    IS_MEASUREMENT,
    PATH_EXIST,
    SHOW_MEASUREMENTS,
    SHOW_TEMPLATES,
    SHOW_SET_TEMPLATES,
    SHOW_USING_TEMPLATES
}
internal class TsFileConstant
{

    public static string TSFILE_SUFFIX = ".tsfile";
    public static string TSFILE_HOME = "TSFILE_HOME";
    public static string TSFILE_CONF = "TSFILE_CONF";
    public static string PATH_ROOT = "root";
    public static string TMP_SUFFIX = "tmp";
    public static string PATH_SEPARATOR = ".";
    public static char PATH_SEPARATOR_CHAR = '.';
    public static string PATH_SEPARATER_NO_REGEX = "\\.";
    public static char DOUBLE_QUOTE = '"';

    public static byte TIME_COLUMN_MASK = (byte)0x80;
    public static byte VALUE_COLUMN_MASK = (byte)0x40;

    private TsFileConstant() { }
}
