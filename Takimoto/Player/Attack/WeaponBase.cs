using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class WeaponBase : MonoBehaviour, IPlayerAttack
{
    public virtual void UpdateAttack(float inputAttack, float inputMoveHorizontal, float inputMoveVertical)
    {
        Debug.LogError("overrideされていない(WeaponBase)");
    }

    [SerializeField]
    private Sprite weaponIcon;
    public Sprite GetWeaponIcon()
    {
        return weaponIcon;
    }
}
