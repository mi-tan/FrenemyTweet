using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForewordCanvas : MonoBehaviour {

    private Animator animator;

    private string END_FLAG = "EndFlag";

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

	private void Update () {

        if (Input.anyKey)
        {
            // アニメーションオン
            animator.SetBool(END_FLAG, true);
        }
	}
}
