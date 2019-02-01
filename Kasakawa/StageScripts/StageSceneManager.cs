using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

public sealed class StageSceneManager : SingletonMonoBehaviour<StageSceneManager>
{

    [SerializeField]
    private string stageName = "ステージ";

    /// <summary>
    /// ステージの表示名
    /// </summary>
    public string StageDisplayName
    {
        get
        {
            return stageName;
        }
    }

    [Inject]
    private MainGameManager gameManager;

    [Inject]
    private StageEnemyManager enemyManager;

    public StageEnemyManager EnemyManager
    {
        get
        {
            return enemyManager;
        }
    }

    /// <summary>
    /// ステージマップのシーン名
    /// </summary>
    [SerializeField]
    private string stageMapSceneName = "";

    public string StageMapSceneName
    {
        get
        {
            return stageMapSceneName;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        // 全ての敵を無効化
        EnemyManager.DisableAllEnemy();
        // ゲーム開始時に敵を有効化する
        gameManager.OnGameStart.Subscribe(_ =>
        {
            EnemyManager.ActivateStartEnemy();
        })
            .AddTo(gameObject);
    }

    /// <summary>
    /// ステージクリア時の動作
    /// </summary>
    public void ClearStage()
    {
        GameParameterManager.Instance.JumpNextStage();
        //SceneController.ReloadSceneAsync();
    }
}
