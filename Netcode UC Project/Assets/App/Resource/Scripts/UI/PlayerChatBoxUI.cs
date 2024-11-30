using Unity.Netcode;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Collections;

//The line below is a workaround to prevent null reference spam when trying to update the network variable.
[GenerateSerializationForType(typeof(byte))]
public class PlayerChatBoxUI : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI chatBoxText;
    [SerializeField] private Button sendButton;
    [SerializeField] private TMP_InputField chatBoxInput;  
    [SerializeField] private NetworkedPlayerData _networkPlayers;

    private NetworkVariable<FixedString4096Bytes> text;

    private void Awake()
    {
        text = new NetworkVariable<FixedString4096Bytes>(readPerm: NetworkVariableReadPermission.Everyone);
    }

    void Start()
    {
        Debug.Log("start");
        chatBoxText.text = "";
        sendButton.onClick.AddListener(() => SendMessageToChat(chatBoxInput.text));
        text.OnValueChanged += UpdateTextUI;
        text.Value = "";
    }

    private void UpdateTextUI(FixedString4096Bytes previousValue, FixedString4096Bytes newValue)
    {
        chatBoxText.text = newValue.ToString();
    }

    private void SendMessageToChat(string message)
    {
        ChatMessageServerRpc(message);
    }

    private void UpdateText(string message, ulong clientID)
    {
        text.Value += $"\nPlayer {clientID}: {message}";
    }

    [Rpc(SendTo.Server, RequireOwnership = false)]
    private void ChatMessageServerRpc(string message, RpcParams rpcParams = default)
    {
        UpdateText(message, rpcParams.Receive.SenderClientId);
    }
}

