using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollision : MonoBehaviour
{
    private BoxCollider col;

    private float time = 0f;

    private float startTime = 0.3f;
    private bool start = false;

    private float largeTime = 0.5f;
    private float largeSpeed = 5f;

    private float size = 0.1f;
    private float distance = 27f;

    private float destroyTime = 2.2f;
    private bool destroy = false;

    private float intervalTime = 0.2f;


    private void Awake()
    {
        col = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        col.enabled = false;
        col.size = new Vector3(size, size, distance);
    }

    private void Update()
    {
        col.size = new Vector3(size, size, distance);

        time += Time.deltaTime;

        if (time >= startTime)
        {
            if (!start)
            {
                start = true;
                StartCoroutine(Collider());
            }

            if (time < largeTime)
            {
                size += Time.deltaTime * largeSpeed;
            }
        }

        size = Mathf.Clamp(size, 0.1f, 1f);

        if (time >= destroyTime)
        {
            destroy = true;
        }
    }

    private IEnumerator Collider()
    {
        col.enabled = true;

        yield return new WaitForSeconds(intervalTime);

        col.enabled = false;

        if (!destroy)
        {
            StartCoroutine(Collider());
        }
    }
}
