using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[CreateAssetMenu(menuName = "ScriptableObject/Skill/Recovery")]
public class Recovery : PlayerSkillBase
{
    //[Header("スキルの攻撃力")]
    //[SerializeField]
    //private int skillAttackPower = 100;

    //[Header("消去時間")]
    //[SerializeField]
    //private float destroyTime = 2.4f;

    //private float shakeTime = 2f;
    //private float shakeX = 0.06f;
    //private float shakeY = 0.06f;

    const string PREFAB_NAME = "RecoveryPrefab";


    public override void ActivateSkill(PlayerProvider playerProvider, Vector3 skillCreationPos)
    {
        //Debug.Log("分身生成");
        Vector3 pos =
            playerProvider.transform.position +
            playerProvider.transform.right * skillCreationPos.x +
            playerProvider.transform.up * skillCreationPos.y +
            playerProvider.transform.forward * skillCreationPos.z;

        AttackCollision attackCollision =
            PhotonNetwork.Instantiate(PREFAB_NAME, pos, playerProvider.transform.rotation).GetComponent<AttackCollision>();

        // カメラを揺らす
        //playerCamera.ShakeCamera(shakeTime, shakeX, shakeY);
    }
}
