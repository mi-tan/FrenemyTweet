using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;

/// <summary>
/// 敵（ボス以外）の動きを制御するクラス
/// </summary>
public sealed class EnemyController : NormalEnemy {

    private IEnemyMove iEnemyMove;
    private IEnemyAttack iEnemyAttack;
    private EnemyDamage enemyDamage;
    private EnemyParameter enemyParameter;
    private EnemyAttackListener enemyAttackListener;

    [Inject]
    private MainGameManager gameManager;

    /// <summary>
    /// 初期位置
    /// </summary>
    private Vector3 startPosition;
    /// <summary>
    /// 目的地
    /// </summary>
    private Vector3 destination;
    /// <summary>
    /// 目的地までの距離
    /// </summary>
   　private float destinationDistance = 0f;


    public enum EnemyState
    {
        Walk = 0,   // 移動
        Wait = 1,   // 到着していたら一定時間待つ
        Chase = 2,  // 追いかける
        Attack = 3, // 攻撃する
        Freeze = 4  // 攻撃後のフリーズ状態
    };

    public EnemyState state = EnemyState.Wait;

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
        // 初期位置設定
        startPosition = transform.position;
        // HP初期化
        enemyParameter.hp = enemyParameter.getMaxHP;

        // 武器のコライダー制御
        foreach (AttackCollision weapon in enemyParameter.useWeapon)
        {
            weapon.AttackEnd();
        }
    }

	
	private void Update () {
        float tesX = Input.GetAxisRaw(INPUT_MOVE_HORIZONTAL);
        float tesZ = Input.GetAxisRaw(INPUT_MOVE_VERTICAL);

        // プレイヤーとの距離
        destinationDistance = Vector3.Distance(transform.position, gameManager.player.transform.position);

        Debug.Log("プレイヤーの位置：" + enemyParameter.attackDistance);

        DebugMethod();

        if (state == EnemyState.Attack)
        {
            // 攻撃
            iEnemyAttack.SwordAttack(1);
            state = EnemyState.Freeze;
        }
        // 追いかける状態
        else if (state == EnemyState.Chase|| state == EnemyState.Walk)
        {
            SettingDistanse();
            // 目的地まで歩く処理
            iEnemyMove.Move(destination, enemyParameter.enemyMoveSpeed);

            if (state==EnemyState.Chase)
            {
                // プレイヤーと十分に近づいたら攻撃
                if (destinationDistance > enemyParameter.attackDistance) { return; }
                state = EnemyState.Attack;
            }else if (state == EnemyState.Walk)
            {
                // 目的地にたどり着いたら一時停止
                if (destinationDistance == 0) { return; }
                state = EnemyState.Wait;
            }
        }

        // 目的地にたどり着いた時の待ち状態
        else if (state == EnemyState.Wait) {
            Observable.TimerFrame(enemyParameter.waitFrame).Subscribe(_ =>
            {
                SettingDistanse();
            }).AddTo(gameObject);
        }
        // 攻撃後の硬直状態
        else if (state == EnemyState.Freeze)
        {
            Observable.TimerFrame(enemyParameter.freezeFrame).Subscribe(_ =>
            {
                SettingDistanse();
            }).AddTo(gameObject);
        }
    }

    /// <summary>
    /// プレイヤーとの距離を測る
    /// </summary>
    private void SettingDistanse()
    {
        // プレイヤーとの距離がchaseDistanceより近いとき
        if (destinationDistance <= enemyParameter.chaseDistance)
        {
            // 目的地をプレイヤーに指定
            destination = gameManager.player.transform.position;
            // 追いかける
            state = EnemyState.Chase;
        }
        else if (destinationDistance > enemyParameter.chaseDistance)
        {
            // 目的地をランダムな位置に指定
            destination = ReturnRandomDestination();
            // 巡回
            state = EnemyState.Walk;
        }
    }

    /// <summary>
    /// ランダムな位置の作成
    /// </summary>
    public Vector3 ReturnRandomDestination()
    {
        // ランダムなVector2の値を得る
        Vector2 randDestination = Random.insideUnitCircle * enemyParameter.movingRange;
        // 初期位置にランダムな位置を足して目的地とする
        Vector3 randomDestination= startPosition + new Vector3(randDestination.x, 0, randDestination.y);

        return randomDestination;
    }

    /// <summary>
    /// キーなどでのデバッグ用
    /// </summary>
    private void DebugMethod()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
        // 被ダメージ時武器の当たり判定をなくす
        foreach (AttackCollision weapon in enemyParameter.useWeapon){
            weapon.AttackEnd();
        }
        enemyDamage.TakeDamage(damage);

        // HPが0以下
        if (enemyParameter.hp <= 0) {
            enemyDamage.DeathEnemy();
        }
    }
}
