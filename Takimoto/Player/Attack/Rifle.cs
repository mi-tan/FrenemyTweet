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


    void Awake()
    {
        // コンポーネントを取得
        playerStateManager = GetComponent<PlayerStateManager>();
        playerAnimationManager = GetComponent<PlayerAnimationManager>();
    }

    public override void UpdateAttack(float inputAttack)
    {
        if (inputAttack >= 1)
        {
            // プレイヤーの状態を攻撃中に変更
            playerStateManager.SetPlayerState(PlayerStateManager.PlayerState.ATTACK);

            // 攻撃方向に向く
            Vector3 attackDirection = Vector3.Scale(
                Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            transform.rotation = Quaternion.LookRotation(attackDirection);

            isInput = true;
        }
        else
        {
            // プレイヤーの状態を攻撃中に変更
            playerStateManager.SetPlayerState(PlayerStateManager.PlayerState.ACTABLE);

            isInput = false;
        }

        // 通常攻撃アニメーションを再生
        playerAnimationManager.SetBoolAttack(isInput);

        // 開発途中のため、攻撃しながら歩けます
    }
}
