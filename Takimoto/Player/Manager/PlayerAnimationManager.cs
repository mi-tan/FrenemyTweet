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
    public const string PARAMETER_BOOL_RUN = "Run";
    /// <summary>
    /// 通常攻撃パラメータ
    /// </summary>
    public const string PARAMETER_TRIGGER_ATTACK = "Attack";
    /// <summary>
    /// コンボパラメータ
    /// </summary>
    public const string PARAMETER_INT_COMBO = "Combo";


    void Awake()
    {
        // コンポーネントを取得
        playerAnimator = GetComponent<Animator>();
    }

    public void Animate(string name)
    {
        playerAnimator.SetTrigger(name);
    }

    public void Animate(string name, bool value)
    {
        playerAnimator.SetBool(name, value);
    }

    public void Animate(string name, int value)
    {
        playerAnimator.SetInteger(name, value);
    }

    public bool GetBool(string name)
    {
        return playerAnimator.GetBool(name);
    }
}
