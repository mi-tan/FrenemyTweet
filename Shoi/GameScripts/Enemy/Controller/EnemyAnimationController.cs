using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{

    private Animator enemyAnimator;
    /// <summary>
    /// フラグをオフにするまでのフレーム数
    /// </summary>
    private int flagOffFrame = 12;
    public int GetFlagOffFrame
    {
        get { return flagOffFrame; }
    }

    private const string PARAMETER_BOOL_RUN = "Run";
    private const string PARAMETER_BOOL_ATTACK = "Attack";
    private const string PARAMETER_BOOL_TAKEDAMAGE = "TakeDamage";
    private const string PARAMETER_INT_Type = "Type";
    private const string ANIMATION_BOOL_DEATH_A = "Death_A";
    private const string ANIMATION_BOOL_DEATH_B = "Death_B";
    private const string ANIMATION_BOOL_DEATH_C = "Death_C";

    private void Awake()
    {
        // コンポーネントを取得
        enemyAnimator = GetComponent<Animator>();
    }

    /// <summary>
    /// 移動アニメーション制御
    /// </summary>
    /// <param name="flag"></param>
    public void Run(bool value)
    {
        //Debug.Log("Run");
        Animate(PARAMETER_BOOL_RUN, value);
    }

    /// <summary>
    /// 攻撃アニメーション制御
    /// </summary>
    /// <param name="flag"></param>
    public void Attack(bool value)
    {
        // Debug.Log($"Attack = {value}");
        Animate(PARAMETER_BOOL_ATTACK, value);
    }

    /// <summary>
    /// 被ダメージ時のアニメーション制御
    /// </summary>
    public void TakeDamage(bool value)
    {
        Animate(PARAMETER_BOOL_TAKEDAMAGE, value);
    }

    /// <summary>
    /// 被ダメージ時のアニメーション制御
    /// </summary>
    public void Death(int value)
    {
        if (value == 0) { enemyAnimator.CrossFade(ANIMATION_BOOL_DEATH_A, 0.5f); }
        if (value == 1) { enemyAnimator.CrossFade(ANIMATION_BOOL_DEATH_B, 0.5f); }
        if (value == 2) { enemyAnimator.CrossFade(ANIMATION_BOOL_DEATH_C, 0.5f); }

    }

    /// <summary>
    /// アニメーションの種類
    /// </summary>
    /// <param name="value"></param>
    public void Type(int value)
    {
        Animate(PARAMETER_INT_Type, value);
    }

    #region Animate
    public void Animate(string name)
    {
        enemyAnimator.SetTrigger(name);
    }

    public void Animate(string name, bool value)
    {
        enemyAnimator.SetBool(name, value);
    }

    public void Animate(string name, int value)
    {
        enemyAnimator.SetInteger(name, value);
    }

    //public bool GetBool(string name)
    //{
    //    return enemyAnimator.GetBool(name);
    //}
    #endregion Animate
}
