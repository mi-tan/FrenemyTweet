using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// 素手
/// </summary>
class BareHand : MeleeWeapon
{
    private PlayerStateManager playerStateManager;
    private PlayerAnimationManager playerAnimationManager;
    private PlayerProvider playerProvider;
    private PlayerCamera playerCamera;

    /// <summary>
    /// 入力中か
    /// </summary>
    private bool isInput = false;

    const float ATTACK_DELAY_TIME = 1.1f;

    private Coroutine attackDelayCoroutine;

    /// <summary>
    /// 攻撃角度に向く速度
    /// </summary>
    const float FACE_SPEED = 1200f;

    /// <summary>
    /// 攻撃角度
    /// </summary>
    private Quaternion attackQuaternion;

    [SerializeField]
    private Collider bareHandCollider;

    private AttackCollision attackCollision;

    [SerializeField]
    private GameObject hitEffect;

    /// <summary>
    /// 素手の攻撃力
    /// </summary>
    private int bareHandAttackPower = 10;

    [SerializeField]
    private RoundedUp roundedUp;

    private Coroutine cancelableCoroutine;
    static readonly float CANCELABLE_TIME = 0.4f;

    private float shakeTime = 0.15f;
    private float shakeX = 0.05f;
    private float shakeY = 0.015f;


    void Awake()
    {
        // コンポーネントを取得
        playerStateManager = GetComponent<PlayerStateManager>();
        playerAnimationManager = GetComponent<PlayerAnimationManager>();
        playerProvider = GetComponent<PlayerProvider>();
        attackCollision = bareHandCollider.gameObject.GetComponent<AttackCollision>();
        playerCamera = GetComponent<PlayerCamera>();

        // 素手の当たり判定を初期化
        bareHandCollider.enabled = false;
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
            attackDelayCoroutine = null;

            EndAttack();
        }

        if (attackDelayCoroutine != null)
        {
            // 攻撃方向に向く
            FaceAttack(attackQuaternion);
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

        if (attackDelayCoroutine != null) { return; }

        // プレイヤーの状態を攻撃中に変更
        playerStateManager.SetPlayerState(PlayerStateManager.PlayerState.ATTACK);

        attackDelayCoroutine = StartCoroutine(AttackDelay());

        // 通常攻撃アニメーションを再生
        playerAnimationManager.SetTriggerAttack();

        if (cancelableCoroutine != null)
        {
            StopCoroutine(cancelableCoroutine);
            cancelableCoroutine = null;
        }
        cancelableCoroutine = StartCoroutine(Cancelable());

        Vector3 attackDirection = Vector3.Scale(
            playerProvider.GetMainCamera().transform.forward, new Vector3(1, 0, 1)).normalized;

        attackQuaternion = Quaternion.LookRotation(attackDirection);

        // 移動位置を計算
        Quaternion temp = transform.rotation;
        transform.rotation = attackQuaternion;
        transform.rotation = temp;
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

        // プレイヤーの状態を行動可能に変更
        playerStateManager.SetPlayerState(PlayerStateManager.PlayerState.ACTABLE);

        attackDelayCoroutine = null;
    }

    public void StartAttack()
    {
        // 攻撃力を変更、ダメージ計算
        attackCollision.SetAttackPower = bareHandAttackPower + playerProvider.GetPlayerAttackPower();

        //Debug.Log("当たり判定：有効");
        bareHandCollider.enabled = true;
    }

    public void EndAttack()
    {
        //Debug.Log("当たり判定：無効");
        bareHandCollider.enabled = false;
    }

    private IEnumerator Cancelable()
    {
        if (cancelableCoroutine != null) { yield break; }

        playerStateManager.SetIsCancelable(false);

        yield return new WaitForSeconds(CANCELABLE_TIME);

        playerStateManager.SetIsCancelable(true);

        cancelableCoroutine = null;
    }
}
