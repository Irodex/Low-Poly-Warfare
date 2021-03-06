using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeaponController : MonoBehaviour
{
    public MPBulletSpawner bulletScript;
    public Rigidbody rb;
    public CapsuleCollider coll;
    public Transform player, gunContainer, fpsCam;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;



    private void Start()
    {
        if (!equipped)
        {
            bulletScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
        }

        if (equipped)
        {
            bulletScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
        }
    }
    public void Update()
    {
        Vector3 distancetoPlayer = player.position - transform.position;
        if (!equipped && distancetoPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();


        if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;


        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        rb.isKinematic = true;
        coll.isTrigger = true;

        bulletScript.enabled = true;
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;


        transform.SetParent(null);

        rb.isKinematic = false;
        coll.isTrigger = false;


        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);

        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        bulletScript.enabled = false;
    }
}
