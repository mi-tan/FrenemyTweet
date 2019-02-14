using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 剣
/// </summary>
class Sword : MeleeWeapon
{
    private PlayerStateManager playerStateManager;
    private PlayerAnimationManager playerAnimationManager;
    private PlayerProvider playerProvider;
    private PlayerCamera playerCamera;

    /// <summary>
    /// 入力中か
    /// </summary>
    private bool isInput = false;

    /// <summary>
    /// コンボ
    /// </summary>
    private int combo = 0;
    /// <summary>
    /// 最大コンボ
    /// </summary>
    const int MAX_COMBO = 3;

    const float ATTACK_DELAY_TIME = 0.35f;
    const float STOP_COMBO_TIME = 0.65f;
    const float MAX_COMBO_TIME = 1.1f;

    private Coroutine attackDelayCoroutine;
    private Coroutine stopComboCoroutine;
    private Coroutine maxComboCoroutine;

    private float time = 0f;
    /// <summary>
    /// 攻撃中か
    /// </summary>
    private bool isAttack = false;
    /// <summary>
    /// 移動中か
    /// </summary>
    private bool isMove = false;

    /// <summary>
    /// 攻撃角度に向く速度
    /// </summary>
    const float FACE_SPEED = 1200f;

    /// <summary>
    /// 攻撃角度
    /// </summary>
    private Quaternion attackQuaternion;

    [SerializeField]
    private AttackMoveParameter swordIdleAttack1;
    [SerializeField]
    private AttackMoveParameter swordRunAttack1;
    [SerializeField]
    private AttackMoveParameter swordAttack2;
    [SerializeField]
    private AttackMoveParameter swordAttack3;

    private AttackMoveParameter attackMoveParameter;

    [SerializeField]
    private Collider swordCollider;
    [SerializeField]
    MeleeWeaponTrail trail;

    private AttackCollision attackCollision;

    [SerializeField]
    private GameObject hitEffect;

    /// <summary>
    /// 剣コンボごとの攻撃力
    /// </summary>
    private int[] swordAttackPower = { 0, 30, 35, 40 };

    [SerializeField]
    private RoundedUp roundedUp;

    private Coroutine cancelableCoroutine;
    static readonly float[] CANCELABLE_TIME = { 0f, 0.4f, 0.4f, 0.6f };

    private CharacterController characterController;

    private float shakeTime = 0.15f;
    private float shakeX = 0.05f;
    private float shakeY = 0.015f;

    private SoundManager soundManager;

    [SerializeField]
    private AudioClip[] swingSounds;
    [SerializeField]
    private float[] swingVolumes;


    void Awake()
    {
        // コンポーネントを取得
        playerStateManager = GetComponent<PlayerStateManager>();
        playerAnimationManager = GetComponent<PlayerAnimationManager>();
        playerProvider = GetComponent<PlayerProvider>();
        attackCollision = swordCollider.gameObject.GetComponent<AttackCollision>();
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponent<PlayerCamera>();

        soundManager = GetComponent<SoundManager>();

        // 剣の当たり判定を初期化
        swordCollider.enabled = false;
    }

    private void Start()
    {
        if (hitEffect == null) { return; }
        attackCollision.SetHitEffect = hitEffect;
        // 攻撃が当たった時のイベントを登録
        attackCollision.OnAttackCollision.Subscribe((_) =>
        {
            playerCamera.ShakeCamera(shakeTime, shakeX, shakeY);
        }).AddTo(gameObject);
    }

    public override void UpdateAttack(float inputAttack, float inputMoveHorizontal, float inputMoveVertical)
    {
        if (playerStateManager.GetPlayerState() == PlayerStateManager.PlayerState.DEATH)
        {
            EndAttack();
            return;
        }

        if (playerStateManager.GetPlayerState() == PlayerStateManager.PlayerState.DODGE)
        {
            // 初期化
            isAttack = false;
            time = 0f;
            isMove = false;
            combo = 0;

            if (stopComboCoroutine != null)
            {
                StopCoroutine(stopComboCoroutine);
                stopComboCoroutine = null;
            }

            if (maxComboCoroutine != null)
            {
                StopCoroutine(maxComboCoroutine);
                maxComboCoroutine = null;
            }

            attackDelayCoroutine = null;

            EndAttack();
        }

        if (isAttack)
        {
            // 攻撃方向に向く
            FaceAttack(attackQuaternion);

            time += Time.deltaTime;

            if (isMove)
            {
                if (time >= attackMoveParameter.MoveEndTime)
                {
                    // 初期化
                    isAttack = false;

                    time = 0f;
                    isMove = false;
                }
                else
                {
                    characterController.Move(transform.forward * attackMoveParameter.MoveSpeed * Time.deltaTime);
                }
            }
            else
            {
                if (time >= attackMoveParameter.MoveStartTime)
                {
                    isMove = true;
                }
            }
        }

        if (inputAttack >= 1)
        {
            if (isInput) { return; }

            // 剣攻撃
            AttackSword();

            isInput = true;
        }
        else
        {
            isInput = false;
        }
    }

