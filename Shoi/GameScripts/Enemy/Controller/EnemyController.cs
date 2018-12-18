using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵（ボス以外）の動きを制御するクラス
/// </summary>
public class EnemyController : NormalEnemy {

    private IEnemyMove iEnemyMove;
    private IEnemyAttack iEnemyAttack;

    private EnemyDamage enemyDamage;
    private EnemyParameter enemyParameter;

    int x = 0;
    int y = 0;

    /// <summary>
    /// 移動水平入力
    /// </summary>
    const string INPUT_MOVE_HORIZONTAL = "Horizontal";
    /// <summary>
    /// 移動垂直入力
    /// </summary>
    const string INPUT_MOVE_VERTICAL = "Vertical";

    private void Start () {
        iEnemyMove = GetComponent<IEnemyMove>();
        iEnemyAttack = GetComponent<IEnemyAttack>();
        enemyDamage = GetComponent<EnemyDamage>();
        enemyParameter = GetComponent<EnemyParameter>();

    }
	
	private void Update () {

        float tesX = Input.GetAxisRaw(INPUT_MOVE_HORIZONTAL);
        float tesZ = Input.GetAxisRaw(INPUT_MOVE_VERTICAL);

        // iMove.Move(x, y);
        iEnemyMove.Move(tesX, tesZ, enemyParameter.enemyMoveSpeed);

	}

    public override void TakeDamage(int damage)
    {
        enemyDamage.TakeDamage(damage);

        // HPが0以下
        if(enemyParameter.hp <= 0)
        {
            enemyDamage.DeathEnemy();
        }
    }
}
