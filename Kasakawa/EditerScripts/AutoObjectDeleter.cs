using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アタッチされたゲームオブジェクトを開始時に削除
/// </summary>
public sealed class AutoObjectDeleter : MonoBehaviour {

    [SerializeField]
    private float waitTime = 0f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, waitTime);
	}
}
