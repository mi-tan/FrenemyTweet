using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitPlayeSound : MonoBehaviour {

    [SerializeField]
    private AudioClip audioClip;
    [SerializeField]
    private float waitTime = 0;

    /// <summary>
    /// 経過時間
    /// </summary>
    private float elapsedTime;
    private bool playOneShot = false;
    private AudioSource audioSource;



    private void Start () {
        audioSource = GetComponent<AudioSource>();

    }
	
	private void Update () {

        elapsedTime += Time.deltaTime;

        if (elapsedTime < waitTime) { return; }
        elapsedTime = 0;

        if (playOneShot == false)
        {
            audioSource.PlayOneShot(audioClip);
            playOneShot = true;
        }
    }
}
