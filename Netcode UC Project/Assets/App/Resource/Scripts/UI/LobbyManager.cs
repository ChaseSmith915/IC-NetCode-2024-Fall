using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : NetworkBehaviour
{
    [SerializeField] private Button _startBttn, _leaveBttn, _readyBttn;
    [SerializeField] private GameObject panelPrefab;
    [SerializeField] private GameObject _contentGO;
    [SerializeField] private TMP_Text rdyTxt;

    [SerializeField] private NetworkedPlayerData _networkPlayers;


    private List<GameObject> _playerPanels = new List<GameObject>();

    private ulong _myServerID;

    private bool isReady = false;

    private void Start()
    {
        _myServerID = NetworkManager.ServerClientId;

        if(ServerIsHost)
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
        throw new NotImplementedException();
    }

    private void LeaveBttnClick()
    {
        throw new NotImplementedException();
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

            if(IsServer && playerData._clientId != _myServerID)
            {
                _playerLabel.SetKickActive(true);
                _readyBttn.gameObject.SetActive(false);
            }
            else
            {
                _playerLabel.SetKickActive(false);
                _readyBttn.gameObject.SetActive(true);
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
        }
        else
        {
            _startBttn.gameObject.SetActive(false);
            rdyTxt.text = "waiting for ready players";
        }
    }

    private void KickUsersBttn(ulong obj)
    {
        throw new NotImplementedException();
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
