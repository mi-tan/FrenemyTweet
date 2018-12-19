using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    private PlayerStateManager playerStateManager;
    private PlayerAnimationManager playerAnimationManager;

    /// <summary>
    /// スキル番号
    /// </summary>
    private int skillNumber = 0;

    [SerializeField]
    private PlayerSkillBase[] skillList = new PlayerSkillBase[PlayerParameter.SKILL_QUANTITY];

    /// <summary>
    /// 入力中か
    /// </summary>
    private bool isInput = false;

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

    public PlayerSkillBase GetSelectSkill()
    {
        return skillList[SkillNumber];
    }


    void Awake()
    {
        // コンポーネントを取得
        playerStateManager = GetComponent<PlayerStateManager>();
        playerAnimationManager = GetComponent<PlayerAnimationManager>();
    }

    public void UpdateSkill(float inputActivateSkill, bool inputSelectSkill1, bool inputSelectSkill2, bool inputSelectSkill3)
    {
        // スキル切り替え
        ChangeSkill(
            inputSelectSkill1, 
            inputSelectSkill2, 
            inputSelectSkill3);

        //Debug.Log(skillNumber);

        if (inputActivateSkill >= 1)
        {
            if (isInput) { return; }

            // スキル発動
            if (skillList[SkillNumber] != null)
            {
                // プレイヤーの状態をスキル中に変更
                //playerStateManager.SetPlayerState(PlayerStateManager.PlayerState.SKILL);

                skillList[SkillNumber].ActivateSkill(transform);

                playerAnimationManager.ChangeSkillClip(skillList[SkillNumber].SkillAnimation);
                playerAnimationManager.SetTriggerSkill();
            }
            else
            {
                Debug.LogWarning("skillList["+ skillNumber + "] = null");
            }

            isInput = true;
        }
        else
        {
            isInput = false;
        }
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
        if (inputSelectSkill1) { SkillNumber = 0; }
        if (inputSelectSkill2) { SkillNumber = 1; }
        if (inputSelectSkill3) { SkillNumber = 2; }
    }
}
