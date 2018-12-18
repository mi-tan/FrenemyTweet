using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UniRx.Triggers;
using System;

/// <summary>
/// ゲーム中の管理クラス
/// </summary>
public class MainGameManager : MonoBehaviour {

    // イベントを登録するインスタンス
    private Subject<Unit> startSubject = new Subject<Unit>();

    // サブジェクトを公開する
    public IObservable<Unit> OnGameStart
    {
        get { return startSubject; }
    }

    [SerializeField]
    [Header("サブシーン名のリスト")]
    private string[] subSceneNames;

    /// <summary>
    /// 経過時間をカウントする
    /// </summary>
    public float TimeCount { get; private set; } = 0f;

    // Use this for initialization
    void Start()
    {
        // サブシーンを読み込む
        foreach (var sceneName in subSceneNames)
        {
            SceneController.AddSceneAsync(sceneName);
        }

        // ゲーム開始時のイベントを実行
        startSubject.OnNext(Unit.Default);

        // 時間をカウントする(毎フレーム)
        (this).UpdateAsObservable()
        .Subscribe(_ => { TimeCount += Time.deltaTime; });
    }
}
