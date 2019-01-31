using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// ボスの動きを制御するクラス
/// </summary>
public class BossEnemyController : BossEnemy {

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
        Freeze = 1  // 攻撃後のフリーズ状態
    };

    public BossEnemyState currentState = BossEnemyState.Freeze;

    //private BossParameter bossParameter;
    //private IEnemyAttack iEnemyAttack;

    private　void Start () {
        //iEnemyAttack = GetComponent<IEnemyAttack>();
        //bossParameter = GetComponent<BossParameter>();
        currentState = BossEnemyState.Freeze;

    }

    private void Update () {

        if (currentState == BossEnemyState.Attack)
        {
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
        }
	}
    private void ChangeState(BossEnemyState state)
    {
        // Debug.Log($"{gameObject}:{currentState} => {state}に変更");
        currentState = state;
    }

}
