using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MP_PlayerAttribs : NetworkBehaviour
{
    public Slider hpBar;

    private float maxHp = 100f;
    private float damageVal;

    private NetworkVariableFloat currentHp = new NetworkVariableFloat(100f);
    public NetworkVariableBool powerUp = new NetworkVariableBool(false);

    public NetworkVariableInt deaths = new NetworkVariableInt(0);
    public NetworkVariableInt kills = new NetworkVariableInt(0);


    public MPBulletSpawner bulletScript;
    public GameObject machineGun;
    public GameObject pistol;
    public GameObject sniper;
    public GameObject rocketLauncher;
    public GameObject uzi;
    public Rigidbody rb;
    public CapsuleCollider coll;
    public GameObject gunContainer;
    public bool hasWeapon;
    public string weaponEquipped;

    public GameObject bulletC;

    
    public AudioClip noiseToPlay;



    private void Start()
    {
        bulletScript.enabled = false;
       
        
    
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = currentHp.Value / maxHp;
       
        

        if (currentHp.Value < 0)
        {
            RespawnPlayerServerRpc();
            ResetPlayerClientRpc();
            if (IsOwner)
            {
                Debug.Log("You dead");
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && IsOwner)
        {
            bulletC = collision.gameObject;
            if (collision.gameObject.GetComponent<MP_BulletScript>().spawnerPlayerId != OwnerClientId)
            {
                
                if (currentHp.Value - damageVal < 0)
                {
                    IncreaseKillCountServerRpc(collision.gameObject.GetComponent<MP_BulletScript>().spawnerPlayerId);
                }
                TakeDamageServerRpc(damageVal);
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("MedKit") && IsOwner)
        {
            Debug.Log("I am Healed");
            HealDamageServerRpc();
        }
        else if (collision.gameObject.CompareTag("PowerUp") && IsOwner)
        {
            Debug.Log("I have the power!");
            damageVal = damageVal / 2;
            powerUp.Value = true;
        }

        //define if a certain weapon is picked up

        else if (collision.gameObject.CompareTag("MachineGun") && IsOwner)
        {
            EmptySlots();
            //enable bullet script
            bulletScript.enabled = true;
            //enable machine gun on play model
            machineGun.SetActive(true);
           


            machineGun.transform.localPosition = Vector3.zero;
            machineGun.transform.localRotation = Quaternion.Euler(Vector3.zero);
            machineGun.transform.localScale = Vector3.one;

            rb.isKinematic = true;
            coll.isTrigger = true;
            hasWeapon = true;


            //Change bullet icon and sound
            weaponEquipped = "MachineGun";
     
          

            //change mag and reload

            GetComponent<MPBulletSpawner>().bulletSpeed = 25f;
            damageVal = 20f;
            GetComponent<MPBulletSpawner>().delayTime = 0.5f;
        }

        //pistol
        else if (collision.gameObject.CompareTag("Pistol") && IsOwner)
        {
            EmptySlots();
            //enable bullet script
            bulletScript.enabled = true;
            //enable machine gun on play model
            pistol.SetActive(true);

            pistol.transform.localPosition = Vector3.zero;
            pistol.transform.localRotation = Quaternion.Euler(Vector3.zero);
            pistol.transform.localScale = Vector3.one;

            rb.isKinematic = true;
            coll.isTrigger = true;
            hasWeapon = true;
            //Change bullet icon
            weaponEquipped = "Pistol";
        
            //change mag and reload

            GetComponent<MPBulletSpawner>().bulletSpeed = 10f;
            damageVal = 15f;
            GetComponent<MPBulletSpawner>().delayTime = 0.75f;
        }


        //uzi
        else if (collision.gameObject.CompareTag("Uzi") && IsOwner)
        {
            EmptySlots();
            //enable bullet script
            bulletScript.enabled = true;
            //enable machine gun on play model
            uzi.SetActive(true);

            uzi.transform.localPosition = Vector3.zero;
            uzi.transform.localRotation = Quaternion.Euler(Vector3.zero);
            uzi.transform.localScale = Vector3.one;

            rb.isKinematic = true;
            coll.isTrigger = true;
            hasWeapon = true;
            //Change bullet icon
            weaponEquipped = "Uzi";
      
            //change mag and reload

            GetComponent<MPBulletSpawner>().bulletSpeed = 40f;
            damageVal = 10f;
            GetComponent<MPBulletSpawner>().delayTime = 0.25f;
        }


        //sniper
        else if (collision.gameObject.CompareTag("Sniper") && IsOwner)
        {
            EmptySlots();
            //enable bullet script
            bulletScript.enabled = true;
            //enable machine gun on play model
            sniper.SetActive(true);

            sniper.transform.localPosition = Vector3.zero;
            sniper.transform.localRotation = Quaternion.Euler(Vector3.zero);
            sniper.transform.localScale = Vector3.one;

            rb.isKinematic = true;
            coll.isTrigger = true;
            hasWeapon = true;
            //Change bullet icon
            weaponEquipped = "Sniper";
 
            //change mag and reload
            GetComponent<MPBulletSpawner>().bulletSpeed = 50f;
            damageVal = 30f;
            GetComponent<MPBulletSpawner>().delayTime = 10;
        }


        //rLauncher
        else if (collision.gameObject.CompareTag("RocketLauncher") && IsOwner)
        {
            EmptySlots();
            //enable bullet script
            bulletScript.enabled = true;
            //enable machine gun on play model
            rocketLauncher.SetActive(true);

            rocketLauncher.transform.localPosition = Vector3.zero;
            rocketLauncher.transform.localRotation = Quaternion.Euler(Vector3.zero);
            rocketLauncher.transform.localScale = Vector3.one;

            rb.isKinematic = true;
            coll.isTrigger = true;
            hasWeapon = true;
            //Change bullet icon
            weaponEquipped = "RocketLauncher";
   
            //change mag and reload

            //change bullet speed
            GetComponent<MPBulletSpawner>().bulletSpeed = 5f;
            damageVal = 25f;
            GetComponent<MPBulletSpawner>().delayTime = 6;
        }


    }
 


    public void EmptySlots()
    {
        machineGun.SetActive(false);
        pistol.SetActive(false);
        sniper.SetActive(false);
        uzi.SetActive(false);
        rocketLauncher.SetActive(false);
    }




    

  
    
    [ServerRpc]
    private void TakeDamageServerRpc(float damage, ServerRpcParams svrParams = default)
    {
       

        currentHp.Value -= damage;
        if(currentHp.Value < 0 && OwnerClientId == svrParams.Receive.SenderClientId)
        {
            deaths.Value++;
        }
    }

    [ServerRpc]
    private void HealDamageServerRpc()
    {
        currentHp.Value += 25f;
        if(currentHp.Value > maxHp)
        {
            currentHp.Value = maxHp;
        }

    }


    [ServerRpc]

    private void RespawnPlayerServerRpc()
    {
        //set health to 100%
        currentHp.Value = maxHp;
    }


    [ClientRpc]
    private void ResetPlayerClientRpc()
    {
        //reset player location to spawn point
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        int index = UnityEngine.Random.Range(0, spawnPoints.Length);

        GetComponent<CharacterController>().enabled = false;
        transform.position = spawnPoints[index].transform.position;
        GetComponent<CharacterController>().enabled = true;


    }

    [ServerRpc]
    private void IncreaseKillCountServerRpc(ulong spawnerPlayerId)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject playerObj in players)
        {
            if(playerObj.GetComponent<NetworkObject>().OwnerClientId == spawnerPlayerId)
            {
                playerObj.GetComponent<MP_PlayerAttribs>().kills.Value++;
            }
        }
    }

}

