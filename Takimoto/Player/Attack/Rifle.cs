using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Rifle : RangeWeapon
{
    private PlayerAnimationManager playerAnimationManager;

    /// <summary>
    /// 入力中か
    /// </summary>
    private bool isInput = false;


    void Awake()
    {
        // コンポーネントを取得
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
        playerAnimationManager.Animate(
            PlayerAnimationManager.PARAMETER_TRIGGER_ATTACK, isInput);
    }

    void AttackRifle()
    {

    }
}
