using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(PhotonView))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerSkillBase))]

/// <summary>
/// プレイヤーのアニメーションを管理するクラス
/// </summary>
public class PlayerAnimationManager : MonoBehaviour, IPunObservable
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

    private const string PARAMETER_FLOAT_VERTICAL = "Vertical";
    private const string PARAMETER_FLOAT_HORIZONTAL = "Horizontal";

    private const string PARAMETER_TRIGGER_RELOAD = "Reload";
    private const string PARAMETER_TIRGGER_DODGE = "Dodge";
    private const string PARAMETER_TRIGGER_DEATH = "Death";


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

    public void SetFloatVertical(float value)
    {
        playerAnimator.SetFloat(PARAMETER_FLOAT_VERTICAL, value);
    }

    public void SetFloatHorizontal(float value)
    {
        playerAnimator.SetFloat(PARAMETER_FLOAT_HORIZONTAL, value);
    }

    public void SetTriggerReload()
    {
        playerAnimator.SetTrigger(PARAMETER_TRIGGER_RELOAD);
    }

    public void SetTriggerDodge()
    {
        playerAnimator.SetTrigger(PARAMETER_TIRGGER_DODGE);
    }

    public void SetTriggerDeath()
    {
        playerAnimator.SetTrigger(PARAMETER_TRIGGER_DEATH);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 送信
        if (stream.IsWriting)
        {
            PlayerSkillBase playerSkillBase = GetComponent<PlayerSkillBase>();
            stream.SendNext(playerSkillBase.SkillAnimation);
            stream.SendNext(playerSkillBase.SkillAnimationSpeed);
        }
        //　受信
        else
        {
            PlayerSkillBase playerSkillBase = GetComponent<PlayerSkillBase>();
            playerSkillBase.SkillAnimation = (AnimationClip)stream.ReceiveNext();
            playerSkillBase.SkillAnimationSpeed = (float)stream.ReceiveNext();
        }
    }
}
