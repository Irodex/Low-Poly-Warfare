using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MP_PlayerStats : NetworkBehaviour
{
    public Slider hpBar;
    public float maxHp = 100f;
    public float damageVal = 20f;

    private NetworkVariableFloat currentHp = new NetworkVariableFloat(100f);
    public NetworkVariableBool powerUp = new NetworkVariableBool(false);

    // Update is called once per frame
    void Update()
    {
        hpBar.value = currentHp.Value / maxHp;
    }

   
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && IsOwner)
        {
            Debug.Log("Ow!");
            TakeDamageServerRpc(damageVal);
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("MedKit") && IsOwner)
        {
            Debug.Log("You Healed!");
            HealDamageServerRpc();
        }
        else if(collision.gameObject.CompareTag("PowerUp") && IsOwner)
        {
            damageVal = damageVal / 2; ;
            Debug.Log("PowerUP! Damage taken reduced");
            powerUp.Value = true;


        }
    }
    [ServerRpc]
    private void TakeDamageServerRpc(float damage)
    {

        currentHp.Value -= damage;
        if (currentHp.Value < 0)
        {
            Debug.Log("You Died!");
            Destroy(this.gameObject);
        }
    }
    [ServerRpc]
    private void HealDamageServerRpc()
    {

        currentHp.Value += 25f;
        if (currentHp.Value > maxHp)
        {
            Debug.Log("You Healed!");
            currentHp.Value = maxHp;
        }
    }




}
