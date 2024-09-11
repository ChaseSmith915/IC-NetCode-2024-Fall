using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class ConnectionManager : MonoBehaviour
{
    [SerializeField] private Button _serverBttn, _clientBttn, _hostBttn;

    // Start is called before the first frame update
    void Start()
    {
        _serverBttn.onClick.AddListener(ServerClick);
        _hostBttn.onClick.AddListener(HostClick);
        _clientBttn.onClick.AddListener(ClientClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ServerClick()
    {
        NetworkManager.Singleton.StartHost();
        this.gameObject.SetActive(false);
    }

    private void ClientClick()
    {
        NetworkManager.Singleton.StartClient();
        this.gameObject.SetActive(false);
    }

    private void HostClick()
    {
        NetworkManager.Singleton.StartHost();
        this.gameObject.SetActive(false);
    }
}
