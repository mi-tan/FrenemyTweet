using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 近接武器
/// </summary>
abstract class MeleeWeapon : WeaponBase
{
    public override void UpdateAttack(float inputAttack, float inputMoveHorizontal, float inputMoveVertical)
    {
        Debug.LogError("overrideされていない(MeleeWeapon)");
    }
}
