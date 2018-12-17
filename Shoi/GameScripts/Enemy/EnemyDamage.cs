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

    public void TakeDamage(int damage)
    {

        Debug.Log($"受けたダメージ： {damage}");
    }
}
