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


internal partial class TSOpenSessionReq : TBase
{
    private string _username;
    private string _password;
    private Dictionary<string, string> _configuration;

    /// <summary>
    /// 
    /// <seealso cref="global::.TSProtocolVersion"/>
    /// </summary>
    public TSProtocolVersion Client_protocol { get; set; }

    public string ZoneId { get; set; }

    public string Username
    {
        get
        {
            return _username;
        }
        set
        {
            __isset.username = true;
            this._username = value;
        }
    }

    public string Password
    {
        get
        {
            return _password;
        }
        set
        {
            __isset.password = true;
            this._password = value;
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
        public bool username;
        public bool password;
        public bool configuration;
    }

    public TSOpenSessionReq()
    {
        this.Client_protocol = TSProtocolVersion.IOTDB_SERVICE_PROTOCOL_V3;
    }

    public TSOpenSessionReq(TSProtocolVersion client_protocol, string zoneId) : this()
    {
        this.Client_protocol = client_protocol;
        this.ZoneId = zoneId;
    }

    public TSOpenSessionReq DeepCopy()
    {
        var tmp68 = new TSOpenSessionReq();
        tmp68.Client_protocol = this.Client_protocol;
        if ((ZoneId != null))
        {
            tmp68.ZoneId = this.ZoneId;
        }
        if ((Username != null) && __isset.username)
        {
            tmp68.Username = this.Username;
        }
        tmp68.__isset.username = this.__isset.username;
        if ((Password != null) && __isset.password)
        {
            tmp68.Password = this.Password;
        }
        tmp68.__isset.password = this.__isset.password;
        if ((Configuration != null) && __isset.configuration)
        {
            tmp68.Configuration = this.Configuration.DeepCopy();
        }
        tmp68.__isset.configuration = this.__isset.configuration;
        return tmp68;
    }

    public async global::System.Threading.Tasks.Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
    {
        iprot.IncrementRecursionDepth();
        try
        {
            bool isset_client_protocol = false;
            bool isset_zoneId = false;
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
                        if (field.Type == TType.I32)
                        {
                            Client_protocol = (TSProtocolVersion)await iprot.ReadI32Async(cancellationToken);
                            isset_client_protocol = true;
                        }
                        else
                        {
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        }
                        break;
                    case 2:
                        if (field.Type == TType.String)
                        {
                            ZoneId = await iprot.ReadStringAsync(cancellationToken);
                            isset_zoneId = true;
                        }
                        else
                        {
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        }
                        break;
                    case 3:
                        if (field.Type == TType.String)
                        {
                            Username = await iprot.ReadStringAsync(cancellationToken);
                        }
                        else
                        {
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        }
                        break;
                    case 4:
                        if (field.Type == TType.String)
                        {
                            Password = await iprot.ReadStringAsync(cancellationToken);
                        }
                        else
                        {
                            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
                        }
                        break;
                    case 5:
                        if (field.Type == TType.Map)
                        {
                            {
                                TMap _map69 = await iprot.ReadMapBeginAsync(cancellationToken);
                                Configuration = new Dictionary<string, string>(_map69.Count);
                                for (int _i70 = 0; _i70 < _map69.Count; ++_i70)
                                {
                                    string _key71;
                                    string _val72;
                                    _key71 = await iprot.ReadStringAsync(cancellationToken);
                                    _val72 = await iprot.ReadStringAsync(cancellationToken);
                                    Configuration[_key71] = _val72;
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
            if (!isset_client_protocol)
            {
                throw new TProtocolException(TProtocolException.INVALID_DATA);
            }
            if (!isset_zoneId)
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
            var struc = new TStruct("TSOpenSessionReq");
            await oprot.WriteStructBeginAsync(struc, cancellationToken);
            var field = new TField();
            field.Name = "client_protocol";
            field.Type = TType.I32;
            field.ID = 1;
            await oprot.WriteFieldBeginAsync(field, cancellationToken);
            await oprot.WriteI32Async((int)Client_protocol, cancellationToken);
            await oprot.WriteFieldEndAsync(cancellationToken);
            if ((ZoneId != null))
            {
                field.Name = "zoneId";
                field.Type = TType.String;
                field.ID = 2;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                await oprot.WriteStringAsync(ZoneId, cancellationToken);
                await oprot.WriteFieldEndAsync(cancellationToken);
            }
            if ((Username != null) && __isset.username)
            {
                field.Name = "username";
                field.Type = TType.String;
                field.ID = 3;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                await oprot.WriteStringAsync(Username, cancellationToken);
                await oprot.WriteFieldEndAsync(cancellationToken);
            }
            if ((Password != null) && __isset.password)
            {
                field.Name = "password";
                field.Type = TType.String;
                field.ID = 4;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                await oprot.WriteStringAsync(Password, cancellationToken);
                await oprot.WriteFieldEndAsync(cancellationToken);
            }
            if ((Configuration != null) && __isset.configuration)
            {
                field.Name = "configuration";
                field.Type = TType.Map;
                field.ID = 5;
                await oprot.WriteFieldBeginAsync(field, cancellationToken);
                {
                    await oprot.WriteMapBeginAsync(new TMap(TType.String, TType.String, Configuration.Count), cancellationToken);
                    foreach (string _iter73 in Configuration.Keys)
                    {
                        await oprot.WriteStringAsync(_iter73, cancellationToken);
                        await oprot.WriteStringAsync(Configuration[_iter73], cancellationToken);
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
        if (!(that is TSOpenSessionReq other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return System.Object.Equals(Client_protocol, other.Client_protocol)
          && System.Object.Equals(ZoneId, other.ZoneId)
          && ((__isset.username == other.__isset.username) && ((!__isset.username) || (System.Object.Equals(Username, other.Username))))
          && ((__isset.password == other.__isset.password) && ((!__isset.password) || (System.Object.Equals(Password, other.Password))))
          && ((__isset.configuration == other.__isset.configuration) && ((!__isset.configuration) || (TCollections.Equals(Configuration, other.Configuration))));
    }

    public override int GetHashCode()
    {
        int hashcode = 157;
        unchecked
        {
            hashcode = (hashcode * 397) + Client_protocol.GetHashCode();
            if ((ZoneId != null))
            {
                hashcode = (hashcode * 397) + ZoneId.GetHashCode();
            }
            if ((Username != null) && __isset.username)
            {
                hashcode = (hashcode * 397) + Username.GetHashCode();
            }
            if ((Password != null) && __isset.password)
            {
                hashcode = (hashcode * 397) + Password.GetHashCode();
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
        var sb = new StringBuilder("TSOpenSessionReq(");
        sb.Append(", Client_protocol: ");
        Client_protocol.ToString(sb);
        if ((ZoneId != null))
        {
            sb.Append(", ZoneId: ");
            ZoneId.ToString(sb);
        }
        if ((Username != null) && __isset.username)
        {
            sb.Append(", Username: ");
            Username.ToString(sb);
        }
        if ((Password != null) && __isset.password)
        {
            sb.Append(", Password: ");
            Password.ToString(sb);
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
