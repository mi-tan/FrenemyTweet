using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// ダメージを計算するクラス
/// </summary>
public sealed class EnemyDamage :　MonoBehaviour {

    /// <summary>
    /// 防御力
    /// </summary>
    //private int deffencePower = 0;

    /// <summary>
    /// ダメージアニメーションの数
    /// </summary>
    private int damageAnimationNum = 4;
    /// <summary>
    /// 死亡アニメーションの数
    /// </summary>
    private int deathAnimationNum = 3;

    private EnemyParameter enemyparameter;
    private EnemyAnimationController enemyAnimationController;

    private void Awake()
    {
        enemyparameter = GetComponent<EnemyParameter>();
        enemyAnimationController = GetComponent<EnemyAnimationController>();
    }

    public void TakeDamage(int damage)
    {
        // HPを減らす
        enemyparameter.hp -= damage;
        Debug.Log($"受けたダメージ： {damage}");
    }

    /// <summary>
    /// 被ダメージ時のアニメーション再生
    /// </summary>
    public void TakeDamageAnimation()
    {
        int randNum = Random.Range(1, damageAnimationNum);
        // 被ダメージ時のアニメーション再生
        enemyAnimationController.TakeDamage(true);
        enemyAnimationController.Type(randNum);

        Observable.TimerFrame(enemyAnimationController.GetFlagOffFrame).Subscribe(_ =>
        {
            // 被ダメージ時のアニメーション停止
            enemyAnimationController.TakeDamage(false);
        }).AddTo(gameObject);
    }

    /// <summary>
    /// 死亡処理
    /// </summary>
    public void DeathEnemy()
    {
        // 移動アニメーションを停止
        enemyAnimationController.Run(false);

        // アニメーションを呼び出し
        enemyAnimationController.Death(true);
        enemyAnimationController.Type(Random.Range(0, deathAnimationNum));

        // 呼び出したアニメーションを停止
        Observable.TimerFrame(enemyAnimationController.GetFlagOffFrame).Subscribe(_ =>
        {
            enemyAnimationController.Death(false);
        }).AddTo(gameObject);

    }

    /// <summary>
    /// オブジェクトを削除
    /// </summary>
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
