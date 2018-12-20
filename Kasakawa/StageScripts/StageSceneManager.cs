using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

public class StageSceneManager : MonoBehaviour {

    [Inject]
    private MainGameManager gameManager;

    [SerializeField]
    private StageEnemyManager enemyManager;

	void Awake ()
    {
        // 全ての敵を無効化
        enemyManager.DisableAllEnemy();
        // ゲーム開始時に敵を有効化する
        gameManager.OnGameStart.Subscribe(_ => {
            enemyManager.ActivateStartEnemy();
        })
            .AddTo(gameObject);
    }
}
