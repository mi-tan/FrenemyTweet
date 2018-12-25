using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inhole : MonoBehaviour
{
    private List<Rigidbody> Enemys = new List<Rigidbody>();


    void Awake()
    {
        Destroy(gameObject, 3f);
    }

    void FixedUpdate()
    {
        foreach(Rigidbody rb in Enemys)
        {
            if(!rb) { continue; }

            float distance = 0f;
            distance = Vector3.Distance(transform.position, rb.transform.position);

            rb.velocity = (-(rb.transform.position - transform.position).normalized * 20 * distance / rb.mass);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("範囲内の敵を追加");
        Enemys.Add(other.GetComponent<Rigidbody>());
    }

    void OnTriggerExit(Collider other)
    {
        for(int i = 0; i < Enemys.Count; i++)
        {
            if (Enemys[i] == other.GetComponent<Rigidbody>())
            {
                Enemys.RemoveAt(i);
            }
        }
    }
}
