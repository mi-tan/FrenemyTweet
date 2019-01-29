using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UniRx.Triggers;
using UniRx.Async;
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

    /// <summary>
    /// ステージ開始の待ち時間
    /// </summary>
    [SerializeField]
    private float startWaitTime = 0.5f;

    private PlayerProvider[] players = new PlayerProvider[MAX_PLAYER];

    [Header("PlayerTypeに対応するプレハブを登録")]
    [SerializeField]
    private PlayerProvider[] playerPrefabs;

    [Header("PlayerTypeの確認用(処理には関係なし)")]
    [SerializeField]
    private GameParameterManager.PlayerType playerType;

    private const string Start_Pos_Name = "PlayerStartPosition";

    private const int MAX_PLAYER = 1;

    private void Awake()
    {
        // プレイヤータイプに応じたプレハブを生成する
        players[0] = Instantiate(playerPrefabs[(int)GameParameterManager.Instance.SpawnPlayerType]);
    }

    // Use this for initialization
    private async void Start()
    {
        // サブシーンを読み込む
        foreach (var sceneName in subSceneNames)
        {
           await SceneController.AddSceneAsync(sceneName);
        }

        // プレイヤーのパラメータを設定する
        InitPlayerParameter();

        InitPlayerPosition();

        //// 1フレーム待ってからプレイヤーを初期位置に移動する
        //Observable.TimerFrame(2).Subscribe(_ => InitPlayerPosition());


        // ゲーム開始まで遅延処理する
        Observable.Timer(System.TimeSpan.FromSeconds(startWaitTime))
            .Subscribe(_ =>
            {
                
                // ゲーム開始時のイベントを実行
                startSubject.OnNext(Unit.Default);

                // 時間をカウントする(毎フレーム)
                (this).UpdateAsObservable()
                .Subscribe(x => { TimeCount += Time.deltaTime; })
                .AddTo(gameObject);
            })
            .AddTo(gameObject);

    }

    private void InitPlayerParameter()
    {

        foreach (var player in players)
        {
            // 最大HPをセットする
            player.SetMaxHp(PlayerParameterManager.Instance.PlayerHP);

            // HPをセットする
            player.SetHp(PlayerParameterManager.Instance.PlayerHP);

            // 基礎攻撃力をセットする
            player.SetBasicAttackPower(PlayerParameterManager.Instance.AttackPower);

            // 現在の攻撃力をセットする
            player.SetPlayerAttackPower(PlayerParameterManager.Instance.AttackPower);
        }

        
    }

    private void InitPlayerPosition()
    {

        var startPositionObject = GameObject.Find(Start_Pos_Name);

        if (!startPositionObject) { Debug.LogWarning("プレイヤーの初期位置用オブジェクトがありません。"); return; }

        var startPosition = startPositionObject.transform.position;

        // プレイヤーを初期位置に移動させる
        GetPlayer(0).transform.position = startPosition;
    }

    /// <summary>
    /// プレイヤーを取得する
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    public PlayerProvider GetPlayer(int num)
    {
        if(num >= players.Length)
        {
            Debug.LogWarning("その番号のプレイヤーは存在しません");
        }
        return players[num];
    }

    public PlayerProvider[] GetPlayerArray()
    {
        return players;
    }
}
