using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UnityEngine.UI;

/// <summary>
/// プレイヤー関連のUIを管理するクラス
/// </summary>
public class PlayerUIManager : MonoBehaviour
{
    [Inject]
    private MainGameManager gameManager;

    [SerializeField]
    private Slider hpSlider;

    void Awake()
    {

        // プレイヤーのHPが変化した場合、表示を更新する
        gameManager.ObserveEveryValueChanged(_ => gameManager.player.GetHp())
            .Subscribe(hp => { ChangeHPValue(hp); });

    }

    private void Start()
    {
        InitializePlayerUI();
    }

    private void InitializePlayerUI()
    {
        int playerHP = gameManager.player.GetHp();

        Debug.Log(playerHP);

        hpSlider.maxValue = gameManager.player.GetMaxHp();
        hpSlider.value = playerHP;
    }

    private void ChangeHPValue(int hp)
    {
        hpSlider.value = hp;
    }

}