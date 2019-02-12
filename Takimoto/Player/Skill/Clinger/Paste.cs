using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paste : MonoBehaviour
{
    private bool isPaste = false;
    private Light pointLight;
    private Transform pasteObject;
    private Vector3 offset = Vector3.zero;

    [SerializeField]
    private GameObject explosion;


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

        GameObject e = Instantiate(explosion, transform.position, transform.rotation);

        yield return new WaitForSeconds(0.2f);

        e.GetComponent<Collider>().enabled = false;

        Destroy(e, 1f);
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (isPaste) { return; }

        isPaste = true;

        pasteObject = other.gameObject.transform;
        offset = transform.position - pasteObject.transform.position;
    }

    private void Update()
    {
        if (!isPaste) { return; }

        if (pasteObject != null)
        {
            Vector3 newPos = transform.position;
            newPos = pasteObject.transform.position + offset;
            transform.position = newPos;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
