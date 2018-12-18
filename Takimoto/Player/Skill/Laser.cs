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

    public override void ActivateSkill()
    {
        Instantiate(laserPrefab);
    }
}
