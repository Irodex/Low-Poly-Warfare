using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class ChangeAmmo : NetworkBehaviour
{

    public GameObject mgammo;
    public GameObject pistolammo;
    public GameObject sniperammo;
    public GameObject rocketammo;
    public GameObject uziammo;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChangeBulletTypeServerRpc();
    }

   [ServerRpc]
    public void ChangeBulletTypeServerRpc()
    {
        mgammo.SetActive(false);
        rocketammo.SetActive(false);
        pistolammo.SetActive(false);
        uziammo.SetActive(false);
        sniperammo.SetActive(false);
        if (GetComponent<MP_PlayerAttribs>().weaponEquipped == "MachineGun")
        {
            mgammo.SetActive(true);

        }
        if (GetComponent<MP_PlayerAttribs>().weaponEquipped == "Pistol")
        {
            pistolammo.SetActive(true);
        }
        if (GetComponent<MP_PlayerAttribs>().weaponEquipped == "Uzi")
        {
            uziammo.SetActive(true);
        }
        if (GetComponent<MP_PlayerAttribs>().weaponEquipped == "Sniper")
        {
            sniperammo.SetActive(true);
        }
        if (GetComponent<MP_PlayerAttribs>().weaponEquipped == "RocketLauncher")
        {
            rocketammo.SetActive(true);
        }
    }
}
