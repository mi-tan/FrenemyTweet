using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーのアニメーションを管理するクラス
/// </summary>
public class PlayerAnimationManager : MonoBehaviour
{
    private Animator playerAnimator;

    /// <summary>
    /// 走るパラメータ
    /// </summary>
    private const string PARAMETER_BOOL_RUN = "Run";
    /// <summary>
    /// 通常攻撃パラメータ
    /// </summary>
    private const string PARAMETER_TRIGGER_ATTACK = "Attack";
    /// <summary>
    /// コンボパラメータ
    /// </summary>
    private const string PARAMETER_INT_COMBO = "Combo";


    void Awake()
    {
        // コンポーネントを取得
        playerAnimator = GetComponent<Animator>();
    }


    public void SetBoolRun(bool value)
    {
        playerAnimator.SetBool(PARAMETER_BOOL_RUN, value);
    }

    public bool GetBoolRun()
    {
        return playerAnimator.GetBool(PARAMETER_BOOL_RUN);
    }

    public void SetTriggerAttack()
    {
        playerAnimator.SetTrigger(PARAMETER_TRIGGER_ATTACK);
    }

    public void SetBoolAttack(bool value)
    {
        playerAnimator.SetBool(PARAMETER_TRIGGER_ATTACK, value);
    }

    public void SetIntegerCombo(int value)
    {
        playerAnimator.SetInteger(PARAMETER_INT_COMBO, value);
    }
}
