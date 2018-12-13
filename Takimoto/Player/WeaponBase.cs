using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class WeaponBase : MonoBehaviour, IPlayerAttack
{
    public virtual void Attack()
    {
        Debug.LogError("overrideされていない(WeaponBase)");
    }
}
