using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour {


    private Animator enemyAnimator;

    private const string PARAMETER_BOOL_RUN = "Run";

    private void Awake()
    {
        // コンポーネントを取得
        enemyAnimator = GetComponent<Animator>();
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

    public void Run(bool flag)
    {
        Animate(PARAMETER_BOOL_RUN, flag);
    }
}
