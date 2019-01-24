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

    private int playerNum = 0;

    [Header("銃の弾数を表示するキャンバス")]
    [SerializeField]
    private GameObject gunCanvas;

    [Header("弾の弾数を表示するテキスト")]
    [SerializeField]
    private Text bulletText;

    [Header("弾の最大数を表示するテキスト")]
    [SerializeField]
    private Text bulletMaxText;

    [Header("武器のアイコン表示のイメージ")]
    [SerializeField]
    private Image weaponImage;

    private WeaponBase playerWeapon;

    void Awake()
    {

        // プレイヤーのHPが変化した場合、表示を更新する
        gameManager.ObserveEveryValueChanged(_ => gameManager.GetPlayer(playerNum).GetHp())
            .Subscribe(hp => { UpdateHPValue(hp); });

        // プレイヤーの選択スキルが変化した場合、表示を更新する
        gameManager.ObserveEveryValueChanged(_ => gameManager.GetPlayer(playerNum).GetSelectSkill())
            .Subscribe(skill => { UpdateSelectedSkillInfo(skill,gameManager.GetPlayer(playerNum).GetSkillNumber()); });

        // スキルクールタイムの表示更新
        this.UpdateAsObservable().Subscribe(_ => UpdateSkillCoolTime())
            .AddTo(gameObject);

    }

    private void Start()
    {

        playerWeapon = gameManager.GetPlayer(playerNum).GetWeaponBase();
        InitializePlayerUI();
    }

    /// <summary>
    /// UI初期化処理
    /// </summary>
    private void InitializePlayerUI()
    {
        int playerHP = gameManager.GetPlayer(playerNum).GetHp();

        hpSlider.maxValue = gameManager.GetPlayer(playerNum).GetMaxHp();
        UpdateHPValue(playerHP);

        var skillList = gameManager.GetPlayer(playerNum).GetSkillList();

        // スキルアイコンをUIに表示
        for(int i= 0; i < skillList.Length; i++)
        {
            UpdateSelectedSkillInfo(skillList[i], i);
        }

        // 現在のスキル情報を表示する
        UpdateSelectedSkillInfo(gameManager.GetPlayer(playerNum).GetSelectSkill(),gameManager.GetPlayer(playerNum).GetSkillNumber());

        UpdateUserIconImage();

        UpdateUserIDText();

        // 銃用キャンバスの初期設定をする
        InitGunCanvas();

        // 武器アイコンを更新する
        UpdateWeaponIcon();

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
        var skillCoolTimes = gameManager.GetPlayer(playerNum).GetSkillCoolTimes();

        // スキルのクールタイムを取得する
        var skillBaseTimes = gameManager.GetPlayer(playerNum).GetSkillBaseCoolTimes();

        for (int i = 0;i < skillIconArray.Length; i++)
        {
            // 現在のクールタイムの経過時間を取得
            var skillCoolTime = skillBaseTimes[i] - skillCoolTimes[i];

            // クールタイムをUIに反映する
            skillIconArray[i].IconImage.fillAmount = skillCoolTime/ skillBaseTimes[i];
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

    /// <summary>
    /// ツイッターのID表示を更新する
    /// </summary>
    private void UpdateUserIDText()
    {
        var userID = "@"+TwitterParameterManager.Instance.UserID;

        userIDText.text = userID;
    }

    /// <summary>
    /// 銃用キャンバスの表示を初期化する
    /// </summary>
    private void InitGunCanvas()
    {
        if (!gunCanvas) { Debug.LogWarning("gunCanvasが設定されていません"); return; }
    }

    /// <summary>
    /// 武器のアイコン表示を更新する
    /// </summary>
    private void UpdateWeaponIcon()
    {
        if (!weaponImage) { Debug.LogWarning("weaponImageが設定されていません"); return; }

        var weaponIcon = gameManager.GetPlayer(playerNum).GetWeaponIcon();

        if (!weaponIcon) { Debug.LogWarning("プレイヤーの武器アイコンが設定されていません"); return; }

        weaponImage.sprite = weaponIcon;
    }

    /// <summary>
    /// 弾数の表示を更新する
    /// </summary>
    private void UpdateBulletNumber(int bulletNumber)
    {
        if (!bulletText) { Debug.LogWarning("bulletTextが設定されていません"); return; }
    }
    
}