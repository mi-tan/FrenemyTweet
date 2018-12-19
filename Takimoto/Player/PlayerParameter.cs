using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParameter : MonoBehaviour
{
    public int MaxHp { get; private set; } = 100;

    public int Hp { get; private set; } = 100;

    public void SetHp(int value)
    {
        Hp = Mathf.Clamp(value, 0, MaxHp);
    }

    /// <summary>
    /// スキル数
    /// </summary>
    public const int SKILL_QUANTITY = 3;
}
