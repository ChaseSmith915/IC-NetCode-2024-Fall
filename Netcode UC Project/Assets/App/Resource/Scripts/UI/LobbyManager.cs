using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : NetworkBehaviour
{
    [SerializeField] private Button _startBttn, _leaveBttn, _readyBttn;
    [SerializeField] private GameObject panelPrefab;
    [SerializeField] private GameObject _contentGO;
    [SerializeField] private TMP_Text rdyTxt;

    [SerializeField] private NetworkedPlayerData _networkPlayers;


    private List<GameObject> _playerPanels = new List<GameObject>();

    private ulong _myLocalClientID;

    private bool isReady = false;

    private void Start()
    {
        _myLocalClientID = NetworkManager.ServerClientId;

        if(IsServer)
        {
            rdyTxt.text = "Waiting for Players";
            _readyBttn.gameObject.SetActive(false);
        }
        else
        {
            rdyTxt.text = "Not Ready";
            _readyBttn.gameObject.SetActive(true);
        }

        _networkPlayers._allConnectedPlayers.OnListChanged += NetPlayersChanged;
        _leaveBttn.onClick.AddListener(LeaveBttnClick);
        _readyBttn.onClick.AddListener(ClientRdyBttnToggle);
    }

    private void ClientRdyBttnToggle()
    {
        if(IsServer) { return; }

        isReady = !isReady;

        if (isReady) 
        {
            rdyTxt.text = "Ready!";
        }
        else
        {
            rdyTxt.text = "Not Ready";
        }

        RdyBttnToggleServerRPC(isReady);

    }

    [Rpc(SendTo.Server, RequireOwnership = false)]
    private void RdyBttnToggleServerRPC(bool isReady, RpcParams rpcParams = default)
    {
        Debug.Log("From Rdy bttn RPC");
        _networkPlayers.UpdateReadyClient(rpcParams.Receive.SenderClientId, isReady);
    }

    private void LeaveBttnClick()
    {
        if(!IsServer)
        {
            QuitLobbyServerRPC();
        }
        else
        {
            foreach (PlayerInfoData playerdata in _networkPlayers._allConnectedPlayers)
            {
                if(playerdata._clientId != _myLocalClientID)
                {
                    KickUsersBttn(playerdata._clientId);
                }
            }
            NetworkManager.Shutdown();
            SceneManager.LoadScene(0);
        }
    }

    [Rpc(SendTo.Server, RequireOwnership = false)]
    private void QuitLobbyServerRPC(RpcParams rpcParams=default)
    {
        KickUsersBttn(rpcParams.Receive.SenderClientId);
    }

    private void NetPlayersChanged(NetworkListEvent<PlayerInfoData> changeEvent)
    {
        Debug.Log("Net players has changed!");
        PopulateLabels();
    }

    [ContextMenu("PopulateLabel")]
    private void PopulateLabels()
    {
        ClearPlayerPanel();

        bool allReady = true;

        foreach(PlayerInfoData playerData in _networkPlayers._allConnectedPlayers)
        {
            GameObject newPlayerPanel = Instantiate(panelPrefab, _contentGO.transform);
            PlayerLabel _playerLabel = newPlayerPanel.GetComponent<PlayerLabel>();

            _playerLabel.OnKickClicked += KickUsersBttn;

            if(IsServer && playerData._clientId != _myLocalClientID)
            {
                _playerLabel.SetKickActive(true);
            }
            else
            {
                _playerLabel.SetKickActive(false);
            }

            _playerLabel.SetPLayerLabelName(playerData._clientId);
            _playerLabel.SetReady(playerData._isPlayerReady);
            _playerLabel.SetPlayerColor(playerData._colorId);
            _playerPanels.Add(newPlayerPanel);

            if(playerData._isPlayerReady == false) 
            {
                allReady = false;
            }
        }

        if(IsServer) 
        {
            if(allReady)
            {
                if(_networkPlayers._allConnectedPlayers.Count > 1)
                {
                    rdyTxt.text = "Ready to start";
                    _startBttn.gameObject.SetActive(true);
                }
                else
                {
                    rdyTxt.text = "Empty lobby";
                }
            }
            else
            {
                _startBttn.gameObject.SetActive(false);
                rdyTxt.text = "waiting for ready players";
            }
        }
        else
        {
            _startBttn.gameObject.SetActive(false);
        }
    }

    private void KickUsersBttn(ulong kickTarget)
    {
        if (!IsServer || !IsHost)
        {
            return;
        }

        foreach (PlayerInfoData playerData in _networkPlayers._allConnectedPlayers)
        {
            if(playerData._clientId == kickTarget)
            {
                KickedClientRpc(RpcTarget.Single(kickTarget, RpcTargetUse.Temp));

                NetworkManager.Singleton.DisconnectClient(kickTarget);
            }
        }
    }

    [Rpc(SendTo.SpecifiedInParams)]
    private void KickedClientRpc(RpcParams rpcParams)
    {
        SceneManager.LoadScene(0);
    }

    private void ClearPlayerPanel()
    {
        foreach(GameObject panel in _playerPanels)
        {
            Destroy(panel);
        }

        _playerPanels.Clear();
    }
}
