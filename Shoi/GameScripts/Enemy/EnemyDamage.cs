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
    private int deffencePower = 0;
    /// <summary>
    /// ダメージアニメーションの数
    /// </summary>
    private int damageAnimationNum = 4;
    /// <summary>
    /// 被ダメージ時のエフェクト
    /// </summary>
    private GameObject damageEffect;

    private EnemyParameter enemyparameter;
    private EnemyAnimationController animationController;

    private void Awake()
    {
        enemyparameter = GetComponent<EnemyParameter>();
        animationController = GetComponent<EnemyAnimationController>();

        if (damageEffect == null)
        {
            Debug.LogWarning("ダメージエフェクト未設定");
            return;
        }

        damageEffect = enemyparameter.damageEffect;
    }

    public void TakeDamage(int damage)
    {
        int randNum = Random.Range(1, damageAnimationNum);
        // HPを減らす
        enemyparameter.hp -= damage;
        // 被ダメージ時のアニメーション再生
        animationController.TakeDamage(true);
        animationController.Type(randNum);
        // エフェクト生成
        if (damageEffect==null)
        {
            Debug.LogWarning("敵被ダメージ時のエフェクトが設定されていません");
        }
        else
        {
            Instantiate(damageEffect, transform.position, transform.rotation);
        }

        Observable.TimerFrame(animationController.GetFlagOffFrame).Subscribe(_ =>
        {
            // 被ダメージ時のアニメーション停止
            animationController.TakeDamage(false);
        }).AddTo(gameObject);
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
