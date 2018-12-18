using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProvider : CharacterBase
{
    private PlayerDamage playerDamage;
    private PlayerParameter playerParameter;


    void Awake()
    {
        // コンポーネントを取得
        playerDamage = GetComponent<PlayerDamage>();
        playerParameter = GetComponent<PlayerParameter>();
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
}
