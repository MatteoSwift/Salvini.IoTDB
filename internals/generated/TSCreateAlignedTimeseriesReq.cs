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


internal partial class TSCreateAlignedTimeseriesReq : TBase
{
    private List<string> _measurementAlias;
    private List<Dictionary<string, string>> _tagsList;
    private List<Dictionary<string, string>> _attributesList;

    public long SessionId { get; set; }

    public string PrefixPath { get; set; }

    public List<string> Measurements { get; set; }

    public List<int> DataTypes { get; set; }

    public List<int> Encodings { get; set; }

    public List<int> Compressors { get; set; }

    public List<string> MeasurementAlias
    {
        get
        {
            return _measurementAlias;
        }
        set
        {
            __isset.measurementAlias = true;
            this._measurementAlias = value;
        }
    }

    public List<Dictionary<string, string>> TagsList
    {
        get
        {
            return _tagsList;
        }
        set
        {
            __isset.tagsList = true;
            this._tagsList = value;
        }
    }

    public List<Dictionary<string, string>> AttributesList
    {
        get
        {
            return _attributesList;
        }
        set
        {
            __isset.attributesList = true;
            this._attributesList = value;
        }
    }


    public Isset __isset;
    public struct Isset
    {
        public bool measurementAlias;
        public bool tagsList;
        public bool attributesList;
    }

    public TSCreateAlignedTimeseriesReq()
    {
    }

    public TSCreateAlignedTimeseriesReq(long sessionId, string prefixPath, List<string> measurements, List<int> dataTypes, List<int> encodings, List<int> compressors) : this()
    {
        this.SessionId = sessionId;
        this.PrefixPath = prefixPath;
        this.Measurements = measurements;
        this.DataTypes = dataTypes;
        this.Encodings = encodings;
        this.Compressors = compressors;
    }

    public TSCreateAlignedTimeseriesReq DeepCopy()
    {
        var tmp278 = new TSCreateAlignedTimeseriesReq();
        tmp278.SessionId = this.SessionId;
        if ((PrefixPath != null))
        {
            tmp278.PrefixPath = this.PrefixPath;
        }
        if ((Measurements != null))
        {
            tmp278.Measurements = this.Measurements.DeepCopy();
        }
        if ((DataTypes != null))
        {
            tmp278.DataTypes = this.DataTypes.DeepCopy();
        }
        if ((Encodings != null))
        {
            tmp278.Encodings = this.Encodings.DeepCopy();
        }
        if ((Compressors != null))
        {
            tmp278.Compressors = this.Compressors.DeepCopy();
        }
        if ((MeasurementAlias != null) && __isset.measurementAlias)
        {
            tmp278.MeasurementAlias = this.MeasurementAlias.DeepCopy();
        }
        tmp278.__isset.measurementAlias = this.__isset.measurementAlias;
        if ((TagsList != null) && __isset.tagsList)
        {
            tmp278.TagsList = this.TagsList.DeepCopy();
        }
        tmp278.__isset.tagsList = this.__isset.tagsList;
        if ((AttributesList != null) && __isset.attributesList)
        {
            tmp278.AttributesList = this.AttributesList.DeepCopy();
        }
        tmp278.__isset.attributesList = this.__isset.attributesList;
        return tmp278;
    }

