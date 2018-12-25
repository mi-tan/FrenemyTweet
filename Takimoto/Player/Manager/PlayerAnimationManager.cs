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
    /// <summary>
    /// スキルパラメータ
    /// </summary>
    private const string PARAMETER_TRIGGER_SKILL = "Skill";
    private const string PARAMETER_FLOAT_SKILL_SPEED = "SkillSpeed";

    private AnimatorOverrideController overrideController;
    const string OVERRIDE_CLIP_NAME = "Skill";


    void Awake()
    {
        // コンポーネントを取得
        playerAnimator = GetComponent<Animator>();

        overrideController = new AnimatorOverrideController();
        overrideController.runtimeAnimatorController = playerAnimator.runtimeAnimatorController;
        playerAnimator.runtimeAnimatorController = overrideController;
    }

    public void ChangeSkillClip(AnimationClip overrideAnimationClip, float skillAnimationSpeed)
    {
        // ステートをキャッシュ
        AnimatorStateInfo[] layerInfo = new AnimatorStateInfo[playerAnimator.layerCount];
        for (int i = 0; i < playerAnimator.layerCount; i++)
        {
            layerInfo[i] = playerAnimator.GetCurrentAnimatorStateInfo(i);
        }

        // AnimationClipを差し替えて、強制的にアップデート
        // ステートがリセットされる
        overrideController[OVERRIDE_CLIP_NAME] = overrideAnimationClip;
        playerAnimator.Update(0.0f);

        // ステートを戻す
        for (int i = 0; i < playerAnimator.layerCount; i++)
        {
            playerAnimator.Play(layerInfo[i].fullPathHash, i, layerInfo[i].normalizedTime);
        }

        playerAnimator.SetFloat(PARAMETER_FLOAT_SKILL_SPEED, skillAnimationSpeed);
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

    public void SetTriggerSkill()
    {
        playerAnimator.SetTrigger(PARAMETER_TRIGGER_SKILL);
    }
}
