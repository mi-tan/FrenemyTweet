using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paste : MonoBehaviour
{
    private bool isPaste = false;
    private Light pointLight;
    private Transform pasteObject;
    private Vector3 offset = Vector3.zero;

    private float explosionTime = 3f;

    [SerializeField]
    private GameObject explosion;

    private AudioSource audioSource;

    private bool explode = false;


    private void Awake()
    {
        pointLight = GetComponent<Light>();
        StartCoroutine(Blink());
        StartCoroutine(Explode());

        audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator Blink()
    {
        pointLight.enabled = false;

        yield return new WaitForSeconds(0.1f);

        if (!explode)
        {
            pointLight.enabled = true;
            if (audioSource)
            {
                audioSource.Play();
            }
        }

        yield return new WaitForSeconds(0.3f);

        if (!explode)
        {
            StartCoroutine(Blink());
        }
    }


    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(explosionTime);

        explode = true;
        if (audioSource)
        {
            audioSource.Stop();
        }

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
