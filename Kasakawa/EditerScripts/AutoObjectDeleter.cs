using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

/// <summary>
/// アタッチされたゲームオブジェクトを開始時に削除
/// </summary>
public sealed class AutoObjectDeleter : MonoBehaviour {

    [SerializeField]
    private float waitTime = 0f;

    [SerializeField]
    private bool isPhoton = false;

	// Use this for initialization
	IEnumerator Start () {

        if (isPhoton)
        {
            //GetComponent<PhotonView>();
            yield return new WaitForSeconds(waitTime);
            PhotonNetwork.Destroy(GetComponent<PhotonView>());
            yield break;
        }
        Destroy(gameObject, waitTime);
	}
}
