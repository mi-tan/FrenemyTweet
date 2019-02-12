using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        if (sound == null)
        {
            Debug.LogWarning("Soundの設定がされていません");
            return;
        }

        audioSource.PlayOneShot(sound);
        Debug.Log("音再生①");
    }

    public void PlaySound(AudioClip sound, float volume)
    {
        if (sound == null)
        {
            Debug.LogWarning("Soundの設定がされていません");
            return;
        }
        if (volume == 0)
        {
            Debug.Log("Soundのボリュームが０です");
            return;
        }

        audioSource.PlayOneShot(sound, volume);
        Debug.Log("音再生①");
    }
}
