using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Skill/Laser")]
public class Laser : PlayerSkillBase
{
    [Header("攻撃力")]
    [SerializeField]
    private int attackPower = 2;
    /// <summary>
    /// 攻撃力
    /// </summary>
    public int AttackPower
    {
        get
        {
            return attackPower;
        }
    }


    public override void ActivateSkill(Transform playerTrans, Vector3 skillCreationPos)
    {
        //Debug.Log("レーザー生成");
        Vector3 pos = 
            playerTrans.position + 
            playerTrans.right * skillCreationPos.x +
            playerTrans.up * skillCreationPos.y +
            playerTrans.forward * skillCreationPos.z;

        Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(center);
        RaycastHit hit;

        Quaternion qua = new Quaternion();

        if (Physics.Raycast(ray, out hit, 1000.0f, LayerMask.GetMask(new string[] {"Field", "Enemy"})))
        {
            Vector3 vec = (hit.point - playerTrans.position).normalized;
            qua = Quaternion.LookRotation(vec);
        }
        else
        {
            Debug.LogWarning("Rayで照準位置が取得できませんでした");
        }

        Instantiate(skillPrefab, pos, qua);
    }
}
