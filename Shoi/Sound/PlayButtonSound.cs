using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SoundManager))]
public class PlayButtonSound : MonoBehaviour {

    [SerializeField]
    private AudioClip useAudio;

    private SoundManager soundManager;

    private void Awake()
    {
        soundManager = GetComponent<SoundManager>();
    }

    public void PlaySound()
    {
        soundManager.PlaySound(useAudio);
    }

}
