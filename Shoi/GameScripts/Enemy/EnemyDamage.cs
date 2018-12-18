using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ダメージを計算するクラス
/// </summary>
public class EnemyDamage :　MonoBehaviour {

    /// <summary>
    /// 防御力
    /// </summary>
    int deffencePower = 0;

    private EnemyParameter enemyparameter;

    private void Awake()
    {
        enemyparameter = GetComponent<EnemyParameter>();
    }

    public void TakeDamage(int damage)
    {
        enemyparameter.hp -= damage;
        Debug.Log($"受けたダメージ： {damage}");
    }

    /// <summary>
    /// 死亡処理
    /// </summary>
    public void DeathEnemy()
    {
        Debug.Log("敵死亡");
        Destroy(transform.gameObject);
    }
}
