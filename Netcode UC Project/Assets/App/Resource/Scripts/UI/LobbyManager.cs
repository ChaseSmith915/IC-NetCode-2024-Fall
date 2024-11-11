using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
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
        }
    }

    private void KickUsersBttn(ulong obj)
    {
        throw new NotImplementedException();
    }

    private void ClearPlayerPanel()
    {
        throw new NotImplementedException();
    }
}
