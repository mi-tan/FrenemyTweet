using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProvider : CharacterBase
{
    private PlayerDamage playerDamage;
    private PlayerParameter playerParameter;
    private PlayerSkill playerSkill;

    [SerializeField]
    private MeshRenderer faceMat;


    void Awake()
    {
        // コンポーネントを取得
        playerDamage = GetComponent<PlayerDamage>();
        playerParameter = GetComponent<PlayerParameter>();
        playerSkill = GetComponent<PlayerSkill>();
    }

    private void Start()
    {
        SetFaceTexture(TwitterParameterManager.Instance.IconTexture);
    }

    public override void TakeDamage(int damage)
    {
        playerDamage.Damage(damage);
    }

    public int GetHp()
    {
        return playerParameter.Hp;
    }

    public void SetHp(int value)
    {
        playerParameter.SetHp(value);
    }

    public int GetMaxHp()
    {
        return playerParameter.MaxHp;
    }

    public void SetMaxHp(int value)
    {
        playerParameter.SetMaxHp(value);
    }

    public int GetBasicAttackPower()
    {
        return playerParameter.BasicAttackPower;
    }

    public void SetBasicAttackPower(int value)
    {
        playerParameter.SetBasicAttackPower(value);
    }

    public int GetPlayerAttackPower()
    {
        return playerParameter.PlayerAttackPower;
    }

    public void SetPlayerAttackPower(int value)
    {
        playerParameter.SetPlayerAttackPower(value);
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

    public float[] GetSkillCoolTimes()
    {
        return playerSkill.GetSkillCoolTimes();
    }

    public float[] GetSkillBaseCoolTimes()
    {
        float[] skillBaseCoolTimes = new float[PlayerParameter.SKILL_QUANTITY];

        for(int i = 0; i < skillBaseCoolTimes.Length; i++)
        {
            if (GetSkillList()[i] == null) { continue; }

            skillBaseCoolTimes[i] = GetSkillList()[i].SkillCoolTime;
        }

        return skillBaseCoolTimes;
    }

    public void SetFaceTexture(Texture2D tex)
    {
        if (!tex) { return; }
        faceMat.material.EnableKeyword("_MainTex");
        faceMat.material.SetTexture("_MainTex", tex);
    }
}
