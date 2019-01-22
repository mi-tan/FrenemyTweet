using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;
using System;

/// <summary>
/// 敵（ボス以外）の動きを制御するクラス
/// </summary>
public sealed class EnemyController : NormalEnemy
{

    private IEnemyMove iEnemyMove;
    private IEnemyAttack iEnemyAttack;
    private EnemyDamage enemyDamage;
    private EnemyParameter enemyParameter;
    private EnemyAttackListener enemyAttackListener;
    private EnemyAnimationController enemyAnimationController;
    private AttackCollision attackCollision;

    [Inject]
    private MainGameManager gameManager;

    /// <summary>
    /// 初期位置
    /// </summary>
    private Vector3 startPosition;
    /// <summary>
    /// 目的地
    /// </summary>
    [SerializeField]
    private Vector3 destination;
    /// <summary>
    /// 目的地までの距離
    /// </summary>
   　private float destinationDistance = 0f;

    private float playerDistance = 0f;
    private float elapsedTime = 0f;

    private IDisposable attackWaitStream;

    public enum EnemyState
    {
        Walk = 0,   // 移動
        Wait = 1,   // 到着していたら一定時間待つ
        Chase = 2,  // 追いかける
        Attack = 3, // 攻撃する
        Freeze = 4, // 攻撃後のフリーズ状態
    };
    private EnemyState currentState = EnemyState.Wait;

    private void Awake()
    {
        iEnemyMove = GetComponent<IEnemyMove>();
        iEnemyAttack = GetComponent<IEnemyAttack>();
        enemyDamage = GetComponent<EnemyDamage>();
        enemyParameter = GetComponent<EnemyParameter>();
        enemyAttackListener = GetComponent<EnemyAttackListener>();
        enemyAnimationController = GetComponent<EnemyAnimationController>();

        // 使用する武器を設定
        foreach (AttackCollision weapon in enemyParameter.useWeapon)
        {
            enemyAttackListener.setAttackCollision = weapon;
            weapon.SetDebugFlag = enemyParameter.AttackCollisionDebugFlag;

            if (enemyParameter.attackEffect != null)
            {
                weapon.SetHitEffect = enemyParameter.attackEffect;
            }
        }
    }


    private void Start()
    {
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

    private void Update()
    {

        // プレイヤーとの距離を計測
        playerDistance = Vector3.Distance(transform.position, gameManager.player.transform.position);


        if (currentState == EnemyState.Walk || currentState == EnemyState.Wait)
        {
            // プレイヤーと近い場合（発見した時）
            if (playerDistance < enemyParameter.chaseDistance)
            {
                ChangeState(EnemyState.Chase);
            }
        }


        if (currentState == EnemyState.Wait)
        {
            // 一定時間待つ
            elapsedTime += Time.deltaTime;

            if (elapsedTime < enemyParameter.waitTime) { return; }
            elapsedTime = 0;
            destination = ReturnRandomDestination();
            ChangeState(EnemyState.Walk);
        }
        else if (currentState == EnemyState.Freeze)
        {
            // 一定時間待つ
            elapsedTime += Time.deltaTime;
            
            // iEnemyMove.MoveStop();
            if (elapsedTime < enemyParameter.freezeTime) { return; }
            elapsedTime = 0;
            ChangeState(EnemyState.Chase);
        }
        else if (currentState == EnemyState.Walk)
        {
            // 目的地までの距離を計測
            destinationDistance = Vector3.Distance(transform.position, destination);

            if (destinationDistance > 0.5f)
            {
                // 移動
                iEnemyMove.Move(destination, enemyParameter.enemyWalkSpeed);
            }
            else
            {
                // 移動アニメーション停止
                enemyAnimationController.Run(false);
                ChangeState(EnemyState.Wait);
            }
        }
        else if (currentState == EnemyState.Chase)
        {
            Vector3 direction = (gameManager.player.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(direction, transform.forward);
            //Debug.Log($"プレイヤーへの角度{angle}");

            // プレイヤーに近づききった場合
            if (angle <= enemyParameter.attackAngle &&
                playerDistance < enemyParameter.attackDistance)
            {
                // 移動アニメーション停止
                enemyAnimationController.Run(false);
                Attack();
            }
            else
            {
                // プレイヤーに近づく
                iEnemyMove.Move(gameManager.player.transform.position, enemyParameter.enemyRunSpeed);
            }

            // プレイヤーと距離が空いた場合
            if (playerDistance > enemyParameter.chaseDistance)
            {
                // 移動アニメーション停止
                enemyAnimationController.Run(false);
                ChangeState(EnemyState.Wait);
            }
        }
        //else if (currentState == EnemyState.Attack)
        //{
        //    ChangeState(EnemyState.Freeze);
        //}
    }

    /// <summary>
    /// ランダムな位置の作成
    /// </summary>
    public Vector3 ReturnRandomDestination()
    {
        // ランダムなVector2の値を得る
        Vector2 randDestination = UnityEngine.Random.insideUnitCircle * enemyParameter.movingRange;
        // 初期位置にランダムな位置を足して目的地とする
        Vector3 randomDestination = startPosition + new Vector3(randDestination.x, 0, randDestination.y);

        return randomDestination;
    }


    public override void TakeDamage(int damage)
    {
        // 被ダメージ時武器の当たり判定をなくす
        foreach (AttackCollision weapon in enemyParameter.useWeapon)
        {
            weapon.AttackEnd();
        }
        enemyDamage.TakeDamage(damage);

        ChangeState(EnemyState.Freeze);


        if (attackWaitStream != null)
        {
            attackWaitStream.Dispose();
        }

        // HPが0以下
        if (enemyParameter.hp <= 0)
        {
            enemyDamage.DeathEnemy();
        }
    }

    private void ChangeState(EnemyState state)
    {
        
       // Debug.Log($"{gameObject}:{currentState} => {state}に変更");

        currentState = state;
    }

    private void Attack()
    {
        int swordAttack_1 = 1;
        ChangeState(EnemyState.Attack);
        // 攻撃
        iEnemyAttack.SwordAttack(swordAttack_1);

        if (attackWaitStream != null)
        {
            attackWaitStream.Dispose();
        }

        attackWaitStream = Observable.Timer(System.TimeSpan.FromSeconds(0.4f))
            .Subscribe(_ => ChangeState(EnemyState.Freeze))
            .AddTo(gameObject);
        //ChangeState(EnemyState.Freeze);
    }

}