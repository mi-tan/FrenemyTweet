using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paste : MonoBehaviour
{
    private bool isPaste = false;
    private Light pointLight;


    private void Awake()
    {
        pointLight = GetComponent<Light>();
        StartCoroutine(Blink());
        StartCoroutine(Explode());
    }

    private IEnumerator Blink()
    {
        pointLight.enabled = false;

        yield return new WaitForSeconds(0.1f);

        pointLight.enabled = true;

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

        Vector3 scale = other.gameObject.transform.localScale;
        transform.parent = other.gameObject.transform;
        Vector3 size = new Vector3(
            transform.localScale.x / scale.x,
            transform.localScale.y / scale.y,
            transform.localScale.z / scale.z);
        transform.localScale = size;
    }
}
