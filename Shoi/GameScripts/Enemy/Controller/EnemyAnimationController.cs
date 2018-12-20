using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{

    private Animator enemyAnimator;

    private const string PARAMETER_BOOL_RUN = "Run";
    private const string PARAMETER_BOOL_ATTACK = "Attack";
    private const string PARAMETER_INT_Type = "Type";

    private void Awake()
    {
        // コンポーネントを取得
        enemyAnimator = GetComponent<Animator>();
    }

    /// <summary>
    /// Runアニメーション制御
    /// </summary>
    /// <param name="flag"></param>
    public void Run(bool value)
    {
        Animate(PARAMETER_BOOL_RUN, value);
    }

    /// <summary>
    /// Attackアニメーション制御
    /// </summary>
    /// <param name="flag"></param>
    public void Attack(bool value)
    {
        Animate(PARAMETER_BOOL_ATTACK, value);
    }

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
