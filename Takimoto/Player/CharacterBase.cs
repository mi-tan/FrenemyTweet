using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, IDamage
{
    public virtual void TakeDamage(int damage)
    {
        Debug.LogError("overrideされていない(CharacterBase)");
    }
}
