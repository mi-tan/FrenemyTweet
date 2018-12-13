using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class MeleeWeapon : WeaponBase
{
    public override void Attack()
    {
        Debug.LogError("overrideされていない(MeleeWeapon)");
    }
}
