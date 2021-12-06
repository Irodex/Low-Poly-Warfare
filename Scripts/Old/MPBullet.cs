using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class MPBullet : NetworkBehaviour
{
    private float bulletSpeed = 50f;
    public Rigidbody bullet;
    public Transform bulletSpawnLocation;

    [ServerRpc]
    public void FireServerRpc()
    {
        Rigidbody bulletClone = Instantiate(bullet, transform.position, Quaternion.identity) as Rigidbody;
        bulletClone.velocity = transform.forward * bulletSpeed;
        bulletClone.gameObject.GetComponent<NetworkObject>().Spawn();
        Destroy(bulletClone.gameObject, 3);
        
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && IsLocalPlayer)
        {
            FireServerRpc();
        }

    }
}
