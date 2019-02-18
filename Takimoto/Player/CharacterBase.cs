using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class CharacterBase : MonoBehaviour, IDamage
{
    public virtual void TakeDamage(int damage)
    {
        Debug.LogError("overrideされていない(CharacterBase)");
    }
    public virtual void TakeDamage(int damage, PhotonView photonView)
    {
        Debug.LogError("overrideされていない(CharacterBase)");
    }
}
