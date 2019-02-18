using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inhole : MonoBehaviour
{
    private List<Rigidbody> enemys = new List<Rigidbody>();

    [SerializeField]
    private float gravityPower = 100f;

    [SerializeField]
    private float destroyTime = 2.5f;

    private AudioSource[] audioSources;

    private float time = 0f;
    private float muteTime = 2f;


    void Awake()
    {
        Destroy(gameObject, destroyTime);
        audioSources = GetComponents<AudioSource>();
    }

    void FixedUpdate()
    {
        foreach (Rigidbody rb in enemys)
        {
            if (!rb) { continue; }

            float distance = 0f;
            distance = Vector3.Distance(transform.position, rb.transform.position);

            rb.velocity = (-(rb.transform.position - transform.position).normalized * gravityPower * distance / rb.mass);

            // 小さくして消す
            //float a = rb.gameObject.transform.localScale.x * distance;
            //a = Mathf.Clamp(a, 0, 1);
            //rb.gameObject.transform.localScale = new Vector3(a, a, a);
            //if (a == 0) { Destroy(rb.gameObject); }
        }
    }

    private void Update()
    {
        if (audioSources.Length > 0)
        {
            time += Time.deltaTime;
            if (time >= muteTime)
            {
                foreach (AudioSource audioSource in audioSources)
                {
                    audioSource.volume -= Time.deltaTime * 3f;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("範囲内の敵を追加");
        enemys.Add(other.GetComponent<Rigidbody>());
    }

    void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < enemys.Count; i++)
        {
            if (enemys[i] == other.GetComponent<Rigidbody>())
            {
                enemys.RemoveAt(i);
            }
        }
    }
}