    public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
    {
        iprot.IncrementRecursionDepth();
        try
        {
            bool isset_sessionId = false;
            bool isset_prefixPath = false;
            bool isset_measurements = false;
            bool isset_dataTypes = false;
            bool isset_encodings = false;
            bool isset_compressors = false;
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
                        if (field.Type == TType.I64)
                        {
                            SessionId = await iprot.ReadI64Async(cancellationToken);
                            isset_sessionId = true;
                        }
                        else
                        {
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        }
                        break;
                    case 2:
                        if (field.Type == TType.String)
                        {
                            PrefixPath = await iprot.ReadStringAsync(cancellationToken);
                            isset_prefixPath = true;
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
                                TList _list279 = await iprot.ReadListBeginAsync(cancellationToken);
                                Measurements = new List<string>(_list279.Count);
                                for (int _i280 = 0; _i280 < _list279.Count; ++_i280)
                                {
                                    string _elem281;
                                    _elem281 = await iprot.ReadStringAsync(cancellationToken);
                                    Measurements.Add(_elem281);
                                }
                                await iprot.ReadListEndAsync(cancellationToken);
                            }
                            isset_measurements = true;
                        }
                        else
                        {
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        }
                        break;
                    case 4:
                        if (field.Type == TType.List)
                        {
                            {
                                TList _list282 = await iprot.ReadListBeginAsync(cancellationToken);
                                DataTypes = new List<int>(_list282.Count);
                                for (int _i283 = 0; _i283 < _list282.Count; ++_i283)
                                {
                                    int _elem284;
                                    _elem284 = await iprot.ReadI32Async(cancellationToken);
                                    DataTypes.Add(_elem284);
                                }
                                await iprot.ReadListEndAsync(cancellationToken);
                            }
                            isset_dataTypes = true;
                        }
                        else
                        {
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        }
                        break;
                    case 5:
                        if (field.Type == TType.List)
                        {
                            {
                                TList _list285 = await iprot.ReadListBeginAsync(cancellationToken);
                                Encodings = new List<int>(_list285.Count);
                                for (int _i286 = 0; _i286 < _list285.Count; ++_i286)
                                {
                                    int _elem287;
                                    _elem287 = await iprot.ReadI32Async(cancellationToken);
                                    Encodings.Add(_elem287);
                                }
                                await iprot.ReadListEndAsync(cancellationToken);
                            }
                            isset_encodings = true;
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
                                TList _list288 = await iprot.ReadListBeginAsync(cancellationToken);
                                Compressors = new List<int>(_list288.Count);
                                for (int _i289 = 0; _i289 < _list288.Count; ++_i289)
                                {
                                    int _elem290;
                                    _elem290 = await iprot.ReadI32Async(cancellationToken);
                                    Compressors.Add(_elem290);
                                }
                                await iprot.ReadListEndAsync(cancellationToken);
                            }
                            isset_compressors = true;
                        }
                        else
                        {
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        }
                        break;
                    case 7:
                        if (field.Type == TType.List)
                        {
                            {
                                TList _list291 = await iprot.ReadListBeginAsync(cancellationToken);
                                MeasurementAlias = new List<string>(_list291.Count);
                                for (int _i292 = 0; _i292 < _list291.Count; ++_i292)
                                {
                                    string _elem293;
                                    _elem293 = await iprot.ReadStringAsync(cancellationToken);
                                    MeasurementAlias.Add(_elem293);
                                }
                                await iprot.ReadListEndAsync(cancellationToken);
                            }
                        }
                        else
                        {
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        }
                        break;
                    case 8:
                        if (field.Type == TType.List)
                        {
                            {
                                TList _list294 = await iprot.ReadListBeginAsync(cancellationToken);
                                TagsList = new List<Dictionary<string, string>>(_list294.Count);
                                for (int _i295 = 0; _i295 < _list294.Count; ++_i295)
                                {
                                    Dictionary<string, string> _elem296;
                                    {
                                        TMap _map297 = await iprot.ReadMapBeginAsync(cancellationToken);
                                        _elem296 = new Dictionary<string, string>(_map297.Count);
                                        for (int _i298 = 0; _i298 < _map297.Count; ++_i298)
                                        {
                                            string _key299;
                                            string _val300;
                                            _key299 = await iprot.ReadStringAsync(cancellationToken);
                                            _val300 = await iprot.ReadStringAsync(cancellationToken);
                                            _elem296[_key299] = _val300;
                                        }
                                        await iprot.ReadMapEndAsync(cancellationToken);
                                    }
                                    TagsList.Add(_elem296);
                                }
                                await iprot.ReadListEndAsync(cancellationToken);
                            }
                        }
                        else
                        {
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        }
                        break;
                    case 9:
                        if (field.Type == TType.List)
                        {
                            {
                                TList _list301 = await iprot.ReadListBeginAsync(cancellationToken);
                                AttributesList = new List<Dictionary<string, string>>(_list301.Count);
                                for (int _i302 = 0; _i302 < _list301.Count; ++_i302)
                                {
                                    Dictionary<string, string> _elem303;
                                    {
                                        TMap _map304 = await iprot.ReadMapBeginAsync(cancellationToken);
                                        _elem303 = new Dictionary<string, string>(_map304.Count);
                                        for (int _i305 = 0; _i305 < _map304.Count; ++_i305)
                                        {
                                            string _key306;
                                            string _val307;
                                            _key306 = await iprot.ReadStringAsync(cancellationToken);
                                            _val307 = await iprot.ReadStringAsync(cancellationToken);
                                            _elem303[_key306] = _val307;
                                        }
                                        await iprot.ReadMapEndAsync(cancellationToken);
                                    }
                                    AttributesList.Add(_elem303);
                                }
                                await iprot.ReadListEndAsync(cancellationToken);
                            }
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
            if (!isset_sessionId)
            {
                throw new TProtocolException(TProtocolException.INVALID_DATA);
            }
            if (!isset_prefixPath)
            {
                throw new TProtocolException(TProtocolException.INVALID_DATA);
            }
            if (!isset_measurements)
            {
                throw new TProtocolException(TProtocolException.INVALID_DATA);
            }
            if (!isset_dataTypes)
            {
                throw new TProtocolException(TProtocolException.INVALID_DATA);
            }
            if (!isset_encodings)
            {
                throw new TProtocolException(TProtocolException.INVALID_DATA);
            }
            if (!isset_compressors)
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
            var struc = new TStruct("TSCreateAlignedTimeseriesReq");
            await oprot.WriteStructBeginAsync(struc, cancellationToken);
            var field = new TField();
            field.Name = "sessionId";
            field.Type = TType.I64;
            field.ID = 1;
            await oprot.WriteFieldBeginAsync(field, cancellationToken);
            await oprot.WriteI64Async(SessionId, cancellationToken);
            await oprot.WriteFieldEndAsync(cancellationToken);
            if ((PrefixPath != null))
            {
                field.Name = "prefixPath";
                field.Type = TType.String;
                field.ID = 2;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                await oprot.WriteStringAsync(PrefixPath, cancellationToken);
                await oprot.WriteFieldEndAsync(cancellationToken);
            }
            if ((Measurements != null))
            {
                field.Name = "measurements";
                field.Type = TType.List;
                field.ID = 3;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                {
                    await oprot.WriteListBeginAsync(new TList(TType.String, Measurements.Count), cancellationToken);
                    foreach (string _iter308 in Measurements)
                    {
                        await oprot.WriteStringAsync(_iter308, cancellationToken);
                    }
                    await oprot.WriteListEndAsync(cancellationToken);
                }
                await oprot.WriteFieldEndAsync(cancellationToken);
            }
            if ((DataTypes != null))
            {
                field.Name = "dataTypes";
                field.Type = TType.List;
                field.ID = 4;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                {
                    await oprot.WriteListBeginAsync(new TList(TType.I32, DataTypes.Count), cancellationToken);
                    foreach (int _iter309 in DataTypes)
                    {
                        await oprot.WriteI32Async(_iter309, cancellationToken);
                    }
                    await oprot.WriteListEndAsync(cancellationToken);
                }
                await oprot.WriteFieldEndAsync(cancellationToken);
            }
            if ((Encodings != null))
            {
                field.Name = "encodings";
                field.Type = TType.List;
                field.ID = 5;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                {
                    await oprot.WriteListBeginAsync(new TList(TType.I32, Encodings.Count), cancellationToken);
                    foreach (int _iter310 in Encodings)
                    {
                        await oprot.WriteI32Async(_iter310, cancellationToken);
                    }
                    await oprot.WriteListEndAsync(cancellationToken);
                }
                await oprot.WriteFieldEndAsync(cancellationToken);
            }
            if ((Compressors != null))
            {
                field.Name = "compressors";
                field.Type = TType.List;
                field.ID = 6;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                {
                    await oprot.WriteListBeginAsync(new TList(TType.I32, Compressors.Count), cancellationToken);
                    foreach (int _iter311 in Compressors)
                    {
                        await oprot.WriteI32Async(_iter311, cancellationToken);
                    }
                    await oprot.WriteListEndAsync(cancellationToken);
                }
                await oprot.WriteFieldEndAsync(cancellationToken);
            }
            if ((MeasurementAlias != null) && __isset.measurementAlias)
            {
                field.Name = "measurementAlias";
                field.Type = TType.List;
                field.ID = 7;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                {
                    await oprot.WriteListBeginAsync(new TList(TType.String, MeasurementAlias.Count), cancellationToken);
                    foreach (string _iter312 in MeasurementAlias)
                    {
                        await oprot.WriteStringAsync(_iter312, cancellationToken);
                    }
                    await oprot.WriteListEndAsync(cancellationToken);
                }
                await oprot.WriteFieldEndAsync(cancellationToken);
            }
            if ((TagsList != null) && __isset.tagsList)
            {
                field.Name = "tagsList";
                field.Type = TType.List;
                field.ID = 8;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                {
                    await oprot.WriteListBeginAsync(new TList(TType.Map, TagsList.Count), cancellationToken);
                    foreach (Dictionary<string, string> _iter313 in TagsList)
                    {
                        {
                            await oprot.WriteMapBeginAsync(new TMap(TType.String, TType.String, _iter313.Count), cancellationToken);
                            foreach (string _iter314 in _iter313.Keys)
                            {
                                await oprot.WriteStringAsync(_iter314, cancellationToken);
                                await oprot.WriteStringAsync(_iter313[_iter314], cancellationToken);
                            }
                            await oprot.WriteMapEndAsync(cancellationToken);
                        }
                    }
                    await oprot.WriteListEndAsync(cancellationToken);
                }
                await oprot.WriteFieldEndAsync(cancellationToken);
            }
            if ((AttributesList != null) && __isset.attributesList)
            {
                field.Name = "attributesList";
                field.Type = TType.List;
                field.ID = 9;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                {
                    await oprot.WriteListBeginAsync(new TList(TType.Map, AttributesList.Count), cancellationToken);
                    foreach (Dictionary<string, string> _iter315 in AttributesList)
                    {
                        {
                            await oprot.WriteMapBeginAsync(new TMap(TType.String, TType.String, _iter315.Count), cancellationToken);
                            foreach (string _iter316 in _iter315.Keys)
                            {
                                await oprot.WriteStringAsync(_iter316, cancellationToken);
                                await oprot.WriteStringAsync(_iter315[_iter316], cancellationToken);
                            }
                            await oprot.WriteMapEndAsync(cancellationToken);
                        }
                    }
                    await oprot.WriteListEndAsync(cancellationToken);
                }
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
        if (!(that is TSCreateAlignedTimeseriesReq other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return System.Object.Equals(SessionId, other.SessionId)
          && System.Object.Equals(PrefixPath, other.PrefixPath)
          && TCollections.Equals(Measurements, other.Measurements)
          && TCollections.Equals(DataTypes, other.DataTypes)
          && TCollections.Equals(Encodings, other.Encodings)
          && TCollections.Equals(Compressors, other.Compressors)
          && ((__isset.measurementAlias == other.__isset.measurementAlias) && ((!__isset.measurementAlias) || (TCollections.Equals(MeasurementAlias, other.MeasurementAlias))))
          && ((__isset.tagsList == other.__isset.tagsList) && ((!__isset.tagsList) || (TCollections.Equals(TagsList, other.TagsList))))
          && ((__isset.attributesList == other.__isset.attributesList) && ((!__isset.attributesList) || (TCollections.Equals(AttributesList, other.AttributesList))));
    }

    public override int GetHashCode()
    {
        int hashcode = 157;
        unchecked
        {
            hashcode = (hashcode * 397) + SessionId.GetHashCode();
            if ((PrefixPath != null))
            {
                hashcode = (hashcode * 397) + PrefixPath.GetHashCode();
            }
            if ((Measurements != null))
            {
                hashcode = (hashcode * 397) + TCollections.GetHashCode(Measurements);
            }
            if ((DataTypes != null))
            {
                hashcode = (hashcode * 397) + TCollections.GetHashCode(DataTypes);
            }
            if ((Encodings != null))
            {
                hashcode = (hashcode * 397) + TCollections.GetHashCode(Encodings);
            }
            if ((Compressors != null))
            {
                hashcode = (hashcode * 397) + TCollections.GetHashCode(Compressors);
            }
            if ((MeasurementAlias != null) && __isset.measurementAlias)
            {
                hashcode = (hashcode * 397) + TCollections.GetHashCode(MeasurementAlias);
            }
            if ((TagsList != null) && __isset.tagsList)
            {
                hashcode = (hashcode * 397) + TCollections.GetHashCode(TagsList);
            }
            if ((AttributesList != null) && __isset.attributesList)
            {
                hashcode = (hashcode * 397) + TCollections.GetHashCode(AttributesList);
            }
        }
        return hashcode;
    }

    public override string ToString()
    {
        var sb = new StringBuilder("TSCreateAlignedTimeseriesReq(");
        sb.Append(", SessionId: ");
        SessionId.ToString(sb);
        if ((PrefixPath != null))
        {
            sb.Append(", PrefixPath: ");
            PrefixPath.ToString(sb);
        }
        if ((Measurements != null))
        {
            sb.Append(", Measurements: ");
            Measurements.ToString(sb);
        }
        if ((DataTypes != null))
        {
            sb.Append(", DataTypes: ");
            DataTypes.ToString(sb);
        }
        if ((Encodings != null))
        {
            sb.Append(", Encodings: ");
            Encodings.ToString(sb);
        }
        if ((Compressors != null))
        {
            sb.Append(", Compressors: ");
            Compressors.ToString(sb);
        }
        if ((MeasurementAlias != null) && __isset.measurementAlias)
        {
            sb.Append(", MeasurementAlias: ");
            MeasurementAlias.ToString(sb);
        }
        if ((TagsList != null) && __isset.tagsList)
        {
            sb.Append(", TagsList: ");
            TagsList.ToString(sb);
        }
        if ((AttributesList != null) && __isset.attributesList)
        {
            sb.Append(", AttributesList: ");
            AttributesList.ToString(sb);
        }
        sb.Append(')');
        return sb.ToString();
    }
}

