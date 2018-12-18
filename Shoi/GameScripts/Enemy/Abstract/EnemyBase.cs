using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全ての敵の基底クラス
/// </summary>
public abstract class EnemyBase : MonoBehaviour, IDamage
{

    public virtual void TakeDamage(int damage)
    {
        Debug.LogError("EnemyBaseでTakeDamage()が呼び出されています");
        throw new System.NotImplementedException();
    }
}