    /// <summary>
    /// 剣攻撃
    /// </summary>
    void AttackSword()
    {
        // 現在のプレイヤーの状態が行動可能か攻撃中ではなかったら、この先の処理を行わない
        if (playerStateManager.GetPlayerState() != PlayerStateManager.PlayerState.ACTABLE &&
            playerStateManager.GetPlayerState() != PlayerStateManager.PlayerState.ATTACK) { return; }

        if (attackDelayCoroutine != null || maxComboCoroutine != null) { return; }

        // プレイヤーの状態を攻撃中に変更
        playerStateManager.SetPlayerState(PlayerStateManager.PlayerState.ATTACK);

        attackDelayCoroutine = StartCoroutine(AttackDelay());

        combo++;
        playerAnimationManager.SetIntegerCombo(combo);

        // 通常攻撃アニメーションを再生
        playerAnimationManager.SetTriggerAttack();

        if(cancelableCoroutine != null)
        {
            StopCoroutine(cancelableCoroutine);
            cancelableCoroutine = null;
        }
        cancelableCoroutine = StartCoroutine(Cancelable());

        Vector3 attackDirection = Vector3.Scale(
            playerProvider.GetMainCamera().transform.forward, new Vector3(1, 0, 1)).normalized;

        attackQuaternion = Quaternion.LookRotation(attackDirection);

        isMove = false;
        // 移動パラメータを変更
        ChangeMoveParameter();
        isAttack = true;

        // 移動位置を計算
        Quaternion temp = transform.rotation;
        transform.rotation = attackQuaternion;

        transform.rotation = temp;

        if (stopComboCoroutine != null)
        {
            StopCoroutine(stopComboCoroutine);
            stopComboCoroutine = null;
        }

        if (combo >= MAX_COMBO)
        {
            maxComboCoroutine = StartCoroutine(MaxCombo());
        }
        else
        {
            stopComboCoroutine = StartCoroutine(StopCombo());
        }
    }

    /// <summary>
    /// 移動パラメータを変更
    /// </summary>
    void ChangeMoveParameter()
    {
        // 初期化
        time = 0f;
        AttackMoveParameter amp = null;

        switch (combo)
        {
            case 1:
                if (!playerAnimationManager.GetBoolRun())
                {
                    amp = swordIdleAttack1;
                }
                else
                {
                    amp = swordRunAttack1;
                }
                break;

            case 2:
                amp = swordAttack2;
                break;

            case 3:
                amp = swordAttack3;
                break;
        }

        attackMoveParameter = amp;
    }

    /// <summary>
    /// 攻撃方向に向く
    /// </summary>
    /// <param name="attackQuaternion">攻撃角度</param>
    void FaceAttack(Quaternion attackQuaternion)
    {
        // 攻撃角度に向いていたら、この先の処理を行わない
        if (transform.rotation == attackQuaternion) { return; }

        // 攻撃角度に徐々に向く
        float step = FACE_SPEED * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation, attackQuaternion, step);
    }

    private IEnumerator AttackDelay()
    {
        if (attackDelayCoroutine != null) { yield break; }

        yield return new WaitForSeconds(ATTACK_DELAY_TIME);

        attackDelayCoroutine = null;
    }

    private IEnumerator StopCombo()
    {
        if (stopComboCoroutine != null) { yield break; }

        yield return new WaitForSeconds(STOP_COMBO_TIME);

        // コンボをリセット
        combo = 0;
        // プレイヤーの状態を行動可能に変更
        playerStateManager.SetPlayerState(PlayerStateManager.PlayerState.ACTABLE);

        stopComboCoroutine = null;
    }

    private IEnumerator MaxCombo()
    {
        if (maxComboCoroutine != null) { yield break; }

        yield return new WaitForSeconds(MAX_COMBO_TIME);

        // コンボをリセット
        combo = 0;
        // プレイヤーの状態を行動可能に変更
        playerStateManager.SetPlayerState(PlayerStateManager.PlayerState.ACTABLE);

        maxComboCoroutine = null;
    }

    public void StartAttack()
    {
        // 攻撃力を変更、ダメージ計算
        attackCollision.SetAttackPower = swordAttackPower[combo] + playerProvider.GetPlayerAttackPower();

        if (soundManager && swingSounds.Length > 0)
        {
            // 剣振り音再生
            soundManager.PlaySound(swingSounds[combo], swingVolumes[combo]);
        }

        //Debug.Log("当たり判定：有効");
        swordCollider.enabled = true;

        if (trail == null) { return; }
        // 軌跡有効
        trail.Emit = true;
    }

    public void EndAttack()
    {
        //Debug.Log("当たり判定：無効");
        swordCollider.enabled = false;

        if (trail == null) { return; }
        // 軌跡無効
        trail.Emit = false;
    }


    private IEnumerator Cancelable()
    {
        if (cancelableCoroutine != null) { yield break; }

        playerStateManager.SetIsCancelable(false);

        yield return new WaitForSeconds(CANCELABLE_TIME[combo]);

        playerStateManager.SetIsCancelable(true);

        cancelableCoroutine = null;
    }
}
