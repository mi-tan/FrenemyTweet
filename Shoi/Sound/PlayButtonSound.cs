using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SoundManager))]
public class PlayButtonSound : MonoBehaviour {

    [SerializeField]
    private AudioClip clickAudio;
    [SerializeField]
    private AudioClip enterAudio;

    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = GetComponent<SoundManager>();
    }

    public void PlayClickSound()
    {
        soundManager.PlaySound(clickAudio);
    }

    public void PlayEnterSound()
    {
        soundManager.PlaySound(enterAudio);
    }

}
