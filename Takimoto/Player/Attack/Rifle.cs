using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 銃
/// </summary>
class Rifle : RangeWeapon
{
    private PlayerStateManager playerStateManager;
    private PlayerAnimationManager playerAnimationManager;

    /// <summary>
    /// 入力中か
    /// </summary>
    private bool isInput = false;

    /// <summary>
    /// 攻撃角度
    /// </summary>
    private Quaternion attackQuaternion;

    /// <summary>
    /// 構え移動速度
    /// </summary>
    const float STANCE_MOVE_SPEED = 1.5f;

    const int MAX_BULLET_NUMBER = 30;
    private int bulletNumber = 30; 


    void Awake()
    {
        // コンポーネントを取得
        playerStateManager = GetComponent<PlayerStateManager>();
        playerAnimationManager = GetComponent<PlayerAnimationManager>();
    }

    public override void UpdateAttack(float inputAttack, float inputMoveHorizontal, float inputMoveVertical)
    {
        if (inputAttack >= 1)
        {
            if (!isInput)
            {
                isInput = true;
            }
        }
        else
        {
            isInput = false;
        }

        if (playerStateManager.GetPlayerState() == PlayerStateManager.PlayerState.SKILL) { return; }

        // 通常攻撃アニメーションを再生
        playerAnimationManager.SetBoolAttack(isInput);

        if (isInput)
        {
            // プレイヤーの状態を攻撃中に変更
            playerStateManager.SetPlayerState(PlayerStateManager.PlayerState.ATTACK);

            Vector3 attackDirection = Vector3.Scale(
                Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            attackQuaternion = Quaternion.LookRotation(attackDirection);

            // 攻撃方向に向く
            FaceAttack(attackQuaternion);

            // 構え移動
            StanceMove(inputMoveHorizontal, inputMoveVertical);

            // 弾を発射
            ShootBullet();
        }
        else
        {
            if (playerStateManager.GetPlayerState() == PlayerStateManager.PlayerState.ATTACK)
            {
                // プレイヤーの状態を行動可能に変更
                playerStateManager.SetPlayerState(PlayerStateManager.PlayerState.ACTABLE);
            }
        }
    }

    /// <summary>
    /// 攻撃方向に向く
    /// </summary>
    /// <param name="attackQuaternion">攻撃角度</param>
    void FaceAttack(Quaternion attackQuaternion)
    {
        // 攻撃角度に向いていなかったら
        if (transform.rotation != attackQuaternion)
        {
            // 攻撃角度に向く
            transform.rotation = attackQuaternion;
        }
    }

    /// <summary>
    /// 構え移動
    /// </summary>
    void StanceMove(float inputMoveHorizontal, float inputMoveVertical)
    {
        if (playerStateManager.GetPlayerState() != PlayerStateManager.PlayerState.ATTACK) { return; }

        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveForward = cameraForward * inputMoveVertical + Camera.main.transform.right * inputMoveHorizontal;

        transform.position += moveForward * STANCE_MOVE_SPEED * Time.deltaTime;
    }

    void ShootBullet()
    {

    }
}
