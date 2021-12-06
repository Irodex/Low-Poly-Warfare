using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using UnityEngine.UI;

public class MPBulletSpawner : NetworkBehaviour
{
    public Rigidbody bullet;
    public Transform bulletPos;
    public float bulletSpeed = 30f;
    public int reloadTime;
    public Text reloadDisplay;
    public bool canShoot;
    public float delayTime;
    public AudioClip audioToPlay;
    public AudioClip bulletNoise1;
    public AudioClip bulletNoise2;
    public AudioClip bulletNoise3;
    public AudioClip bulletNoise4;
    public AudioClip bulletNoise5;
    public AudioSource audioSource;

    IEnumerator CountdownToReload()
    {
        while (reloadTime > 0)
        {
            reloadDisplay.text = reloadTime.ToString();

            yield return new WaitForSeconds(1f);

            reloadTime--;
        }
    }
    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(delayTime);
        canShoot = true;
    }



    private void Start()
    {
        audioToPlay = GetComponent<MP_PlayerAttribs>().noiseToPlay;
        audioSource = GetComponent<AudioSource>();
    }



    // Update is called once per frame



    void Update()
    {
        if (Input.GetButtonDown("Fire1") && IsOwner && canShoot)
        {
            FireServerRpc(bulletSpeed, gameObject.GetComponent<MP_PlayerAttribs>().powerUp.Value);
            //FireServerRpc(bulletSpeed);
            
        }
    }
    [ServerRpc]
    private void FireServerRpc(float speed, bool powerUp, ServerRpcParams serverRpcParams = default)
    {
       
        Rigidbody bulletClone = Instantiate(bullet, bulletPos.position, transform.rotation);
        bulletClone.velocity = transform.forward * speed;
        bulletClone.GetComponent<MP_BulletScript>().spawnerPlayerId = serverRpcParams.Receive.SenderClientId;
        bulletClone.gameObject.GetComponent<NetworkObject>().Spawn();
        PlayShootingSound();
        Destroy(bulletClone.gameObject, 3);
        canShoot = false;
        StartCoroutine(ShootDelay());


        if (powerUp)
        {
            Vector3 temp = new Vector3(1, 0, 0);
            bulletPos.Translate(temp, bulletPos);

            bulletClone = Instantiate(bullet, bulletPos.position, transform.rotation);
            bulletClone.velocity = transform.forward * speed;
            bulletClone.GetComponent<MP_BulletScript>().spawnerPlayerId = serverRpcParams.Receive.SenderClientId;
            bulletClone.gameObject.GetComponent<NetworkObject>().Spawn();
            Destroy(bulletClone.gameObject, 3);


            temp = new Vector3(-2, 0, 0);
            bulletPos.Translate(temp, bulletPos);

            bulletClone = Instantiate(bullet, bulletPos.position, transform.rotation);
            bulletClone.velocity = transform.forward * speed;
            bulletClone.GetComponent<MP_BulletScript>().spawnerPlayerId = serverRpcParams.Receive.SenderClientId;
            bulletClone.gameObject.GetComponent<NetworkObject>().Spawn();
            Destroy(bulletClone.gameObject, 3);



            temp = new Vector3(1, 0, 0);
            bulletPos.Translate(temp, bulletPos);
        }
    }

    void PlayShootingSound()
    {
        if(GetComponent<MP_PlayerAttribs>().weaponEquipped == "MachineGun")
        {
            audioSource.clip = bulletNoise4;
            audioSource.Play();
        }
        if (GetComponent<MP_PlayerAttribs>().weaponEquipped == "Pistol")
        {
            audioSource.clip = bulletNoise2;
            audioSource.Play();
        }
        if (GetComponent<MP_PlayerAttribs>().weaponEquipped == "Uzi")
        {
            audioSource.clip = bulletNoise3;
            audioSource.Play();
        }
        if (GetComponent<MP_PlayerAttribs>().weaponEquipped == "Sniper")
        {
            audioSource.clip = bulletNoise1;
            audioSource.Play();
        }
        if (GetComponent<MP_PlayerAttribs>().weaponEquipped == "RocketLauncher")
        {
            audioSource.clip = bulletNoise5;
            audioSource.Play();
        }
    }
}
    


    



