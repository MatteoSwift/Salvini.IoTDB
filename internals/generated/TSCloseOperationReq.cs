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


internal partial class TSCloseOperationReq : TBase
{
    private long _queryId;
    private long _statementId;

    public long SessionId { get; set; }

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

    public long StatementId
    {
        get
        {
            return _statementId;
        }
        set
        {
            __isset.statementId = true;
            this._statementId = value;
        }
    }


    public Isset __isset;
    public struct Isset
    {
        public bool queryId;
        public bool statementId;
    }

    public TSCloseOperationReq()
    {
    }

    public TSCloseOperationReq(long sessionId) : this()
    {
        this.SessionId = sessionId;
    }

    public TSCloseOperationReq DeepCopy()
    {
        var tmp89 = new TSCloseOperationReq();
        tmp89.SessionId = this.SessionId;
        if (__isset.queryId)
        {
            tmp89.QueryId = this.QueryId;
        }
        tmp89.__isset.queryId = this.__isset.queryId;
        if (__isset.statementId)
        {
            tmp89.StatementId = this.StatementId;
        }
        tmp89.__isset.statementId = this.__isset.statementId;
        return tmp89;
    }

    public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
    {
        iprot.IncrementRecursionDepth();
        try
        {
            bool isset_sessionId = false;
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
                        if (field.Type == TType.I64)
                        {
                            StatementId = await iprot.ReadI64Async(cancellationToken);
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
            var struc = new TStruct("TSCloseOperationReq");
            await oprot.WriteStructBeginAsync(struc, cancellationToken);
            var field = new TField();
            field.Name = "sessionId";
            field.Type = TType.I64;
            field.ID = 1;
            await oprot.WriteFieldBeginAsync(field, cancellationToken);
            await oprot.WriteI64Async(SessionId, cancellationToken);
            await oprot.WriteFieldEndAsync(cancellationToken);
            if (__isset.queryId)
            {
                field.Name = "queryId";
                field.Type = TType.I64;
                field.ID = 2;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                await oprot.WriteI64Async(QueryId, cancellationToken);
                await oprot.WriteFieldEndAsync(cancellationToken);
            }
            if (__isset.statementId)
            {
                field.Name = "statementId";
                field.Type = TType.I64;
                field.ID = 3;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                await oprot.WriteI64Async(StatementId, cancellationToken);
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
        if (!(that is TSCloseOperationReq other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return System.Object.Equals(SessionId, other.SessionId)
          && ((__isset.queryId == other.__isset.queryId) && ((!__isset.queryId) || (System.Object.Equals(QueryId, other.QueryId))))
          && ((__isset.statementId == other.__isset.statementId) && ((!__isset.statementId) || (System.Object.Equals(StatementId, other.StatementId))));
    }

    public override int GetHashCode()
    {
        int hashcode = 157;
        unchecked
        {
            hashcode = (hashcode * 397) + SessionId.GetHashCode();
            if (__isset.queryId)
            {
                hashcode = (hashcode * 397) + QueryId.GetHashCode();
            }
            if (__isset.statementId)
            {
                hashcode = (hashcode * 397) + StatementId.GetHashCode();
            }
        }
        return hashcode;
    }

    public override string ToString()
    {
        var sb = new StringBuilder("TSCloseOperationReq(");
        sb.Append(", SessionId: ");
        SessionId.ToString(sb);
        if (__isset.queryId)
        {
            sb.Append(", QueryId: ");
            QueryId.ToString(sb);
        }
        if (__isset.statementId)
        {
            sb.Append(", StatementId: ");
            StatementId.ToString(sb);
        }
        sb.Append(')');
        return sb.ToString();
    }
}

