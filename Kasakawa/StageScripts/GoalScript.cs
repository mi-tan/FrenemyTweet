using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GoalScript : MonoBehaviour {

    [Inject]
    private StageSceneManager stageManager;

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーに衝突した場合
        if (other.gameObject.layer != (int)LayerManager.Layer.Player) { return; }

        // ステージクリア動作
        stageManager.ClearStage();

        // 自分を消す
        Destroy(gameObject);
    }

}
