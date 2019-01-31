using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class PlayerSkillBase : ScriptableObject
{
    [Header("スキル名")]
    [SerializeField]
    private string skillName;
    /// <summary>
    /// スキル名
    /// </summary>
    public string SkillName
    {
        get
        {
            return skillName;
        }
    }

    [Header("スキルアイコン")]
    [SerializeField]
    private Sprite skillIcon;
    /// <summary>
    /// スキルアイコン
    /// </summary>
    public Sprite SkillIcon
    {
        get
        {
            return skillIcon;
        }
    }

    [Header("スキルアニメーション")]
    [SerializeField]
    private AnimationClip skillAnimation;
    /// <summary>
    /// スキル発動アニメーション
    /// </summary>
    public AnimationClip SkillAnimation
    {
        get
        {
            return skillAnimation;
        }
    }


    [Header("スキルアニメーション速度")]
    [SerializeField]
    private float skillAnimationSpeed;
    /// <summary>
    /// スキル発動アニメーション速度
    /// </summary>
    public float SkillAnimationSpeed
    {
        get
        {
            return skillAnimationSpeed;
        }
    }

    [Header("スキル生成時間")]
    [SerializeField]
    private float skillCreationTime;
    /// <summary>
    /// スキル生成時間
    /// </summary>
    public float SkillCreationTime
    {
        get
        {
            return skillCreationTime;
        }
    }

    [Header("スキル生成位置")]
    [SerializeField]
    private Vector3 skillCreationPos;
    /// <summary>
    /// スキル生成位置
    /// </summary>
    public Vector3 SkillCreationPos
    {
        get
        {
            return skillCreationPos;
        }
    }

    [Header("スキルプレハブ")]
    /// <summary>
    /// スキルプレハブ
    /// </summary>
    [SerializeField]
    protected AttackCollision skillPrefab;

    [Header("スキル硬直解除時間")]
    [SerializeField]
    private float skillRecoveryTime;
    /// <summary>
    /// スキル硬直解除時間
    /// </summary>
    public float SkillRecoveryTime
    {
        get
        {
            return skillRecoveryTime;
        }
    }

    [Header("スキルクールタイム")]
    [SerializeField]
    private float skillCoolTime;
    /// <summary>
    /// スキルクールタイム
    /// </summary>
    public float SkillCoolTime
    {
        get
        {
            return skillCoolTime;
        }
    }

    public abstract void ActivateSkill(Transform playerTrans, Vector3 skillCreationPos, Camera mainCamera, PlayerCamera playerCamera);

    protected int playerAttackPower = 0;
    public void SetPlayerAttackPower(int value)
    {
        playerAttackPower = value;
    }

    protected GameObject hitEffect;
    public void SetHitEffect(GameObject effect)
    {
        hitEffect = effect;
    }
}
