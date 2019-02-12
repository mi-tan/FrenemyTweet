using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UniRx.Triggers;
using UniRx.Async;
using System;
using Photon.Pun;

/// <summary>
/// ゲーム中の管理クラス
/// </summary>
public class MainGameManager : MonoBehaviour
{

    [SerializeField]
    private Camera playerCamera;

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
    /// 必要なシーンがすべて読み込まれた時に呼ばれる
    /// </summary>
    public Subject<Unit> OnLoadSceneCompleted { get; } = new Subject<Unit>();

    public Subject<Unit> OnGameOver { get; } = new Subject<Unit>();

    public Subject<Unit> OnGameEnd { get; } = new Subject<Unit>();

    [Inject]
    DiContainer container;

    /// <summary>
    /// ステージ開始の待ち時間
    /// </summary>
    [SerializeField]
    private float startWaitTime = 0.5f;

    private PlayerProvider[] players = new PlayerProvider[MAX_PLAYER];

    [Header("PlayerTypeに対応するプレハブ名を登録")]
    [SerializeField]
    private string[] playerPrefabNames;

    [Header("PlayerTypeの確認用(処理には関係なし)")]
    [SerializeField]
    private GameParameterManager.PlayerType playerType;

    private const string Start_Pos_Name = "PlayerStartPosition";

    private const int MAX_PLAYER = 1;

    [SerializeField]
    private Animator loadCanvasAnimator;

    private const string LOAD_ANIM_END_PARAM = "End";

    private void Awake()
    {

        players[0] = PhotonNetwork.Instantiate(playerPrefabNames[(int)GameParameterManager.Instance.SpawnPlayerType], Vector3.zero, Quaternion.identity, 0, null).GetComponent<PlayerProvider>();

        container.InjectGameObject(players[0].gameObject);

        players[0].SetMainCamera(playerCamera);

        // プレイヤーのパラメータを設定する
        InitPlayerParameter();
    }

    // Use this for initialization
    private async void Start()
    {
        // サブシーンを読み込む
        foreach (var sceneName in subSceneNames)
        {
            await SceneController.AddSceneAsync(sceneName);
        }

        // ステージを読み込む
        await SceneController.AddSceneAsync(GameParameterManager.Instance.StageSceneName);

        // ステージのマップを読み込む
        await SceneController.AddSceneAsync(StageSceneManager.Instance.StageMapSceneName);

        // シーン読み込み完了イベントを発行
        OnLoadSceneCompleted.OnNext(Unit.Default);

        OnLoadSceneCompleted.Dispose();

        // ロードキャンバスをフェードさせる
        loadCanvasAnimator.SetTrigger(LOAD_ANIM_END_PARAM);

        InitPlayerPosition();

        //// 1フレーム待ってからプレイヤーを初期位置に移動する
        //Observable.TimerFrame(2).Subscribe(_ => InitPlayerPosition());


        // ゲーム開始まで遅延処理する
        Observable.Timer(System.TimeSpan.FromSeconds(startWaitTime))
            .Subscribe(_ =>
            {

                // ゲーム開始時のイベントを実行
                startSubject.OnNext(Unit.Default);

                startSubject.Dispose();

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

            // 移動速度をセットする
            player.SetMoveSpeed(PlayerParameterManager.Instance.MoveSpeed);

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
        if (num >= players.Length)
        {
            Debug.LogWarning("その番号のプレイヤーは存在しません");
        }
        return players[num];
    }

    public PlayerProvider[] GetPlayerArray()
    {
        return players;
    }

    public void EndGame()
    {
        //カーソル表示
        Cursor.visible = true;
        // マウスのロックを解除
        Cursor.lockState = CursorLockMode.None;

        OnGameEnd.OnNext(Unit.Default);
    }

    public void OnDeathPlayer(PlayerProvider player)
    {
        //カーソル表示
        Cursor.visible = true;
        // マウスのロックを解除
        Cursor.lockState = CursorLockMode.None;

        OnGameOver.OnNext(Unit.Default);
    }
}
