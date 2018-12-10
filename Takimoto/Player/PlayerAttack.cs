using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの通常攻撃を行うクラス
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    private PlayerStateManager playerStateManager;
    private PlayerAnimationController playerAnimationController;

    /// <summary>
    /// 通常攻撃入力
    /// </summary>
    const string INPUT_ATTACK = "Attack";

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

    const float ATTACK_DELAY_TIME = 0.2f;
    const float STOP_COMBO_TIME = 0.8f;
    const float MAX_COMBO_TIME = 0.95f;

    private Coroutine attackDelayCoroutine;
    private Coroutine stopComboCoroutine;
    private Coroutine maxComboCoroutine;

    private float time = 0f;
    private float moveTime = 1f;
    public bool attack = false;
    public bool move = false;
    private float forward = 0;
    private float speed = 0;
    private Vector3 endPosition;


    void Awake()
    {
        // コンポーネントを取得
        playerStateManager = GetComponent<PlayerStateManager>();
        playerAnimationController = GetComponent<PlayerAnimationController>();
    }

    // Use this for initialization
    void Start () {
        // 初期化
        playerAnimationController.Animate(
            PlayerAnimationController.PARAMETER_INT_COMBO, combo);
    }
	
	// Update is called once per frame
	void Update ()
    {
        // ↓テスト用↓
        if (attack)
        {
            time += Time.deltaTime;
            if(time >= moveTime)
            {
                move = true;
            }

            if (move)
            {
                if (transform.position != endPosition)
                {
                    transform.position = Vector3.Lerp(
                        transform.position, endPosition, speed * Time.deltaTime);
                }
                else
                {
                    attack = false;

                    time = 0f;
                    move = false;
                }
            }
        }
        // ↑テスト用↑

        if (Input.GetAxisRaw(INPUT_ATTACK) >= 1)
        {
            if (isInput) { return; }

            // 通常攻撃
            Attack();

            isInput = true;
        }
        else
        {
            isInput = false;
        }
    }

    /// <summary>
    /// 通常攻撃
    /// </summary>
    void Attack()
    {
        if (attackDelayCoroutine != null || maxComboCoroutine != null) { return; }

        // プレイヤーの状態を攻撃中に変更
        playerStateManager.SetPlayerState(PlayerStateManager.PlayerState.ATTACK);

        attackDelayCoroutine = StartCoroutine(AttackDelay());

        combo++;
        playerAnimationController.Animate(
            PlayerAnimationController.PARAMETER_INT_COMBO, combo);

        // 通常攻撃アニメーションを再生
        playerAnimationController.Animate(
            PlayerAnimationController.PARAMETER_TRIGGER_ATTACK);

        move = false;
        MoveAttack();
        endPosition = transform.position + transform.forward * forward;
        attack = true;

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
    /// 攻撃移動
    /// </summary>
    void MoveAttack()
    {
        // ↓テスト用↓
        switch (combo)
        {
            case 1:
                if (playerAnimationController.GetBool(
                    PlayerAnimationController.PARAMETER_BOOL_RUN))
                {
                    time = 0f;
                    moveTime = 0.2f;
                    forward = 1.3f;
                    speed = 19f;
                }
                else
                {
                    time = 0f;
                    moveTime = 0.05f;
                    forward = 0.7f;
                    speed = 15f;
                }
                break;

            case 2:
                time = 0f;
                moveTime = 0.3f;
                forward = 1f;
                speed = 22f;
                break;

            case 3:
                time = 0f;
                moveTime = 0.2f;
                forward = 1f;
                speed = 19f;
                break;
        }
        // ↑テスト用↑
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
        ResetCombo();

        stopComboCoroutine = null;
    }

    private IEnumerator MaxCombo()
    {
        if (maxComboCoroutine != null) { yield break; }

        yield return new WaitForSeconds(MAX_COMBO_TIME);

        // コンボをリセット
        ResetCombo();

        maxComboCoroutine = null;
    }

    /// <summary>
    /// コンボをリセット
    /// </summary>
    void ResetCombo()
    {
        combo = 0;

        // プレイヤーの状態を行動可能に変更
        playerStateManager.SetPlayerState(PlayerStateManager.PlayerState.ACTABLE);
    }
}
