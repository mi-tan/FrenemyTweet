using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UnityEngine.UI;

/// <summary>
/// プレイヤー関連のUIを管理するクラス
/// </summary>
public sealed class PlayerUIManager : MonoBehaviour
{
    [Inject]
    private MainGameManager gameManager;

    [SerializeField]
    private Slider hpSlider;

    [SerializeField]
    private Image[] skillIconImage;

    [SerializeField]
    private Transform selectSkillUI;

    [SerializeField]
    private RawImage userIconImage;

    [SerializeField]
    private Text userIDText;

    void Awake()
    {

        // プレイヤーのHPが変化した場合、表示を更新する
        gameManager.ObserveEveryValueChanged(_ => gameManager.player.GetHp())
            .Subscribe(hp => { UpdateHPValue(hp); });

        // プレイヤーの選択スキルが変化した場合、表示を更新する
        gameManager.ObserveEveryValueChanged(_ => gameManager.player.GetSelectSkill())
            .Subscribe(skill => { UpdateSelectedSkillInfo(skill,gameManager.player.GetSkillNumber()); });

    }

    private void Start()
    {
        InitializePlayerUI();
    }

    /// <summary>
    /// UI初期化処理
    /// </summary>
    private void InitializePlayerUI()
    {
        int playerHP = gameManager.player.GetHp();

        hpSlider.maxValue = gameManager.player.GetMaxHp();
        UpdateHPValue(playerHP);

        var skillList = gameManager.player.GetSkillList();

        // スキルアイコンをUIに表示
        for(int i= 0; i < skillList.Length; i++)
        {
            UpdateSelectedSkillInfo(skillList[i], i);
        }

        // 現在のスキル情報を表示する
        UpdateSelectedSkillInfo(gameManager.player.GetSelectSkill(),gameManager.player.GetSkillNumber());

        UpdateUserIconImage();

        UpdateUserIDText();

    }

    /// <summary>
    /// プレイヤーのHPをUIに反映する
    /// </summary>
    /// <param name="hp"></param>
    private void UpdateHPValue(int hp)
    {
        hpSlider.value = hp;
    }

    /// <summary>
    /// プレイヤーが選択しているスキルの情報をUIに反映する
    /// </summary>
    /// <param name="skill"></param>
    private void UpdateSelectedSkillInfo(PlayerSkillBase skill,int skillNum)
    {
        //skillNameText.text = skill.SkillName;

        if (!skill) {
            Debug.LogWarning("SkillがNull");
            return;
        }

        skillIconImage[skillNum].sprite = skill.SkillIcon;

        selectSkillUI.transform.position = skillIconImage[skillNum].transform.position;
    }

    private void UpdateUserIconImage()
    {
        var iconTexture = TwitterParameterManager.Instance.IconTexture;

        if (!iconTexture) {
            Debug.LogWarning("Twitterアイコンが読み込まれていません");
            return;
        }

        userIconImage.texture = iconTexture;
    }

    private void UpdateUserIDText()
    {
        var userID = "@"+TwitterParameterManager.Instance.UserID;

        userIDText.text = userID;
    }

}