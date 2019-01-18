using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボスの動きを制御するクラス
/// </summary>
public class BossEnemyController : BossEnemy {

    /// <summary>
    /// 経過時間
    /// </summary>
    private float elapsedTime;

    public enum BossEnemyState
    {
        // Walk = 0,   // 移動
        // Wait = 1,   // 到着していたら一定時間待つ
        // Chase = 2,  // 追いかける
        Attack = 0, // 攻撃する
        Freeze = 1, // 攻撃後のフリーズ状態
        Skill = 2   // スキルを使った攻撃 
    };

    public BossEnemyState state = BossEnemyState.Freeze;

    BossParameter bossParameter;
    IEnemyAttack iEnemyAttack;

    // Use this for initialization
    private　void Start () {
        iEnemyAttack = GetComponent<IEnemyAttack>();
        bossParameter = GetComponent<BossParameter>();

    }
	
	// Update is called once per frame
	private void Update () {

        if (state == BossEnemyState.Attack)
        {
            // 攻撃


            state = BossEnemyState.Freeze;
        }
        else if (state == BossEnemyState.Skill)
        {
            // スキル


            state = BossEnemyState.Freeze;
        }
        else if (state == BossEnemyState.Freeze)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime < bossParameter.freezeTime) { return; }
            elapsedTime = 0;
            state = BossEnemyState.Attack;
        }
	}
}
