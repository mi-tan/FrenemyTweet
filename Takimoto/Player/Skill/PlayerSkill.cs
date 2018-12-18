using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    /// <summary>
    /// スキル数
    /// </summary>
    const int SKILL_QUANTITY = 3;
    /// <summary>
    /// スキル番号
    /// </summary>
    private int skillNumber = 0;

    [SerializeField]
    private PlayerSkillBase[] skillList = new PlayerSkillBase[SKILL_QUANTITY];

    /// <summary>
    /// 入力中か
    /// </summary>
    private bool isInput = false;

    public int SkillNumber
    {
        get
        {
            return skillNumber;
        }
    }

    public PlayerSkillBase GetSelectSkill()
    {
        return skillList[SkillNumber];
    }

    public void UpdateSkill(float inputAttack)
    {
        if (inputAttack >= 1)
        {
            if (isInput) { return; }

            // スキル発動
            skillList[SkillNumber].ActivateSkill();

            isInput = true;
        }
        else
        {
            isInput = false;
        }
    }
}
