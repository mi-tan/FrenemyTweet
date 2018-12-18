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
    [SerializeField]
    MainGameManager gameManager;

    [SerializeField]
    private Slider hpSlider;

    void Awake()
    {



    }

    private void Start()
    {
        InitializePlayerUI();
    }

    private void InitializePlayerUI()
    {
        
    }

}