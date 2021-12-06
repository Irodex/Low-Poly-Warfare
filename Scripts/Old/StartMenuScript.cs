using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class StartMenuScript : MonoBehaviour
{
    public GameObject startMenu;
    public void HostButtonPressed()
    {
        NetworkManager.Singleton.StartHost();
        startMenu.SetActive(false);
    }
    public void ClientButtonPressed()
    {
        NetworkManager.Singleton.StartClient();
        startMenu.SetActive(false);
    }
}
