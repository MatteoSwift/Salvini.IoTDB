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


internal partial class TSOpenSessionResp : TBase
{
    private long _sessionId;
    private Dictionary<string, string> _configuration;

    public TSStatus Status { get; set; }

    /// <summary>
    /// 
    /// <seealso cref="global::.TSProtocolVersion"/>
    /// </summary>
    public TSProtocolVersion ServerProtocolVersion { get; set; }

    public long SessionId
    {
        get
        {
            return _sessionId;
        }
        set
        {
            __isset.sessionId = true;
            this._sessionId = value;
        }
    }

    public Dictionary<string, string> Configuration
    {
        get
        {
            return _configuration;
        }
        set
        {
            __isset.configuration = true;
            this._configuration = value;
        }
    }


    public Isset __isset;
    public struct Isset
    {
        public bool sessionId;
        public bool configuration;
    }

    public TSOpenSessionResp()
    {
        this.ServerProtocolVersion = TSProtocolVersion.IOTDB_SERVICE_PROTOCOL_V1;
    }

    public TSOpenSessionResp(TSStatus status, TSProtocolVersion serverProtocolVersion) : this()
    {
        this.Status = status;
        this.ServerProtocolVersion = serverProtocolVersion;
    }

    public TSOpenSessionResp DeepCopy()
    {
        var tmp61 = new TSOpenSessionResp();
        if ((Status != null))
        {
            tmp61.Status = (TSStatus)this.Status.DeepCopy();
        }
        tmp61.ServerProtocolVersion = this.ServerProtocolVersion;
        if (__isset.sessionId)
        {
            tmp61.SessionId = this.SessionId;
        }
        tmp61.__isset.sessionId = this.__isset.sessionId;
        if ((Configuration != null) && __isset.configuration)
        {
            tmp61.Configuration = this.Configuration.DeepCopy();
        }
        tmp61.__isset.configuration = this.__isset.configuration;
        return tmp61;
    }

    public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
    {
        iprot.IncrementRecursionDepth();
        try
        {
            bool isset_status = false;
            bool isset_serverProtocolVersion = false;
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
                        if (field.Type == TType.I32)
                        {
                            ServerProtocolVersion = (TSProtocolVersion)await iprot.ReadI32Async(cancellationToken);
                            isset_serverProtocolVersion = true;
                        }
                        else
                        {
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        }
                        break;
                    case 3:
                        if (field.Type == TType.I64)
                        {
                            SessionId = await iprot.ReadI64Async(cancellationToken);
                        }
                        else
                        {
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        }
                        break;
                    case 4:
                        if (field.Type == TType.Map)
                        {
                            {
                                TMap _map62 = await iprot.ReadMapBeginAsync(cancellationToken);
                                Configuration = new Dictionary<string, string>(_map62.Count);
                                for (int _i63 = 0; _i63 < _map62.Count; ++_i63)
                                {
                                    string _key64;
                                    string _val65;
                                    _key64 = await iprot.ReadStringAsync(cancellationToken);
                                    _val65 = await iprot.ReadStringAsync(cancellationToken);
                                    Configuration[_key64] = _val65;
                                }
                                await iprot.ReadMapEndAsync(cancellationToken);
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
            if (!isset_status)
            {
                throw new TProtocolException(TProtocolException.INVALID_DATA);
            }
            if (!isset_serverProtocolVersion)
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
            var struc = new TStruct("TSOpenSessionResp");
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
            field.Name = "serverProtocolVersion";
            field.Type = TType.I32;
            field.ID = 2;
            await oprot.WriteFieldBeginAsync(field, cancellationToken);
            await oprot.WriteI32Async((int)ServerProtocolVersion, cancellationToken);
            await oprot.WriteFieldEndAsync(cancellationToken);
            if (__isset.sessionId)
            {
                field.Name = "sessionId";
                field.Type = TType.I64;
                field.ID = 3;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                await oprot.WriteI64Async(SessionId, cancellationToken);
                await oprot.WriteFieldEndAsync(cancellationToken);
            }
            if ((Configuration != null) && __isset.configuration)
            {
                field.Name = "configuration";
                field.Type = TType.Map;
                field.ID = 4;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                {
                    await oprot.WriteMapBeginAsync(new TMap(TType.String, TType.String, Configuration.Count), cancellationToken);
                    foreach (string _iter66 in Configuration.Keys)
                    {
                        await oprot.WriteStringAsync(_iter66, cancellationToken);
                        await oprot.WriteStringAsync(Configuration[_iter66], cancellationToken);
                    }
                    await oprot.WriteMapEndAsync(cancellationToken);
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
        if (!(that is TSOpenSessionResp other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return System.Object.Equals(Status, other.Status)
          && System.Object.Equals(ServerProtocolVersion, other.ServerProtocolVersion)
          && ((__isset.sessionId == other.__isset.sessionId) && ((!__isset.sessionId) || (System.Object.Equals(SessionId, other.SessionId))))
          && ((__isset.configuration == other.__isset.configuration) && ((!__isset.configuration) || (TCollections.Equals(Configuration, other.Configuration))));
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
            hashcode = (hashcode * 397) + ServerProtocolVersion.GetHashCode();
            if (__isset.sessionId)
            {
                hashcode = (hashcode * 397) + SessionId.GetHashCode();
            }
            if ((Configuration != null) && __isset.configuration)
            {
                hashcode = (hashcode * 397) + TCollections.GetHashCode(Configuration);
            }
        }
        return hashcode;
    }

    public override string ToString()
    {
        var sb = new StringBuilder("TSOpenSessionResp(");
        if ((Status != null))
        {
            sb.Append(", Status: ");
            Status.ToString(sb);
        }
        sb.Append(", ServerProtocolVersion: ");
        ServerProtocolVersion.ToString(sb);
        if (__isset.sessionId)
        {
            sb.Append(", SessionId: ");
            SessionId.ToString(sb);
        }
        if ((Configuration != null) && __isset.configuration)
        {
            sb.Append(", Configuration: ");
            Configuration.ToString(sb);
        }
        sb.Append(')');
        return sb.ToString();
    }
}

