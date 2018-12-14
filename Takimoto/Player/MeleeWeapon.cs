using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 近接武器
/// </summary>
abstract class MeleeWeapon : WeaponBase
{
    public override void UpdateAttack(float inputAttack)
    {
        Debug.LogError("overrideされていない(MeleeWeapon)");
    }
}
