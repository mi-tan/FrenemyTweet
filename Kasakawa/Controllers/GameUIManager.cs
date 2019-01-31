using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [Inject]
    private MainGameManager gameManager;

    [SerializeField]
    private GameObject startUI;

    [SerializeField]
    private float startUITime = 1f;

    void Awake()
    {
        startUI.SetActive(false);

        // ゲーム開始時に開始時UIを登録する
        gameManager.OnGameStart.Subscribe(_ => {
            ActiveStartUI();

            //// 一定時間後に開始時UIを非表示にする
            //Observable.Timer(System.TimeSpan.FromSeconds(startUITime))
            //    .Subscribe(x => DisableStartUI())
            //    .AddTo(gameObject);

        })
            .AddTo(gameObject);
        
    }

    /// <summary>
    /// ゲーム開始時のUIを有効化(UI演出はUI側で制御)
    /// </summary>
    private void ActiveStartUI()
    {
        startUI.SetActive(true);
    }

    /// <summary>
    /// ゲーム開始時のUIを無効化
    /// </summary>
    private void DisableStartUI()
    {
        startUI.SetActive(false);
    }

}