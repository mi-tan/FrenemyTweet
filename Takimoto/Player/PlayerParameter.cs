using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParameter : MonoBehaviour
{
    /// <summary>
    /// 最大HP
    /// </summary>
    public int MaxHp { get; private set; } = 100;
    public void SetMaxHp(int value)
    {
        MaxHp = value;
    }

    /// <summary>
    /// 現在のHP
    /// </summary>
    public int Hp { get; private set; } = 100;
    public void SetHp(int value)
    {
        Hp = Mathf.Clamp(value, 0, MaxHp);
    }

    /// <summary>
    /// 基礎攻撃力
    /// </summary>
    public int BasicAttackPower { get; private set; } = 1;
    public void SetBasicAttackPower(int value)
    {
        BasicAttackPower = value;
    }

    /// <summary>
    /// 現在のプレイヤーの攻撃力
    /// </summary>
    public int PlayerAttackPower { get; private set; } = 1;  
    public void SetPlayerAttackPower(int value)
    {
        PlayerAttackPower = value;
    }

    /// <summary>
    /// プレイヤーの移動速度
    /// </summary>
    public float MoveSpeed { get; private set; } = 6f;
    public void SetMoveSpeed(float value)
    {
        MoveSpeed = value;
    }

    /// <summary>
    /// スキル数
    /// </summary>
    public const int SKILL_QUANTITY = 3;
}
