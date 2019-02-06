using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Skill/Clinger")]
public class Clinger : PlayerSkillBase
{
    [Header("スキルの攻撃力")]
    [SerializeField]
    private int skillAttackPower = 100;

    [Header("消去時間")]
    [SerializeField]
    private float destroyTime = 2.4f;

    private float shakeTime = 2f;
    private float shakeX = 0.06f;
    private float shakeY = 0.06f;


    public override void ActivateSkill(PlayerProvider playerProvider, Vector3 skillCreationPos)
    {
        //Debug.Log("レーザー生成");
        Vector3 pos =
            playerProvider.transform.position +
            playerProvider.transform.right * skillCreationPos.x +
            playerProvider.transform.up * skillCreationPos.y +
            playerProvider.transform.forward * skillCreationPos.z;

        Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2);
        Ray ray = playerProvider.GetMainCamera().ScreenPointToRay(center);
        RaycastHit hit;

        Quaternion qua = new Quaternion();

        if (Physics.Raycast(ray, out hit, 1000.0f, LayerMask.GetMask(new string[] {"Field"})))
        {
            Vector3 vec = (hit.point - pos).normalized;
            qua = Quaternion.LookRotation(vec);
        }
        else
        {
            Debug.LogWarning("Rayで照準位置が取得できていない(Clinger)");
        }

        AttackCollision attackCollision = Instantiate(skillPrefab, pos, qua);
        //AttackCollision attackCollision = Instantiate(skillPrefab, pos, playerTrans.rotation);
        // ダメージ計算
        //attackCollision.SetAttackPower = skillAttackPower + playerAttackPower;
        attackCollision.SetHitEffect = hitEffect;
        Destroy(attackCollision.gameObject, destroyTime);

        // カメラを揺らす
        //playerCamera.ShakeCamera(shakeTime, shakeX, shakeY);
    }
}
