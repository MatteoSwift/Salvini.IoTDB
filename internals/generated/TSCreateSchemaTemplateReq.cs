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


internal partial class TSCreateSchemaTemplateReq : TBase
{

    public long SessionId { get; set; }

    public string Name { get; set; }

    public byte[] SerializedTemplate { get; set; }

    public TSCreateSchemaTemplateReq()
    {
    }

    public TSCreateSchemaTemplateReq(long sessionId, string name, byte[] serializedTemplate) : this()
    {
        this.SessionId = sessionId;
        this.Name = name;
        this.SerializedTemplate = serializedTemplate;
    }

    public TSCreateSchemaTemplateReq DeepCopy()
    {
        var tmp387 = new TSCreateSchemaTemplateReq();
        tmp387.SessionId = this.SessionId;
        if ((Name != null))
        {
            tmp387.Name = this.Name;
        }
        if ((SerializedTemplate != null))
        {
            tmp387.SerializedTemplate = this.SerializedTemplate.ToArray();
        }
        return tmp387;
    }

    public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
    {
        iprot.IncrementRecursionDepth();
        try
        {
            bool isset_sessionId = false;
            bool isset_name = false;
            bool isset_serializedTemplate = false;
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
                            Name = await iprot.ReadStringAsync(cancellationToken);
                            isset_name = true;
                        }
                        else
                        {
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        }
                        break;
                    case 3:
                        if (field.Type == TType.String)
                        {
                            SerializedTemplate = await iprot.ReadBinaryAsync(cancellationToken);
                            isset_serializedTemplate = true;
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
            if (!isset_name)
            {
                throw new TProtocolException(TProtocolException.INVALID_DATA);
            }
            if (!isset_serializedTemplate)
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
            var struc = new TStruct("TSCreateSchemaTemplateReq");
            await oprot.WriteStructBeginAsync(struc, cancellationToken);
            var field = new TField();
            field.Name = "sessionId";
            field.Type = TType.I64;
            field.ID = 1;
            await oprot.WriteFieldBeginAsync(field, cancellationToken);
            await oprot.WriteI64Async(SessionId, cancellationToken);
            await oprot.WriteFieldEndAsync(cancellationToken);
            if ((Name != null))
            {
                field.Name = "name";
                field.Type = TType.String;
                field.ID = 2;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                await oprot.WriteStringAsync(Name, cancellationToken);
                await oprot.WriteFieldEndAsync(cancellationToken);
            }
            if ((SerializedTemplate != null))
            {
                field.Name = "serializedTemplate";
                field.Type = TType.String;
                field.ID = 3;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                await oprot.WriteBinaryAsync(SerializedTemplate, cancellationToken);
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
        if (!(that is TSCreateSchemaTemplateReq other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return System.Object.Equals(SessionId, other.SessionId)
          && System.Object.Equals(Name, other.Name)
          && TCollections.Equals(SerializedTemplate, other.SerializedTemplate);
    }

    public override int GetHashCode()
    {
        int hashcode = 157;
        unchecked
        {
            hashcode = (hashcode * 397) + SessionId.GetHashCode();
            if ((Name != null))
            {
                hashcode = (hashcode * 397) + Name.GetHashCode();
            }
            if ((SerializedTemplate != null))
            {
                hashcode = (hashcode * 397) + SerializedTemplate.GetHashCode();
            }
        }
        return hashcode;
    }

    public override string ToString()
    {
        var sb = new StringBuilder("TSCreateSchemaTemplateReq(");
        sb.Append(", SessionId: ");
        SessionId.ToString(sb);
        if ((Name != null))
        {
            sb.Append(", Name: ");
            Name.ToString(sb);
        }
        if ((SerializedTemplate != null))
        {
            sb.Append(", SerializedTemplate: ");
            SerializedTemplate.ToString(sb);
        }
        sb.Append(')');
        return sb.ToString();
    }
}
