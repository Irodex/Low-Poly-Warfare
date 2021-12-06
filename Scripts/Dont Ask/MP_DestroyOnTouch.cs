using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP_DestroyOnTouch : NetworkBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Touched"+ collision.gameObject.tag);
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
