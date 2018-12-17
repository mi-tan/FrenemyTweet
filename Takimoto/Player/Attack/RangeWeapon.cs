using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 遠距離武器
/// </summary>
abstract class RangeWeapon : WeaponBase
{
    public override void UpdateAttack(float inputAttack)
    {
        Debug.LogError("overrideされていない(RangeWeapon)");
    }
}
