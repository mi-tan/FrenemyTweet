using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 近接プレイヤーの移動を行うクラス
/// </summary>
public class PlayerRangeMove : MonoBehaviour, IPlayerMove
{
    private PlayerStateManager playerStateManager;
    private PlayerAnimationManager playerAnimationManager;

    /// <summary>
    /// 入力の遊び値
    /// </summary>
    const float INPUT_IDLE_VALUE = 0.3f;
    /// <summary>
    /// 移動方向に向く速度
    /// </summary>
    const float FACE_SPEED = 1500f;
    /// <summary>
    /// 移動速度
    /// </summary>
    const float MOVE_SPEED = 5f;
    /// <summary>
    /// 攻撃時の移動速度
    /// </summary>
    const float ATTACK_MOVE_SPEED = 1f;

    /// <summary>
    /// 移動方向
    /// </summary>
    private Quaternion moveQuaternion;

    private bool isDodge = false;
    private Coroutine recoveryDodgeCoroutine;
    private Vector3 dodgePos = new Vector3();
    const float DODGE_DISTANCE = 6f;
    const float DODGE_SPEED = 2.5f;
    const float DODGE_TIME = 0.68f;


    void Awake()
    {
        // コンポーネントを取得
        playerStateManager = GetComponent<PlayerStateManager>();
        playerAnimationManager = GetComponent<PlayerAnimationManager>();

        // 移動方向を初期化
        moveQuaternion = transform.rotation;
    }

    /// <summary>
    /// 移動
    /// </summary>
    /// <param name="inputMoveHorizontal">移動水平入力</param>
    /// <param name="inputMoveVertical">移動垂直入力</param>
    public void UpdateMove(float inputMoveHorizontal, float inputMoveVertical)
    {
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveDirection = cameraForward * inputMoveVertical + Camera.main.transform.right * inputMoveHorizontal;

        // 移動方向に向く
        FaceMove(moveQuaternion);

        playerAnimationManager.SetFloatHorizontal(inputMoveHorizontal);
        playerAnimationManager.SetFloatVertical(inputMoveVertical);

        // 移動入力されていたら
        if (Mathf.Abs(inputMoveHorizontal) > INPUT_IDLE_VALUE ||
            Mathf.Abs(inputMoveVertical) > INPUT_IDLE_VALUE)
        {
            // 走るアニメーションを再生
            playerAnimationManager.SetBoolRun(true);

            // 現在のプレイヤーの状態が行動可能だったら
            if (playerStateManager.GetPlayerState() == PlayerStateManager.PlayerState.ACTABLE)
            {
                // 移動方向を計算
                moveQuaternion = Quaternion.LookRotation(moveDirection);

                // 位置を移動
                transform.position += moveDirection.normalized * MOVE_SPEED * Time.deltaTime;
            }
            else
            {
                moveQuaternion = transform.rotation;
            }
        }
        else
        {
            moveQuaternion = transform.rotation;

            // 走るアニメーションを停止
            playerAnimationManager.SetBoolRun(false);
        }
    }

    public void UpdateDodge(bool inputDodge, float inputMoveHorizontal, float inputMoveVertical)
    {
        if (playerStateManager.GetPlayerState() == PlayerStateManager.PlayerState.DODGE)
        {
            // 移動位置に徐々に移動
            transform.position = Vector3.Lerp(
                transform.position, dodgePos, DODGE_SPEED * Time.deltaTime);
        }

        if (inputDodge)
        {
            if (!isDodge)
            {
                Debug.Log("回避");

                if (playerStateManager.GetPlayerState() != PlayerStateManager.PlayerState.ACTABLE &&
                    (playerStateManager.GetPlayerState() != PlayerStateManager.PlayerState.ATTACK ||
                    !playerStateManager.GetIsCancelable()) &&
                    (playerStateManager.GetPlayerState() != PlayerStateManager.PlayerState.RELOAD ||
                    !playerStateManager.GetIsCancelable())) { return; }

                playerStateManager.SetPlayerState(PlayerStateManager.PlayerState.DODGE);

                // 回避アニメーション再生
                playerAnimationManager.SetTriggerDodge();

                Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
                Vector3 moveDirection = cameraForward * inputMoveVertical + Camera.main.transform.right * inputMoveHorizontal;

                transform.rotation = Quaternion.LookRotation(moveDirection);
                dodgePos = transform.position + transform.forward * DODGE_DISTANCE;

                recoveryDodgeCoroutine = StartCoroutine(RecoveryDodge());
            }

            isDodge = true;
        }
        else
        {
            isDodge = false;
        }
    }

    private IEnumerator RecoveryDodge()
    {
        if (recoveryDodgeCoroutine != null) { yield break; }

        yield return new WaitForSeconds(DODGE_TIME);

        playerStateManager.SetPlayerState(PlayerStateManager.PlayerState.ACTABLE);

        recoveryDodgeCoroutine = null;
    }

    /// <summary>
    /// 移動方向に向く
    /// </summary>
    /// <param name="moveQuaternion">移動方向</param>
    void FaceMove(Quaternion moveQuaternion)
    {
        // 現在のプレイヤーの状態が行動可能ではなかったら、この先の処理を行わない
        if (playerStateManager.GetPlayerState() != PlayerStateManager.PlayerState.ACTABLE) { return; }

        // 移動方向に向いていたら、この先の処理を行わない
        if (transform.rotation == moveQuaternion) { return; }

        // 移動方向に徐々に向く
        float step = FACE_SPEED * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation, moveQuaternion, step);
    }
}
