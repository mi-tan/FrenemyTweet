using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Skill/Clone")]
public class Clone : PlayerSkillBase
{
    //[Header("スキルの攻撃力")]
    //[SerializeField]
    //private int skillAttackPower = 100;

    //[Header("消去時間")]
    //[SerializeField]
    //private float destroyTime = 2.4f;

    private float shakeTime = 2f;
    private float shakeX = 0.06f;
    private float shakeY = 0.06f;


    public override void ActivateSkill(PlayerProvider playerProvider, Vector3 skillCreationPos)
    {
        //Debug.Log("分身生成");
        Vector3 pos =
            playerProvider.transform.position +
            playerProvider.transform.right * skillCreationPos.x +
            playerProvider.transform.up * skillCreationPos.y +
            playerProvider.transform.forward * skillCreationPos.z;

        AttackCollision attackCollision = Instantiate(skillPrefab, pos, playerProvider.transform.rotation);
        CreateClone createClone = attackCollision.GetComponent<CreateClone>();
        createClone.Create((int)playerProvider.GetWeapon());

        // カメラを揺らす
        //playerCamera.ShakeCamera(shakeTime, shakeX, shakeY);
    }
}
