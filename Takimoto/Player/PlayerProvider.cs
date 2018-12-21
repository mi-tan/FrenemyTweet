using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProvider : CharacterBase
{
    private PlayerDamage playerDamage;
    private PlayerParameter playerParameter;
    private PlayerSkill playerSkill;


    void Awake()
    {
        // コンポーネントを取得
        playerDamage = GetComponent<PlayerDamage>();
        playerParameter = GetComponent<PlayerParameter>();
        playerSkill = GetComponent<PlayerSkill>();
    }

    public override void TakeDamage(int damage)
    {
        playerDamage.Damage(damage);
    }

    public int GetHp()
    {
        return playerParameter.Hp;
    }

    public int GetMaxHp()
    {
        return playerParameter.MaxHp;
    }

    public int GetSkillNumber()
    {
        return playerSkill.SkillNumber;
    }

    public PlayerSkillBase[] GetSkillList()
    {
        return playerSkill.GetSkillList();
    }

    public PlayerSkillBase GetSelectSkill()
    {
        return playerSkill.GetSelectSkill();
    }
}
