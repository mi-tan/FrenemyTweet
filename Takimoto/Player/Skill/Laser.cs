using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Skill/Laser")]
public class Laser : PlayerSkillBase
{
    [SerializeField]
    private int attackPower = 2;

    [SerializeField]
    private AttackCollision laserPrefab;

    [SerializeField]
    private Vector3 createOffset;


    public override void ActivateSkill(Transform createTrans)
    {
        Debug.Log("レーザー生成");
        Instantiate(laserPrefab, createTrans.position + createOffset, createTrans.rotation);
    }
}
