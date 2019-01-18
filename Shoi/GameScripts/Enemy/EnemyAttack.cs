using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 敵の攻撃クラス
/// </summary>
public sealed class EnemyAttack : MonoBehaviour, IEnemyAttack
{
    private EnemyAnimationController enemyAnimationController;


    private void Start()
    {
        enemyAnimationController = GetComponent<EnemyAnimationController>();
    }

    public void SwordAttack(int value)
    {
        // 移動アニメーションを停止
        enemyAnimationController.Run(false);
        // 攻撃を許可
        enemyAnimationController.Attack(true);
        // 攻撃アニメーションを呼び出し
        enemyAnimationController.Type(value);

        Observable.TimerFrame(enemyAnimationController.GetFlagOffFrame).Subscribe(_ => {
            enemyAnimationController.Attack(false);
        });
    }
}
