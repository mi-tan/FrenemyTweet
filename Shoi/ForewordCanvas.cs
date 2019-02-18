using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ForewordCanvas : MonoBehaviour {

    [SerializeField]
    private AudioClip select;

    private Animator animator;
    private SoundManager soundManager;

    private string END_FLAG = "EndFlag";
    private bool oneClickFlag = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        soundManager = GetComponent<SoundManager>();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            NextCanvas();
        }
    }

    public void NextCanvas()
    {
        if (oneClickFlag) { return; }
        oneClickFlag = true;

        soundManager.PlaySound(select);
        // アニメーションオン
        animator.SetBool(END_FLAG, true);
    }
}
