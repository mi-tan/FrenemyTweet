using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollision : MonoBehaviour
{
    private Collider col;

    private float time = 0f;

    private float destroyTime = 2.3f;

    private bool destroy = false;


    private void Awake()
    {
        col = GetComponent<Collider>();
    }

    private void Start()
    {
        StartCoroutine(Collider());
    }

    private void Update()
    {
        time += Time.deltaTime;
        if(time >= destroyTime)
        {
            destroy = true;
        }
    }

    private IEnumerator Collider()
    {
        col.enabled = true;

        yield return new WaitForSeconds(0.2f);

        col.enabled = false;

        if (!destroy)
        {
            StartCoroutine(Collider());
        }
    }
}
