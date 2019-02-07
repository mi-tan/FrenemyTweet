using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundedUp : MonoBehaviour
{
    [SerializeField]
    private float power = 12f;

    private Vector3 pos;

    void OnTriggerEnter(Collider other)
    {
        string layerName = LayerMask.LayerToName(other.gameObject.layer);

        if(layerName != "Enemy") { return; }

        Rigidbody rb = other.GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * power, ForceMode.VelocityChange);
        //Debug.Log("飛ばす");
    }
}
