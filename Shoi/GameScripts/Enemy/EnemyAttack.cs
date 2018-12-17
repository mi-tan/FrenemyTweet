using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の攻撃クラス
/// </summary>
public class EnemyAttack : MonoBehaviour, IEnemyAttack
{

    public void Attack()
    {
        Debug.Log("攻撃");
    }
}
