using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アタッチされたゲームオブジェクトを開始時に削除
/// </summary>
public class AutoObjectDeleter : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject);
	}
}
