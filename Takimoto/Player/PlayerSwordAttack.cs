using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの通常攻撃を行うクラス
/// </summary>
public class PlayerSwordAttack : MonoBehaviour
{
    private Animator playerAnimator;

    /// <summary>
    /// 通常攻撃入力
    /// </summary>
    const string INPUT_ATTACK = "Attack";

    /// <summary>
    /// 通常攻撃パラメータ
    /// </summary>
    const string PARAMETER_SWORD_ATTACK = "SwordAttack";
    const string PARAMETER_COMBO = "Combo";

    private bool isInput = false;

    /// <summary>
    /// コンボ
    /// </summary>
    private int combo = 0;
    const int MAX_COMBO = 3;

    const float ATTACK_DELAY_TIME = 0.2f;
    const float STOP_COMBO_TIME = 0.75f;
    const float MAX_COMBO_TIME = 0.95f;

    private Coroutine attackDelayCoroutine;
    private Coroutine stopComboCoroutine;
    private Coroutine maxComboCoroutine;


    void Awake()
    {
        // コンポーネントを取得
        playerAnimator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
        // 初期化
        playerAnimator.SetInteger(PARAMETER_COMBO, combo);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //if (Input.GetButtonDown("Jump"))
        //{
        //    playerAnimator.SetTrigger("Jump");
        //}

        if(Input.GetAxisRaw(INPUT_ATTACK) >= 1)
        {
            if (!isInput)
            {
                if (attackDelayCoroutine == null && maxComboCoroutine == null)
                {
                    attackDelayCoroutine = StartCoroutine(AttackDelay());

                    combo++;
                    playerAnimator.SetInteger(PARAMETER_COMBO, combo);

                    // 通常攻撃アニメーションを再生
                    playerAnimator.SetTrigger(PARAMETER_SWORD_ATTACK);

                    if (combo >= MAX_COMBO)
                    {
                        maxComboCoroutine = StartCoroutine(MaxCombo());
                    }
                    else
                    {
                        if (stopComboCoroutine != null)
                        {
                            StopCoroutine(stopComboCoroutine);
                            stopComboCoroutine = null;
                        }
                        stopComboCoroutine = StartCoroutine(StopCombo());
                    }
                }
            }

            isInput = true;
        }
        else
        {
            isInput = false;
        }
    }

    private IEnumerator AttackDelay()
    {
        if (attackDelayCoroutine != null) { yield break; }
        yield return new WaitForSeconds(ATTACK_DELAY_TIME);
        attackDelayCoroutine = null;
    }

    private IEnumerator StopCombo()
    {
        if (stopComboCoroutine != null) { yield break; }
        yield return new WaitForSeconds(STOP_COMBO_TIME);
        combo = 0;
    }

    private IEnumerator MaxCombo()
    {
        if (maxComboCoroutine != null) { yield break; }
        yield return new WaitForSeconds(MAX_COMBO_TIME);
        combo = 0;
        maxComboCoroutine = null;
    }
}
