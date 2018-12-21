using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

public sealed class StageSceneManager : MonoBehaviour {

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

    void Awake ()
    {
        // 全ての敵を無効化
        EnemyManager.DisableAllEnemy();
        // ゲーム開始時に敵を有効化する
        gameManager.OnGameStart.Subscribe(_ => {
            EnemyManager.ActivateStartEnemy();
        })
            .AddTo(gameObject);
    }

    /// <summary>
    /// ステージクリア時の動作
    /// </summary>
    public void ClearStage()
    {
        SceneController.ReloadSceneAsync();
    }
}
