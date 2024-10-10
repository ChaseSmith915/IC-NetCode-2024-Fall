using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PlayerLabel : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerText;
    [SerializeField] private Button _kickButton;
    [SerializeField] private Image _readyStatusImage, _playerColorImage;

    public event Action<ulong> OnKickClicked;
    private ulong _clientID;


    private void OnEnable()
    {
        _kickButton.onClick.AddListener(BttnKick_Clicked);
    }

    public void SetPLayerLabelName(ulong playerName)
    {
        _clientID = playerName;
        _playerText.text = "Player" + playerName.ToString();
    }

    private void BttnKick_Clicked()
    {
        OnKickClicked?.Invoke(_clientID);
    }

    public void SetKickActive(bool isOn)
    {
        _kickButton.gameObject.SetActive(isOn);
    }

    public void SetReady(bool isReady)
    {
        if(isReady)
        {
            _readyStatusImage.color = Color.green;
        }
        else
        {
            
            _readyStatusImage.color = Color.red;
        }
    }

    public void SetPlayerColor(Color color)
    {
        _playerColorImage.color = color;
    }
}
