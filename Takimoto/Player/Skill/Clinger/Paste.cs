using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paste : MonoBehaviour
{
    private bool isPaste = false;
    private Light light;


    private void Awake()
    {
        light = GetComponent<Light>();
        StartCoroutine(Blink());
        StartCoroutine(Explode());
    }

    private IEnumerator Blink()
    {
        light.enabled = false;

        yield return new WaitForSeconds(0.1f);

        light.enabled = true;

        yield return new WaitForSeconds(0.3f);

        StartCoroutine(Blink());
    }


    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(5f);

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPaste) { return; }

        isPaste = true;

        transform.parent = other.gameObject.transform;
    }
}
