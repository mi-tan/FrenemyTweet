using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の攻撃アニメーションイベントを受け取るクラス
/// </summary>
public sealed class EnemyAttackListener : MonoBehaviour {

    /// <summary>
    /// 使用する武器のリスト
    /// </summary>
    private List<AttackCollision> attackCollisionList = new List<AttackCollision>();

    public AttackCollision setAttackCollision
    {
        set { attackCollisionList.Add(value); }
    }

    /// <summary>
    /// 武器のコライダーを有効にする
    /// </summary>
    private void EnableWeaponCollider()
    {
        foreach(AttackCollision attackCollision in attackCollisionList){
            attackCollision.AttackStart();
        }
    }

    /// <summary>
    /// 武器のコライダーを無効にする
    /// </summary>
    private void DisableWeaponCollider()
    {
        foreach (AttackCollision attackCollision in attackCollisionList)
        {
            attackCollision.AttackEnd();
        }
    }
}
