using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    private PlayerStateManager playerStateManager;
    private PlayerAnimationManager playerAnimationManager;
    private PlayerProvider playerProvider;

    private int skillNumber = 0;
    /// <summary>
    /// スキル番号
    /// </summary>
    public int SkillNumber
    {
        get
        {
            return skillNumber;
        }
        private set
        {
            skillNumber = value;
        }
    }

    [SerializeField]
    private PlayerSkillBase[] skillList = new PlayerSkillBase[PlayerParameter.SKILL_QUANTITY];
    public PlayerSkillBase[] GetSkillList()
    {
        return skillList;
    }
    public PlayerSkillBase GetSelectSkill()
    {
        return skillList[skillNumber];
    }
    public void SetSkill(PlayerSkillBase skill, int num)
    {
        skillList[num] = skill;
    }

    /// <summary>
    /// 攻撃中か
    /// </summary>
    private bool isAttack = false;
    /// <summary>
    /// 生成したか
    /// </summary>
    private bool isCreation = false;
    /// <summary>
    /// 入力中か
    /// </summary>
    private bool isInput = false;

    /// <summary>
    /// 攻撃角度
    /// </summary>
    private Quaternion attackQuaternion;

    private float[] skillCoolTimes = new float[PlayerParameter.SKILL_QUANTITY];
    public float[] GetSkillCoolTimes()
    {
        return skillCoolTimes;
    }

    [SerializeField]
    private GameObject testSkillHitEffect;


    void Awake()
    {
        // コンポーネントを取得
        playerStateManager = GetComponent<PlayerStateManager>();
        playerAnimationManager = GetComponent<PlayerAnimationManager>();
        playerProvider = GetComponent<PlayerProvider>();
    }

    private void Start()
    {
        // 仮エフェクト設定
        if (testSkillHitEffect == null) { return; }

        for (int i = 0; i < skillList.Length; i++)
        {
            if(skillList[i] == null) { continue; }

            skillList[i].SetHitEffect(testSkillHitEffect);
        }
    }

    public void UpdateSkill(float inputActivateSkill, bool inputSelectSkill1, bool inputSelectSkill2, bool inputSelectSkill3)
    {
        if (playerStateManager.GetPlayerState() == PlayerStateManager.PlayerState.DEATH) { return; }

        // スキル切り替え
        ChangeSkill(
            inputSelectSkill1,
            inputSelectSkill2,
            inputSelectSkill3);

        if (isAttack)
        {
            if (!isCreation)
            {
                Vector3 attackDirection = Vector3.Scale(
                    playerProvider.GetMainCamera().transform.forward, new Vector3(1, 0, 1)).normalized;
                attackQuaternion = Quaternion.LookRotation(attackDirection);
            }

            // 攻撃方向に向く
            FaceAttack(attackQuaternion);
        }

        UpdateCoolTime();

        // 現在のプレイヤーの状態が行動可能ではなかったら、この先の処理を行わない
        if (playerStateManager.GetPlayerState() != PlayerStateManager.PlayerState.ACTABLE) { return; }

        if (inputActivateSkill >= 1)
        {
            if (isInput) { return; }

            PlayerSkillBase skill = skillList[SkillNumber];

            // スキルがあったら
            if (skill != null)
            {
                if (skillCoolTimes[SkillNumber] <= 0)
                {
                    // プレイヤーの状態をスキル中に変更
                    playerStateManager.SetPlayerState(PlayerStateManager.PlayerState.SKILL);

                    isAttack = true;

                    // スキル発動アニメーション再生
                    playerAnimationManager.ChangeSkillClip(skill.SkillAnimation, skill.SkillAnimationSpeed);
                    playerAnimationManager.SetTriggerSkill();

                    // スキル生成
                    StartCoroutine(CreateSkill(skill.SkillCreationTime, skill));

                    skillCoolTimes[SkillNumber] = skill.SkillCoolTime;

                    // スキル硬直解除
                    StartCoroutine(RecoverySkill(skill.SkillRecoveryTime));

                    skill.SetPlayerAttackPower(playerProvider.GetPlayerAttackPower());
                }
                else
                {
                    //Debug.Log(skillList[SkillNumber].SkillName + "：クールタイム(" + skillCoolTimes[SkillNumber] + "秒)");
                }
            }
            else
            {
                Debug.LogWarning("skillList["+ SkillNumber + "] = null");
            }

            isInput = true;
        }
        else
        {
            isInput = false;
        }
    }

    /// <summary>
    /// 攻撃方向に向く
    /// </summary>
    /// <param name="attackQuaternion">攻撃角度</param>
    void FaceAttack(Quaternion attackQuaternion)
    {
        // 攻撃角度に向いていなかったら
        if (transform.rotation != attackQuaternion)
        {
            // 攻撃角度に向く
            transform.rotation = attackQuaternion;
        }
    }

    /// <summary>
    /// スキル生成
    /// </summary>
    /// <param name="skillCreationTime">スキル生成時間</param>
    /// <param name="skill"></param>
    /// <returns></returns>
    private IEnumerator CreateSkill(float skillCreationTime, PlayerSkillBase skill)
    {
        yield return new WaitForSeconds(skillCreationTime);

        isCreation = true;

        if (playerStateManager.GetPlayerState() == PlayerStateManager.PlayerState.DEATH) { yield break; }

        skill.ActivateSkill(playerProvider, skill.SkillCreationPos);
    }

    /// <summary>
    /// スキル硬直解除
    /// </summary>
    /// <param name="recoveryTime">硬直解除時間</param>
    /// <returns></returns>
    private IEnumerator RecoverySkill(float recoveryTime)
    {
        yield return new WaitForSeconds(recoveryTime);

        // 初期化
        isAttack = false;
        isCreation = false;

        // プレイヤーの状態を行動可能に変更
        playerStateManager.SetPlayerState(PlayerStateManager.PlayerState.ACTABLE);
    }

    /// <summary>
    /// スキル切り替え
    /// </summary>
    /// <param name="inputSelectNormalAttack">通常攻撃切り替え入力</param>
    /// <param name="inputSelectSkill1">スキル1切り替え入力</param>
    /// <param name="inputSelectSkill2">スキル2切り替え入力</param>
    /// <param name="inputSelectSkill3">スキル3切り替え入力</param>
    void ChangeSkill(bool inputSelectSkill1, bool inputSelectSkill2, bool inputSelectSkill3)
    {
        if (inputSelectSkill1)
        {
            if(GetSkillList()[0] == null) { return; }
            SkillNumber = 0;
        }
        if (inputSelectSkill2)
        {
            if (GetSkillList()[1] == null) { return; }
            SkillNumber = 1;
        }
        if (inputSelectSkill3)
        {
            if (GetSkillList()[2] == null) { return; }
            SkillNumber = 2;
        }
    }

    void UpdateCoolTime()
    {
        for(int i = 0; i < skillCoolTimes.Length; i++)
        {
            if (skillCoolTimes[i] > 0)
            {
                skillCoolTimes[i] -= Time.deltaTime;
            }
            else
            {
                skillCoolTimes[i] = 0f;
            }
        }
    }
}
