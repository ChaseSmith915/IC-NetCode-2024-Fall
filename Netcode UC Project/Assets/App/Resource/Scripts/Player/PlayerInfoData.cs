using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;

public struct PlayerInfoData : INetworkSerializable, IEquatable<PlayerInfoData>
{
    private ulong _clientId;
    private FixedString64Bytes _name;
    private bool _isPlayerConnected;
    public Color _colorId;

    public  PlayerInfoData(ulong id)
    {
        _clientId = id;
        _name = "";
        _isPlayerConnected = false;
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
            reader.ReadValueSafe(out _isPlayerConnected);
        }
        else
        {
            var writer = serializer.GetFastBufferWriter();
            writer.WriteValueSafe(_colorId);
            writer.WriteValueSafe(_clientId);
            writer.WriteValueSafe(_name);
            writer.WriteValueSafe(_isPlayerConnected);
        }
    }

    public bool Equals(PlayerInfoData other)
    {
        return _clientId == other._clientId && _name.Equals(other._name) && _isPlayerConnected == other._isPlayerConnected && _colorId.Equals(other._colorId);
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
