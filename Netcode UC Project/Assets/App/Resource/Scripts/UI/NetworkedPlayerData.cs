using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkedPlayerData : NetworkBehaviour
{
    public NetworkList<PlayerInfoData> _allConnectedPlayers;
    private int _players = -1;
    private ulong _serverLocalID;

    private Color[] _playerColors = new Color[]
    {
        Color.blue,
        Color.red,
        Color.green,
        Color.yellow,
        Color.cyan
    };

    private void Awake()
    {
        _allConnectedPlayers = new NetworkList<PlayerInfoData>(readPerm: NetworkVariableReadPermission.Everyone);
    }


    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if(IsServer)
        {
            NetworkManager.Singleton.OnConnectionEvent += OnConnectionEvents;
            _serverLocalID = NetworkManager.LocalClientId;
        }
    }

    public override void OnNetworkDespawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.OnConnectionEvent -= OnConnectionEvents;
        }

        base.OnNetworkDespawn();
    }

    private void OnConnectionEvents(NetworkManager netManager, ConnectionEventData eventData)
    {
        if(eventData.EventType == ConnectionEvent.ClientConnected)
        {
            CreateNewClientData(eventData.ClientId);
        }

        if(eventData.EventType == ConnectionEvent.ClientDisconnected)
        {
            _players--;
        }
    }

    private void CreateNewClientData(ulong clientID)
    {
        PlayerInfoData playerInfoData = new PlayerInfoData(clientID);

        if (_serverLocalID == clientID)
        {
            playerInfoData._isPlayerReady = true;
        }
        else
        {
            playerInfoData._isPlayerReady = false;
        }

        _players++;

        playerInfoData._colorId = _playerColors[_players];

        _allConnectedPlayers.Add(playerInfoData);
    }

    public void RemovePlayerData(PlayerInfoData playerInfoData)
    {
        _allConnectedPlayers.Remove(playerInfoData);
    }

    public PlayerInfoData FindPlayerInfoData(ulong clientID)
    {
        return _allConnectedPlayers[FindPlayerIndex(clientID)];
    }

    private int FindPlayerIndex(ulong clientID)
    {
        int myMach = -1;

        for (int i = 0; i < _allConnectedPlayers.Count; i++)
        {
            if (clientID == _allConnectedPlayers[i]._clientId)
            {
                myMach = i;
            }
        }

        return myMach;
    }

    private void UpdateReadyClient(ulong clientID, bool isReady)
    {
        int idx = FindPlayerIndex(clientID);

        if(idx == -1) { return; }


        PlayerInfoData playerInfo = new PlayerInfoData();
        playerInfo = _allConnectedPlayers[idx];
        playerInfo._isPlayerReady = isReady;
        _allConnectedPlayers[idx] = playerInfo;

    }

}
