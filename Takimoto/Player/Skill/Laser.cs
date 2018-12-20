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


    public override void ActivateSkill(Transform playerTrans, Vector3 createPos)
    {
        //Debug.Log("レーザー生成");
        Vector3 pos = 
            playerTrans.position + 
            playerTrans.right * createPos.x +
            playerTrans.up * createPos.y +
            playerTrans.forward * createPos.z;
        Instantiate(skillPrefab, pos, playerTrans.rotation);
    }
}
