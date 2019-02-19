using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcquisitionPlayers : MonoBehaviour {

    private AttackArea attackArea;

    private AttackArea getAttackArea
    {
        get
        {
            if (attackArea == null)
            {
                attackArea = transform.parent.GetComponent<AttackArea>();
            }
            return attackArea;
        }
    }


    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.name);
        PlayerProvider playerProvider = other.GetComponent<PlayerProvider>();
        // プレイヤー以外をここで除外
        if (playerProvider == null) { return; }
        bool playerAddFlag = true;

        for (int i = 0; getAttackArea.GetAcquisitionPlayerList.Count > i; i++)
        {
            // もうすでに取得しているプレイヤーであれば除外
            if (getAttackArea.GetAcquisitionPlayerList[i].gameObject == other.gameObject)
            {
                playerAddFlag = false;
                break;
            }
        }

        if (playerAddFlag)
        {
            // 追加
            getAttackArea.AddAcquisitonPlayerList = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerProvider playerProvider = other.GetComponent<PlayerProvider>();
        // プレイヤー以外をここで除外
        if (playerProvider != null)
        {
            getAttackArea.RemoveAcquisitonPlayerList = other.gameObject;
        }
    }
}
