using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 銃
/// </summary>
class Rifle : RangeWeapon
{
    //private PlayerStateManager playerStateManager;
    private PlayerAnimationManager playerAnimationManager;

    /// <summary>
    /// 入力中か
    /// </summary>
    private bool isInput = false;


    void Awake()
    {
        // コンポーネントを取得
        //playerStateManager = GetComponent<PlayerStateManager>();
        playerAnimationManager = GetComponent<PlayerAnimationManager>();
    }

    public override void UpdateAttack(float inputAttack)
    {
        if (inputAttack >= 1)
        {
            if (isInput) { return; }

            isInput = true;
        }
        else
        {
            isInput = false;
        }

        // 通常攻撃アニメーションを再生
        playerAnimationManager.SetBoolAttack(isInput);

        // 開発途中のため、攻撃しながら歩けます
    }

    void AttackRifle()
    {

    }
}
