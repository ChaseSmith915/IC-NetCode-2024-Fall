using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;


public class ConnectionManager : NetworkBehaviour
{
    [SerializeField] private Button _serverBttn, _clientBttn, _hostBttn, _startBttn;
    [SerializeField] private GameObject _connectionBttnGroup;
    [SerializeField] private SpawnController _mySpawnController;

    void Start()
    {
        _startBttn.gameObject.SetActive(false);
        _serverBttn.onClick.AddListener(ServerClick);
        _hostBttn.onClick.AddListener(HostClick);
        _clientBttn.onClick.AddListener(ClientClick);
        _startBttn.onClick.AddListener(StartClick);
    }
    private void StartClick()
    {
        if(IsServer)
        {
            _mySpawnController.SpawnAllPlayers();
            _startBttn.gameObject.SetActive(false);
        }
    }

    private void ServerClick()
    {
        NetworkManager.Singleton.StartHost();
        _connectionBttnGroup.SetActive(false);
        _startBttn.gameObject.SetActive(true);
    }


    private void ClientClick()
    {
        NetworkManager.Singleton.StartClient();
        _connectionBttnGroup.SetActive(false);
    }

    private void HostClick()
    {
        NetworkManager.Singleton.StartHost();
        _connectionBttnGroup.SetActive(false);
        _startBttn.gameObject.SetActive(true);
    }
}
