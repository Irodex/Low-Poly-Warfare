using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using UnityEngine.UI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;
using System;
using MLAPI.NetworkVariable.Collections;

public class MP_ChatScript : NetworkBehaviour
{
    public GameObject chatUI = null;
    public Text chatText = null;
    public InputField inputField = null;
    

    NetworkVariableString messages = new NetworkVariableString("Temp");

    public NetworkList<MP_PlayerInfo> chatPlayers;

    public string pubMsg = "";

    private string localPlayerName = "NA";
    void Start()
    {
        messages.OnValueChanged += updateUIClientRpc;
        foreach (MP_PlayerInfo connectedplayer in chatPlayers)
        {
            if (NetworkManager.LocalClientId == connectedplayer.networkClientID)
            {
                localPlayerName = connectedplayer.networkPlayerName;
            }
        }
    }
    void Update()
    {
        pubMsg = messages.Value;
       // Debug.Log("Owner: " + NetworkObject.OwnerClientId);

    }

    public void handleSend()
    {
       

        if (!IsServer)
        {

            Debug.Log("Send Clicked");

            NewMessageServerRpc(" says :"+inputField.text);
        }
        else
        {
            messages.Value += "\n" + localPlayerName + " says :" + inputField.text;
        }


    }
    [ServerRpc]
    public void NewMessageServerRpc(string msg, ServerRpcParams serverRpcParams = default)
    {
        Debug.Log("hi"+ msg);
        foreach (MP_PlayerInfo connectedplayer in chatPlayers)
        {
            if (serverRpcParams.Receive.SenderClientId == connectedplayer.networkClientID)
            {
                localPlayerName = connectedplayer.networkPlayerName;
            }
        }
        messages.Value += "\n" + localPlayerName + msg;
    }
    [ClientRpc]
    private void updateUIClientRpc(string previousValue, string newValue)
    {
        chatText.text += "\n" + newValue.Substring(previousValue.Length, newValue.Length - previousValue.Length);


    }
}

