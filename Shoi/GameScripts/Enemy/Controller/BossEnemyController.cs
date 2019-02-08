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

    [SerializeField]
    private EnemySkillBase[] enemySkillBase;
    [Inject]
    MainGameManager gameManager;

    /// <summary>
    /// 経過時間
    /// </summary>
    private float elapsedTime;
    /// <summary>
    /// 現在使用しているスキル番号
    /// </summary>
    private int nowSkillNum = 0;

    public enum BossEnemyState
    {
        // Walk = 0,   // 移動
        // Wait = 1,   // 到着していたら一定時間待つ
        // Chase = 2,  // 追いかける
        Attack = 0, // 攻撃する
        Freeze = 1, // 攻撃後のフリーズ状態
        Death = 2   // 死亡
    };

    public BossEnemyState currentState = BossEnemyState.Freeze;


    private void Awake()
    {
        // iEnemyAttack = GetComponent<IEnemyAttack>();
        enemyDamage = GetComponent<EnemyDamage>();
        bossParameter = GetComponent<BossParameter>();
    }

    private　void Start () {
        currentState = BossEnemyState.Freeze;

    }

    private void Update () {

        if (currentState == BossEnemyState.Attack)
        {
            nowSkillNum = RandomSkillNum();
            Debug.Log("ボス攻撃");
            enemySkillBase[nowSkillNum].setGameManager = gameManager;
            // 攻撃
            enemySkillBase[nowSkillNum].ActivateSkill(transform);
            ChangeState(BossEnemyState.Freeze);
        }
        else if (currentState == BossEnemyState.Freeze)
        {
            elapsedTime += Time.deltaTime;
            Debug.Log("硬直中 時間：" + enemySkillBase[nowSkillNum].getSkillRecoveryTime);
            if (elapsedTime < enemySkillBase[nowSkillNum].getSkillRecoveryTime) { return; }
            elapsedTime = 0;
            ChangeState(BossEnemyState.Attack);
        }else if(currentState == BossEnemyState.Death)
        {
            Debug.Log("しにました");
        }
	}

    private int RandomSkillNum()
    {
        // ランダムにスキルを設定
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

            ChangeState(BossEnemyState.Death);
        }
        else
        {
            enemyDamage.TakeDamageAnimation();
        }
    }
}
