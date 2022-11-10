/**
 * Autogenerated by Thrift Compiler (0.14.2)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Thrift;
using Thrift.Collections;

using Thrift.Protocol;
using Thrift.Protocol.Entities;
using Thrift.Protocol.Utilities;
using Thrift.Transport;
using Thrift.Transport.Client;
using Thrift.Transport.Server;
using Thrift.Processor;


#pragma warning disable IDE0079  // remove unnecessary pragmas
#pragma warning disable IDE1006  // parts of the code use IDL spelling


internal partial class TSExecuteStatementResp : TBase
{
    private long _queryId;
    private List<string> _columns;
    private string _operationType;
    private bool _ignoreTimeStamp;
    private List<string> _dataTypeList;
    private TSQueryDataSet _queryDataSet;
    private TSQueryNonAlignDataSet _nonAlignQueryDataSet;
    private Dictionary<string, int> _columnNameIndexMap;
    private List<string> _sgColumns;
    private List<sbyte> _aliasColumns;
    private TSTracingInfo _tracingInfo;

    public TSStatus Status { get; set; }

    public long QueryId
    {
        get
        {
            return _queryId;
        }
        set
        {
            __isset.queryId = true;
            this._queryId = value;
        }
    }

    public List<string> Columns
    {
        get
        {
            return _columns;
        }
        set
        {
            __isset.columns = true;
            this._columns = value;
        }
    }

    public string OperationType
    {
        get
        {
            return _operationType;
        }
        set
        {
            __isset.operationType = true;
            this._operationType = value;
        }
    }

    public bool IgnoreTimeStamp
    {
        get
        {
            return _ignoreTimeStamp;
        }
        set
        {
            __isset.ignoreTimeStamp = true;
            this._ignoreTimeStamp = value;
        }
    }

    public List<string> DataTypeList
    {
        get
        {
            return _dataTypeList;
        }
        set
        {
            __isset.dataTypeList = true;
            this._dataTypeList = value;
        }
    }

    public TSQueryDataSet QueryDataSet
    {
        get
        {
            return _queryDataSet;
        }
        set
        {
            __isset.queryDataSet = true;
            this._queryDataSet = value;
        }
    }

    public TSQueryNonAlignDataSet NonAlignQueryDataSet
    {
        get
        {
            return _nonAlignQueryDataSet;
        }
        set
        {
            __isset.nonAlignQueryDataSet = true;
            this._nonAlignQueryDataSet = value;
        }
    }

    public Dictionary<string, int> ColumnNameIndexMap
    {
        get
        {
            return _columnNameIndexMap;
        }
        set
        {
            __isset.columnNameIndexMap = true;
            this._columnNameIndexMap = value;
        }
    }

    public List<string> SgColumns
    {
        get
        {
            return _sgColumns;
        }
        set
        {
            __isset.sgColumns = true;
            this._sgColumns = value;
        }
    }

    public List<sbyte> AliasColumns
    {
        get
        {
            return _aliasColumns;
        }
        set
        {
            __isset.aliasColumns = true;
            this._aliasColumns = value;
        }
    }

    public TSTracingInfo TracingInfo
    {
        get
        {
            return _tracingInfo;
        }
        set
        {
            __isset.tracingInfo = true;
            this._tracingInfo = value;
        }
    }


    public Isset __isset;
    public struct Isset
    {
        public bool queryId;
        public bool columns;
        public bool operationType;
        public bool ignoreTimeStamp;
        public bool dataTypeList;
        public bool queryDataSet;
        public bool nonAlignQueryDataSet;
        public bool columnNameIndexMap;
        public bool sgColumns;
        public bool aliasColumns;
        public bool tracingInfo;
    }

    public TSExecuteStatementResp()
    {
    }

    public TSExecuteStatementResp(TSStatus status) : this()
    {
        this.Status = status;
    }

    public TSExecuteStatementResp DeepCopy()
    {
        var tmp38 = new TSExecuteStatementResp();
        if ((Status != null))
        {
            tmp38.Status = (TSStatus)this.Status.DeepCopy();
        }
        if (__isset.queryId)
        {
            tmp38.QueryId = this.QueryId;
        }
        tmp38.__isset.queryId = this.__isset.queryId;
        if ((Columns != null) && __isset.columns)
        {
            tmp38.Columns = this.Columns.DeepCopy();
        }
        tmp38.__isset.columns = this.__isset.columns;
        if ((OperationType != null) && __isset.operationType)
        {
            tmp38.OperationType = this.OperationType;
        }
        tmp38.__isset.operationType = this.__isset.operationType;
        if (__isset.ignoreTimeStamp)
        {
            tmp38.IgnoreTimeStamp = this.IgnoreTimeStamp;
        }
        tmp38.__isset.ignoreTimeStamp = this.__isset.ignoreTimeStamp;
        if ((DataTypeList != null) && __isset.dataTypeList)
        {
            tmp38.DataTypeList = this.DataTypeList.DeepCopy();
        }
        tmp38.__isset.dataTypeList = this.__isset.dataTypeList;
        if ((QueryDataSet != null) && __isset.queryDataSet)
        {
            tmp38.QueryDataSet = (TSQueryDataSet)this.QueryDataSet.DeepCopy();
        }
        tmp38.__isset.queryDataSet = this.__isset.queryDataSet;
        if ((NonAlignQueryDataSet != null) && __isset.nonAlignQueryDataSet)
        {
            tmp38.NonAlignQueryDataSet = (TSQueryNonAlignDataSet)this.NonAlignQueryDataSet.DeepCopy();
        }
        tmp38.__isset.nonAlignQueryDataSet = this.__isset.nonAlignQueryDataSet;
        if ((ColumnNameIndexMap != null) && __isset.columnNameIndexMap)
        {
            tmp38.ColumnNameIndexMap = this.ColumnNameIndexMap.DeepCopy();
        }
        tmp38.__isset.columnNameIndexMap = this.__isset.columnNameIndexMap;
        if ((SgColumns != null) && __isset.sgColumns)
        {
            tmp38.SgColumns = this.SgColumns.DeepCopy();
        }
        tmp38.__isset.sgColumns = this.__isset.sgColumns;
        if ((AliasColumns != null) && __isset.aliasColumns)
        {
            tmp38.AliasColumns = this.AliasColumns.DeepCopy();
        }
        tmp38.__isset.aliasColumns = this.__isset.aliasColumns;
        if ((TracingInfo != null) && __isset.tracingInfo)
        {
            tmp38.TracingInfo = (TSTracingInfo)this.TracingInfo.DeepCopy();
        }
        tmp38.__isset.tracingInfo = this.__isset.tracingInfo;
        return tmp38;
    }

    public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
    {
        iprot.IncrementRecursionDepth();
        try
        {
            bool isset_status = false;
            TField field;
            await iprot.ReadStructBeginAsync(cancellationToken);
            while (true)
            {
                field = await iprot.ReadFieldBeginAsync(cancellationToken);
                if (field.Type == TType.Stop)
                {
                    break;
                }

                switch (field.ID)
                {
                    case 1:
                        if (field.Type == TType.Struct)
                        {
                            Status = new TSStatus();
                            await Status.ReadAsync(iprot, cancellationToken);
                            isset_status = true;
                        }
                        else
                        {
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        }
                        break;
                    case 2:
                        if (field.Type == TType.I64)
                        {
                            QueryId = await iprot.ReadI64Async(cancellationToken);
                        }
                        else
                        {
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        }
                        break;
                    case 3:
                        if (field.Type == TType.List)
                        {
                            {
                                TList _list39 = await iprot.ReadListBeginAsync(cancellationToken);
                                Columns = new List<string>(_list39.Count);
                                for (int _i40 = 0; _i40 < _list39.Count; ++_i40)
                                {
                                    string _elem41;
                                    _elem41 = await iprot.ReadStringAsync(cancellationToken);
                                    Columns.Add(_elem41);
                                }
                                await iprot.ReadListEndAsync(cancellationToken);
                            }
                        }
                        else
                        {
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        }
                        break;
                    case 4:
                        if (field.Type == TType.String)
                        {
                            OperationType = await iprot.ReadStringAsync(cancellationToken);
                        }
                        else
                        {
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        }
                        break;
                    case 5:
                        if (field.Type == TType.Bool)
                        {
                            IgnoreTimeStamp = await iprot.ReadBoolAsync(cancellationToken);
                        }
                        else
                        {
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        }
                        break;
                    case 6:
                        if (field.Type == TType.List)
                        {
                            {
                                TList _list42 = await iprot.ReadListBeginAsync(cancellationToken);
                                DataTypeList = new List<string>(_list42.Count);
                                for (int _i43 = 0; _i43 < _list42.Count; ++_i43)
                                {
                                    string _elem44;
                                    _elem44 = await iprot.ReadStringAsync(cancellationToken);
                                    DataTypeList.Add(_elem44);
                                }
                                await iprot.ReadListEndAsync(cancellationToken);
                            }
                        }
                        else
                        {
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        }
                        break;
                    case 7:
                        if (field.Type == TType.Struct)
                        {
                            QueryDataSet = new TSQueryDataSet();
                            await QueryDataSet.ReadAsync(iprot, cancellationToken);
                        }
                        else
                        {
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        }
                        break;
                    case 8:
                        if (field.Type == TType.Struct)
                        {
                            NonAlignQueryDataSet = new TSQueryNonAlignDataSet();
                            await NonAlignQueryDataSet.ReadAsync(iprot, cancellationToken);
                        }
                        else
                        {
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        }
                        break;
                    case 9:
                        if (field.Type == TType.Map)
                        {
                            {
                                TMap _map45 = await iprot.ReadMapBeginAsync(cancellationToken);
                                ColumnNameIndexMap = new Dictionary<string, int>(_map45.Count);
                                for (int _i46 = 0; _i46 < _map45.Count; ++_i46)
                                {
                                    string _key47;
                                    int _val48;
                                    _key47 = await iprot.ReadStringAsync(cancellationToken);
                                    _val48 = await iprot.ReadI32Async(cancellationToken);
                                    ColumnNameIndexMap[_key47] = _val48;
                                }
                                await iprot.ReadMapEndAsync(cancellationToken);
                            }
                        }
                        else
                        {
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        }
                        break;
                    case 10:
                        if (field.Type == TType.List)
                        {
                            {
                                TList _list49 = await iprot.ReadListBeginAsync(cancellationToken);
                                SgColumns = new List<string>(_list49.Count);
                                for (int _i50 = 0; _i50 < _list49.Count; ++_i50)
                                {
                                    string _elem51;
                                    _elem51 = await iprot.ReadStringAsync(cancellationToken);
                                    SgColumns.Add(_elem51);
                                }
                                await iprot.ReadListEndAsync(cancellationToken);
                            }
                        }
                        else
                        {
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        }
                        break;
                    case 11:
                        if (field.Type == TType.List)
                        {
                            {
                                TList _list52 = await iprot.ReadListBeginAsync(cancellationToken);
                                AliasColumns = new List<sbyte>(_list52.Count);
                                for (int _i53 = 0; _i53 < _list52.Count; ++_i53)
                                {
                                    sbyte _elem54;
                                    _elem54 = await iprot.ReadByteAsync(cancellationToken);
                                    AliasColumns.Add(_elem54);
                                }
                                await iprot.ReadListEndAsync(cancellationToken);
                            }
                        }
                        else
                        {
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        }
                        break;
                    case 12:
                        if (field.Type == TType.Struct)
                        {
                            TracingInfo = new TSTracingInfo();
                            await TracingInfo.ReadAsync(iprot, cancellationToken);
                        }
                        else
                        {
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        }
                        break;
                    default:
                        await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        break;
                }

                await iprot.ReadFieldEndAsync(cancellationToken);
            }

            await iprot.ReadStructEndAsync(cancellationToken);
            if (!isset_status)
            {
                throw new TProtocolException(TProtocolException.INVALID_DATA);
            }
        }
        finally
        {
            iprot.DecrementRecursionDepth();
        }
    }

    public async global::System.Threading.Tasks.Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
    {
        oprot.IncrementRecursionDepth();
        try
        {
            var struc = new TStruct("TSExecuteStatementResp");
            await oprot.WriteStructBeginAsync(struc, cancellationToken);
            var field = new TField();
            if ((Status != null))
            {
                field.Name = "status";
                field.Type = TType.Struct;
                field.ID = 1;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                await Status.WriteAsync(oprot, cancellationToken);
                await oprot.WriteFieldEndAsync(cancellationToken);
            }
            if (__isset.queryId)
            {
                field.Name = "queryId";
                field.Type = TType.I64;
                field.ID = 2;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                await oprot.WriteI64Async(QueryId, cancellationToken);
                await oprot.WriteFieldEndAsync(cancellationToken);
            }
            if ((Columns != null) && __isset.columns)
            {
                field.Name = "columns";
                field.Type = TType.List;
                field.ID = 3;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                {
                    await oprot.WriteListBeginAsync(new TList(TType.String, Columns.Count), cancellationToken);
                    foreach (string _iter55 in Columns)
                    {
                        await oprot.WriteStringAsync(_iter55, cancellationToken);
                    }
                    await oprot.WriteListEndAsync(cancellationToken);
                }
                await oprot.WriteFieldEndAsync(cancellationToken);
            }
            if ((OperationType != null) && __isset.operationType)
            {
                field.Name = "operationType";
                field.Type = TType.String;
                field.ID = 4;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                await oprot.WriteStringAsync(OperationType, cancellationToken);
                await oprot.WriteFieldEndAsync(cancellationToken);
            }
            if (__isset.ignoreTimeStamp)
            {
                field.Name = "ignoreTimeStamp";
                field.Type = TType.Bool;
                field.ID = 5;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                await oprot.WriteBoolAsync(IgnoreTimeStamp, cancellationToken);
                await oprot.WriteFieldEndAsync(cancellationToken);
            }
            if ((DataTypeList != null) && __isset.dataTypeList)
            {
                field.Name = "dataTypeList";
                field.Type = TType.List;
                field.ID = 6;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                {
                    await oprot.WriteListBeginAsync(new TList(TType.String, DataTypeList.Count), cancellationToken);
                    foreach (string _iter56 in DataTypeList)
                    {
                        await oprot.WriteStringAsync(_iter56, cancellationToken);
                    }
                    await oprot.WriteListEndAsync(cancellationToken);
                }
                await oprot.WriteFieldEndAsync(cancellationToken);
            }
            if ((QueryDataSet != null) && __isset.queryDataSet)
            {
                field.Name = "queryDataSet";
                field.Type = TType.Struct;
                field.ID = 7;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                await QueryDataSet.WriteAsync(oprot, cancellationToken);
                await oprot.WriteFieldEndAsync(cancellationToken);
            }
            if ((NonAlignQueryDataSet != null) && __isset.nonAlignQueryDataSet)
            {
                field.Name = "nonAlignQueryDataSet";
                field.Type = TType.Struct;
                field.ID = 8;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                await NonAlignQueryDataSet.WriteAsync(oprot, cancellationToken);
                await oprot.WriteFieldEndAsync(cancellationToken);
            }
            if ((ColumnNameIndexMap != null) && __isset.columnNameIndexMap)
            {
                field.Name = "columnNameIndexMap";
                field.Type = TType.Map;
                field.ID = 9;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                {
                    await oprot.WriteMapBeginAsync(new TMap(TType.String, TType.I32, ColumnNameIndexMap.Count), cancellationToken);
                    foreach (string _iter57 in ColumnNameIndexMap.Keys)
                    {
                        await oprot.WriteStringAsync(_iter57, cancellationToken);
                        await oprot.WriteI32Async(ColumnNameIndexMap[_iter57], cancellationToken);
                    }
                    await oprot.WriteMapEndAsync(cancellationToken);
                }
                await oprot.WriteFieldEndAsync(cancellationToken);
            }
            if ((SgColumns != null) && __isset.sgColumns)
            {
                field.Name = "sgColumns";
                field.Type = TType.List;
                field.ID = 10;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                {
                    await oprot.WriteListBeginAsync(new TList(TType.String, SgColumns.Count), cancellationToken);
                    foreach (string _iter58 in SgColumns)
                    {
                        await oprot.WriteStringAsync(_iter58, cancellationToken);
                    }
                    await oprot.WriteListEndAsync(cancellationToken);
                }
                await oprot.WriteFieldEndAsync(cancellationToken);
            }
            if ((AliasColumns != null) && __isset.aliasColumns)
            {
                field.Name = "aliasColumns";
                field.Type = TType.List;
                field.ID = 11;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                {
                    await oprot.WriteListBeginAsync(new TList(TType.Byte, AliasColumns.Count), cancellationToken);
                    foreach (sbyte _iter59 in AliasColumns)
                    {
                        await oprot.WriteByteAsync(_iter59, cancellationToken);
                    }
                    await oprot.WriteListEndAsync(cancellationToken);
                }
                await oprot.WriteFieldEndAsync(cancellationToken);
            }
            if ((TracingInfo != null) && __isset.tracingInfo)
            {
                field.Name = "tracingInfo";
                field.Type = TType.Struct;
                field.ID = 12;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                await TracingInfo.WriteAsync(oprot, cancellationToken);
                await oprot.WriteFieldEndAsync(cancellationToken);
            }
            await oprot.WriteFieldStopAsync(cancellationToken);
            await oprot.WriteStructEndAsync(cancellationToken);
        }
        finally
        {
            oprot.DecrementRecursionDepth();
        }
    }

    public override bool Equals(object that)
    {
        if (!(that is TSExecuteStatementResp other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return System.Object.Equals(Status, other.Status)
          && ((__isset.queryId == other.__isset.queryId) && ((!__isset.queryId) || (System.Object.Equals(QueryId, other.QueryId))))
          && ((__isset.columns == other.__isset.columns) && ((!__isset.columns) || (TCollections.Equals(Columns, other.Columns))))
          && ((__isset.operationType == other.__isset.operationType) && ((!__isset.operationType) || (System.Object.Equals(OperationType, other.OperationType))))
          && ((__isset.ignoreTimeStamp == other.__isset.ignoreTimeStamp) && ((!__isset.ignoreTimeStamp) || (System.Object.Equals(IgnoreTimeStamp, other.IgnoreTimeStamp))))
          && ((__isset.dataTypeList == other.__isset.dataTypeList) && ((!__isset.dataTypeList) || (TCollections.Equals(DataTypeList, other.DataTypeList))))
          && ((__isset.queryDataSet == other.__isset.queryDataSet) && ((!__isset.queryDataSet) || (System.Object.Equals(QueryDataSet, other.QueryDataSet))))
          && ((__isset.nonAlignQueryDataSet == other.__isset.nonAlignQueryDataSet) && ((!__isset.nonAlignQueryDataSet) || (System.Object.Equals(NonAlignQueryDataSet, other.NonAlignQueryDataSet))))
          && ((__isset.columnNameIndexMap == other.__isset.columnNameIndexMap) && ((!__isset.columnNameIndexMap) || (TCollections.Equals(ColumnNameIndexMap, other.ColumnNameIndexMap))))
          && ((__isset.sgColumns == other.__isset.sgColumns) && ((!__isset.sgColumns) || (TCollections.Equals(SgColumns, other.SgColumns))))
          && ((__isset.aliasColumns == other.__isset.aliasColumns) && ((!__isset.aliasColumns) || (TCollections.Equals(AliasColumns, other.AliasColumns))))
          && ((__isset.tracingInfo == other.__isset.tracingInfo) && ((!__isset.tracingInfo) || (System.Object.Equals(TracingInfo, other.TracingInfo))));
    }

    public override int GetHashCode()
    {
        int hashcode = 157;
        unchecked
        {
            if ((Status != null))
            {
                hashcode = (hashcode * 397) + Status.GetHashCode();
            }
            if (__isset.queryId)
            {
                hashcode = (hashcode * 397) + QueryId.GetHashCode();
            }
            if ((Columns != null) && __isset.columns)
            {
                hashcode = (hashcode * 397) + TCollections.GetHashCode(Columns);
            }
            if ((OperationType != null) && __isset.operationType)
            {
                hashcode = (hashcode * 397) + OperationType.GetHashCode();
            }
            if (__isset.ignoreTimeStamp)
            {
                hashcode = (hashcode * 397) + IgnoreTimeStamp.GetHashCode();
            }
            if ((DataTypeList != null) && __isset.dataTypeList)
            {
                hashcode = (hashcode * 397) + TCollections.GetHashCode(DataTypeList);
            }
            if ((QueryDataSet != null) && __isset.queryDataSet)
            {
                hashcode = (hashcode * 397) + QueryDataSet.GetHashCode();
            }
            if ((NonAlignQueryDataSet != null) && __isset.nonAlignQueryDataSet)
            {
                hashcode = (hashcode * 397) + NonAlignQueryDataSet.GetHashCode();
            }
            if ((ColumnNameIndexMap != null) && __isset.columnNameIndexMap)
            {
                hashcode = (hashcode * 397) + TCollections.GetHashCode(ColumnNameIndexMap);
            }
            if ((SgColumns != null) && __isset.sgColumns)
            {
                hashcode = (hashcode * 397) + TCollections.GetHashCode(SgColumns);
            }
            if ((AliasColumns != null) && __isset.aliasColumns)
            {
                hashcode = (hashcode * 397) + TCollections.GetHashCode(AliasColumns);
            }
            if ((TracingInfo != null) && __isset.tracingInfo)
            {
                hashcode = (hashcode * 397) + TracingInfo.GetHashCode();
            }
        }
        return hashcode;
    }

    public override string ToString()
    {
        var sb = new StringBuilder("TSExecuteStatementResp(");
        if ((Status != null))
        {
            sb.Append(", Status: ");
            Status.ToString(sb);
        }
        if (__isset.queryId)
        {
            sb.Append(", QueryId: ");
            QueryId.ToString(sb);
        }
        if ((Columns != null) && __isset.columns)
        {
            sb.Append(", Columns: ");
            Columns.ToString(sb);
        }
        if ((OperationType != null) && __isset.operationType)
        {
            sb.Append(", OperationType: ");
            OperationType.ToString(sb);
        }
        if (__isset.ignoreTimeStamp)
        {
            sb.Append(", IgnoreTimeStamp: ");
            IgnoreTimeStamp.ToString(sb);
        }
        if ((DataTypeList != null) && __isset.dataTypeList)
        {
            sb.Append(", DataTypeList: ");
            DataTypeList.ToString(sb);
        }
        if ((QueryDataSet != null) && __isset.queryDataSet)
        {
            sb.Append(", QueryDataSet: ");
            QueryDataSet.ToString(sb);
        }
        if ((NonAlignQueryDataSet != null) && __isset.nonAlignQueryDataSet)
        {
            sb.Append(", NonAlignQueryDataSet: ");
            NonAlignQueryDataSet.ToString(sb);
        }
        if ((ColumnNameIndexMap != null) && __isset.columnNameIndexMap)
        {
            sb.Append(", ColumnNameIndexMap: ");
            ColumnNameIndexMap.ToString(sb);
        }
        if ((SgColumns != null) && __isset.sgColumns)
        {
            sb.Append(", SgColumns: ");
            SgColumns.ToString(sb);
        }
        if ((AliasColumns != null) && __isset.aliasColumns)
        {
            sb.Append(", AliasColumns: ");
            AliasColumns.ToString(sb);
        }
        if ((TracingInfo != null) && __isset.tracingInfo)
        {
            sb.Append(", TracingInfo: ");
            TracingInfo.ToString(sb);
        }
        sb.Append(')');
        return sb.ToString();
    }
}
