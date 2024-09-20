using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

<<<<<<< HEAD
public class ConnectionManager : NetworkBehaviour
{
    [SerializeField] private Button _serverBttn, _clientBttn, _hostBttn, _startBttn;
    [SerializeField] private GameObject _connectionBttnGroup;
    [SerializeField] private SpawnController _mySpawnController;
=======
public class ConnectionManager : MonoBehaviour
{
    [SerializeField] private Button _serverBttn, _clientBttn, _hostBttn;
>>>>>>> c5a9ab3552d92cf8ee81e36e3a2477d05d40579f

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        _startBttn.gameObject.SetActive(false);
        _serverBttn.onClick.AddListener(ServerClick);
        _hostBttn.onClick.AddListener(HostClick);
        _clientBttn.onClick.AddListener(ClientClick);
        _startBttn.onClick.AddListener(StartClick);
=======
        _serverBttn.onClick.AddListener(ServerClick);
        _hostBttn.onClick.AddListener(HostClick);
        _clientBttn.onClick.AddListener(ClientClick);
>>>>>>> c5a9ab3552d92cf8ee81e36e3a2477d05d40579f
    }

    // Update is called once per frame
    void Update()
    {
        
    }

<<<<<<< HEAD
    private void StartClick()
    {
        if(IsServer)
        {
            _mySpawnController.SpawnAllPlayers();
            _startBttn.gameObject.SetActive(false);
            _startBttn.gameObject.SetActive(true);
        }
    }

    private void ServerClick()
    {
        NetworkManager.Singleton.StartHost();
        _connectionBttnGroup.SetActive(false);
=======
    private void ServerClick()
    {
        NetworkManager.Singleton.StartHost();
        this.gameObject.SetActive(false);
>>>>>>> c5a9ab3552d92cf8ee81e36e3a2477d05d40579f
    }

    private void ClientClick()
    {
        NetworkManager.Singleton.StartClient();
<<<<<<< HEAD
        _connectionBttnGroup.SetActive(false);
=======
        this.gameObject.SetActive(false);
>>>>>>> c5a9ab3552d92cf8ee81e36e3a2477d05d40579f
    }

    private void HostClick()
    {
        NetworkManager.Singleton.StartHost();
<<<<<<< HEAD
        _connectionBttnGroup.SetActive(false);
        _startBttn.gameObject.SetActive(true);
=======
        this.gameObject.SetActive(false);
>>>>>>> c5a9ab3552d92cf8ee81e36e3a2477d05d40579f
    }
}
