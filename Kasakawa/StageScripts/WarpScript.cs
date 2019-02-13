using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpScript : MonoBehaviour {

    [SerializeField]
    private Transform warpEndPoint;

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーに当たった場合
        if(other.gameObject.layer == (int)LayerManager.Layer.Player)
        {
            other.transform.position = warpEndPoint.position;
        }
    }
}
