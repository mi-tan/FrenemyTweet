using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UniRx.Triggers;
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
    private SkillIconImage[] skillIconArray;

    [SerializeField]
    private Transform selectSkillUI;

    [SerializeField]
    private RawImage userIconImage;

    [SerializeField]
    private Text userIDText;

    [System.Serializable]
    private class SkillIconImage
    {
        [SerializeField]
        private Image iconImage;

        public Image IconImage
        {
            get
            {
                return iconImage;
            }
        }

        [SerializeField]
        private Image iconBackGroundImage;

        public Image IconBackGroundImage
        {
            get
            {
                return iconBackGroundImage;
            }
        }
    }

    void Awake()
    {

        // プレイヤーのHPが変化した場合、表示を更新する
        gameManager.ObserveEveryValueChanged(_ => gameManager.player.GetHp())
            .Subscribe(hp => { UpdateHPValue(hp); });

        // プレイヤーの選択スキルが変化した場合、表示を更新する
        gameManager.ObserveEveryValueChanged(_ => gameManager.player.GetSelectSkill())
            .Subscribe(skill => { UpdateSelectedSkillInfo(skill,gameManager.player.GetSkillNumber()); });

        // スキルクールタイムの表示更新
        this.UpdateAsObservable().Subscribe(_ => UpdateSkillCoolTime())
            .AddTo(gameObject);

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

        // アイコン画像を変更する
        skillIconArray[skillNum].IconImage.sprite = skill.SkillIcon;

        // アイコンの背景画像を変更する
        skillIconArray[skillNum].IconBackGroundImage.sprite = skill.SkillIcon;

        selectSkillUI.transform.position = skillIconArray[skillNum].IconImage.transform.position;
    }

    private void UpdateSkillCoolTime()
    {

        // スキルクールタイムの残り時間を取得する
        var skillCoolTimes = gameManager.player.GetSkillCoolTimes();

        // スキルのクールタイムを取得する
        var skillBaseTimes = gameManager.player.GetSkillBaseCoolTimes();

        for (int i = 0;i < skillIconArray.Length; i++)
        {
            // 現在のクールタイムの経過時間を取得
            var skillCoolTime = skillBaseTimes[i] - skillCoolTimes[i];

            // クールタイムをUIに反映する
            skillIconArray[i].IconImage.fillAmount = skillBaseTimes[i]/skillCoolTime;
        }
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