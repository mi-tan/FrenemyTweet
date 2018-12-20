using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// ダメージを計算するクラス
/// </summary>
public class EnemyDamage :　MonoBehaviour {

    /// <summary>
    /// 防御力
    /// </summary>
    private int deffencePower = 0;
    /// <summary>
    /// ダメージアニメーションの数
    /// </summary>
    private int damageAnimationNum = 4;

    private EnemyParameter enemyparameter;
    private EnemyAnimationController animationController;

    private void Awake()
    {
        enemyparameter = GetComponent<EnemyParameter>();
        animationController = GetComponent<EnemyAnimationController>();
    }

    public void TakeDamage(int damage)
    {
        int randNum = Random.Range(1, damageAnimationNum);
        // HPを減らす
        enemyparameter.hp -= damage;
        // 被ダメージ時のアニメーション再生
        animationController.TakeDamage(true);
        animationController.Type(randNum);
        Observable.TimerFrame(animationController.GetFlagOffFrame).Subscribe(_ => {
        // 被ダメージ時のアニメーション停止
            animationController.TakeDamage(false);
        });
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
