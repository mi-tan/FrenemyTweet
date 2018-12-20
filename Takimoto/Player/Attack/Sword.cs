using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 剣
/// </summary>
class Sword : MeleeWeapon
{
    private PlayerStateManager playerStateManager;
    private PlayerAnimationManager playerAnimationManager;

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

    const float ATTACK_DELAY_TIME = 0.25f;
    const float STOP_COMBO_TIME = 0.8f;
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
    /// 移動位置
    /// </summary>
    private Vector3 movePosition;

    /// <summary>
    /// 攻撃角度に向く速度
    /// </summary>
    const float FACE_SPEED = 1200f;

    /// <summary>
    /// 攻撃角度
    /// </summary>
    private Quaternion attackQuaternion;

    [System.Serializable]
    public struct MoveParameters
    {
        /// <summary>
        /// 移動時間
        /// </summary>
        public float moveTime;
        /// <summary>
        /// 移動距離
        /// </summary>
        public float moveDistance;
        /// <summary>
        /// 移動速度
        /// </summary>
        public float moveSpeed;
    }
    [SerializeField]
    private MoveParameters[] moveParameters = new MoveParameters[4];
    private MoveParameters moveParameter;


    void Awake()
    {
        // コンポーネントを取得
        playerStateManager = GetComponent<PlayerStateManager>();
        playerAnimationManager = GetComponent<PlayerAnimationManager>();
    }

    public override void UpdateAttack(float inputAttack)
    {
        if (isAttack)
        {
            // 攻撃方向に向く
            FaceAttack(attackQuaternion);

            time += Time.deltaTime;
            if (time >= moveParameter.moveTime)
            {
                isMove = true;
            }

            if (isMove)
            {
                if (transform.position != movePosition)
                {
                    // 移動位置に徐々に移動
                    transform.position = Vector3.Lerp(
                        transform.position, movePosition, moveParameter.moveSpeed * Time.deltaTime);
                }
                else
                {
                    // 初期化
                    isAttack = false;

                    time = 0f;
                    isMove = false;
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

        Vector3 attackDirection = Vector3.Scale(
            Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        attackQuaternion = Quaternion.LookRotation(attackDirection);

        isMove = false;
        // 移動パラメータを変更
        ChangeMoveParameter();
        isAttack = true;

        // 移動位置を計算
        Quaternion temp = transform.rotation;
        transform.rotation = attackQuaternion;
        movePosition = transform.position + transform.forward * moveParameter.moveDistance;
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
        int num = 0;

        switch (combo)
        {
            case 1:
                if (playerAnimationManager.GetBoolRun())
                {
                    num = 0;
                }
                else
                {
                    num = 1;
                }
                break;

            case 2:
                num = 2;
                break;

            case 3:
                num = 3;
                break;
        }

        moveParameter = moveParameters[num];
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
}
