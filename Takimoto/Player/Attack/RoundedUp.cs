using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundedUp : MonoBehaviour
{
    public List<Rigidbody> enemys = new List<Rigidbody>();

    private float gravityPower = 700f;

    private Vector3 pos;

    private float time = 0f;
    const float ROUNDED_UP_TIME = 1f;


    public void SetPosition(Vector3 pos)
    {
        this.pos = pos;
        time = 0f;
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;

        if(time < ROUNDED_UP_TIME)
        {
            foreach (Rigidbody rb in enemys)
            {
                if (!rb) { continue; }

                float distance = 0f;
                distance = Vector3.Distance(pos, rb.transform.position);

                rb.velocity = (-(rb.transform.position - (pos)).normalized * gravityPower * distance / rb.mass);

                // 小さくして消す
                //float a = rb.gameObject.transform.localScale.x * distance;
                //a = Mathf.Clamp(a, 0, 1);
                //rb.gameObject.transform.localScale = new Vector3(a, a, a);
                //if (a == 0) { Destroy(rb.gameObject); }
            }
        }
        else
        {
            enemys = new List<Rigidbody>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        bool isOverlap = false;

        for (int i = 0; i < enemys.Count; i++)
        {
            if (enemys[i] == other.GetComponent<Rigidbody>())
            {
                isOverlap = true;
            }
        }

        if(isOverlap) { return; }

        enemys.Add(other.GetComponent<Rigidbody>());
    }
}
