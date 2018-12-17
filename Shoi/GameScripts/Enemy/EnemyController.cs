using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵（ボス以外）の動きを制御するクラス
/// </summary>
public class EnemyController : NormalEnemy {

    private IEnemyMove iEnemyMove;
    private IEnemyAttack iEnemyAttack;

    // int x = 0;
    // int y = 0;

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

    }
	
	private void Update () {

        float tesX = Input.GetAxisRaw(INPUT_MOVE_HORIZONTAL);
        float tesZ = Input.GetAxisRaw(INPUT_MOVE_VERTICAL);

        // iMove.Move(x, y);
        iEnemyMove.Move(tesX, tesZ, enemyMoveSpeed);


	}
}
