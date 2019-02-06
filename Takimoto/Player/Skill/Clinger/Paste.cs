using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paste : MonoBehaviour
{
    private bool isPaste = false;
    private Light pointLight;
    private Transform pasteObject;
    private Vector3 offset = Vector3.zero;


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

        pasteObject = other.gameObject.transform;
        offset = transform.position - pasteObject.transform.position;
    }

    private void LateUpdate()
    {
        if(pasteObject == null) { return; }

        Vector3 newPos = transform.position;
        newPos = pasteObject.transform.position + offset;
        transform.position = newPos;
    }
}
