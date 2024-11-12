using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;

public struct PlayerInfoData : INetworkSerializable, IEquatable<PlayerInfoData>
{
    public ulong _clientId;
    private FixedString64Bytes _name;
    public bool _isPlayerReady;
    public Color _colorId;

    public  PlayerInfoData(ulong id)
    {
        _clientId = id;
        _name = "";
        _isPlayerReady = false;
        _colorId = Color.magenta;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        if(serializer.IsReader)
        {
            var reader = serializer.GetFastBufferReader();
            reader.ReadValueSafe(out _clientId);
            reader.ReadValueSafe(out _name);
            reader.ReadValueSafe(out _colorId);
            reader.ReadValueSafe(out _isPlayerReady);
        }
        else
        {
            var writer = serializer.GetFastBufferWriter();
            writer.WriteValueSafe(_clientId);
            writer.WriteValueSafe(_name);
            writer.WriteValueSafe(_colorId);
            writer.WriteValueSafe(_isPlayerReady);
        }
    }

    public bool Equals(PlayerInfoData other)
    {
        return _clientId == other._clientId && _name.Equals(other._name) && _isPlayerReady == other._isPlayerReady && _colorId.Equals(other._colorId);
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString() => _name.Value.ToString();

    public static implicit operator string (PlayerInfoData name) => name.ToString();

    public static implicit operator PlayerInfoData(string s) => 
        new PlayerInfoData { _name = new FixedString64Bytes(s) };
}
