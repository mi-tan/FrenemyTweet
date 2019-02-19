using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

/// <summary>
/// ボスの動きを制御するクラス
/// </summary>
public class BossEnemyController : BossEnemy {

    private IDisposable attackWaitStream;
    private IEnemyAttack iEnemyAttack;
    private BossParameter bossParameter;
    private EnemyDamage enemyDamage;
    private EnemyAnimationController enemyAnimationController;
    [SerializeField]
    private EnemySkillBase[] enemySkillBase;
    [Inject]
    MainGameManager gameManager;

    /// <summary>
    /// 経過時間
    /// </summary>
    private float elapsedTime;
    /// <summary>
    /// 現在使用しているスキル
    /// </summary>
    EnemySkillBase currentSkill;

    public enum BossEnemyState
    {
        // Walk = 0,   // 移動
        // Wait = 1,   // 到着していたら一定時間待つ
        // Chase = 2,  // 追いかける
        Spawn = 0,  // 出現時の処理
        Attack = 1, // 攻撃する
        Freeze = 2, // 攻撃後のフリーズ状態
        Death = 3,   // 死亡
    };

    public BossEnemyState currentState = BossEnemyState.Freeze;


    private void Awake()
    {
        // iEnemyAttack = GetComponent<IEnemyAttack>();
        enemyDamage = GetComponent<EnemyDamage>();
        bossParameter = GetComponent<BossParameter>();
        enemyAnimationController = GetComponent<EnemyAnimationController>();
    }

    private　void Start () {
        currentState = BossEnemyState.Freeze;
    }

    private void Update () {

        if (currentState == BossEnemyState.Attack)
        {
            currentSkill = enemySkillBase[RandomSkillNum()];
            // Debug.Log("ボス：" + currentSkill.getSkillName);
            currentSkill.setGameManager = gameManager;
            // 攻撃
            currentSkill.ActivateSkill(transform);

            if (currentSkill.getSkillAnimation != null)
            {
                // アニメーション再生
                enemyAnimationController.ChangeBossSkillClip(currentSkill.getSkillAnimation);
                enemyAnimationController.BossSkill();
            }
            else
            {
                Debug.LogWarning("スキルのアニメーションが設定されていません");
            }

            ChangeState(BossEnemyState.Freeze);
        }
        else if (currentState == BossEnemyState.Freeze)
        {
            if (currentSkill == null)
            {
                ChangeState(BossEnemyState.Attack);
                return;
            }

            Debug.Log("停止中");

            elapsedTime += Time.deltaTime;

            if (elapsedTime < currentSkill.getSkillRecoveryTime) { return; }
            elapsedTime = 0;
            ChangeState(BossEnemyState.Attack);
        } else if (currentState == BossEnemyState.Death)
        {
            //gameManager.EndGame();
        }
    }

    /// <summary>
    /// ランダムにスキル番号を返す
    /// </summary>
    /// <returns></returns>
    private int RandomSkillNum()
    {
        int randomSkillNum = UnityEngine.Random.Range(0, enemySkillBase.Length);
        return randomSkillNum;
    }

    private void ChangeState(BossEnemyState state)
    {
        // Debug.Log($"{gameObject}:{currentState} => {state}に変更");
        currentState = state;
    }

    public override void TakeDamage(int damage)
    {
        if (bossParameter.hp <= 0) { return; }

        enemyDamage.TakeDamage(damage);

        if (attackWaitStream != null)
        {
            attackWaitStream.Dispose();
        }

        // HPが0以下
        if (bossParameter.hp <= 0)
        {
            enemyDamage.DeathEnemy();
            gameObject.layer = (int)LayerManager.Layer.IgnoreRayCast;
            if (bossParameter.deathAnimation == null)
            {
                Debug.LogWarning("死亡時のアニメーションが設定されていません");
            }
            else
            {
                enemyAnimationController.BossDeath();
            }
            ChangeState(BossEnemyState.Death);
        }
        else
        {
            enemyDamage.TakeDamageAnimation();
        }
    }

    public void BossDeath()
    {
        gameManager.EndGame();
    }
}
