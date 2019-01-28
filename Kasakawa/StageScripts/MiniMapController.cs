using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MiniMapController : MonoBehaviour {

    [Inject]
    private MainGameManager gameManager;

    [SerializeField]
    private PlayerUIManager playerUIManager;

    private void LateUpdate()
    {
        if (!playerUIManager) { Debug.LogWarning("ミニマップカメラにUIマネージャーを登録してください"); return; }

        if (!gameManager) { Debug.LogWarning("ゲームマネージャーが存在しません");return; }

        // プレイヤーの座標を取得する
        var movePos = gameManager.GetPlayer(playerUIManager.PlayerNum).transform.position;

        movePos.y = transform.position.y;

        transform.position = movePos;
    }
}
