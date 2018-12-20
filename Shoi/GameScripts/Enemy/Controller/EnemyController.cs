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
    private EnemyAttackListener enemyAttackListener;

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

    private void Awake()
    {
        iEnemyMove = GetComponent<IEnemyMove>();
        iEnemyAttack = GetComponent<IEnemyAttack>();
        enemyDamage = GetComponent<EnemyDamage>();
        enemyParameter = GetComponent<EnemyParameter>();
        enemyAttackListener = GetComponent<EnemyAttackListener>();

        // 使用する武器を設定
        foreach (AttackCollision weapon in enemyParameter.useWeapon)
        {
            enemyAttackListener.setAttackCollision = weapon;
            weapon.SetDebugFlag = enemyParameter.AttackCollisionDebugFlag;
        }
    }


    private void Start() {

        // HP初期化
        enemyParameter.hp = enemyParameter.getMaxHP;
    }

	
	private void Update () {

        float tesX = Input.GetAxisRaw(INPUT_MOVE_HORIZONTAL);
        float tesZ = Input.GetAxisRaw(INPUT_MOVE_VERTICAL);

        // iMove.Move(x, y);
        iEnemyMove.Move(tesX, tesZ, enemyParameter.enemyMoveSpeed);

        DebugMethod();
    }

    /// <summary>
    /// キーなどでのデバッグ用
    /// </summary>
    private void DebugMethod()
    {
        KeyCode inputKey = KeyCode.Space;
        if (Input.GetKeyDown(inputKey))
        {
            iEnemyAttack.SwordAttack(1);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            iEnemyAttack.SwordAttack(2);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            iEnemyAttack.SwordAttack(3);
        }
    }

    public override void TakeDamage(int damage)
    {
        // 
        foreach (AttackCollision weapon in enemyParameter.useWeapon)
        {
            weapon.AttackEnd();
        }

        enemyDamage.TakeDamage(damage);


        // HPが0以下
        if (enemyParameter.hp <= 0)
        {
            enemyDamage.DeathEnemy();
        }
    }
}
