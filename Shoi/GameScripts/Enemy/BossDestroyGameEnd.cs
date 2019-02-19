using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BossDestroyGameEnd : MonoBehaviour {

    [SerializeField]
    private float gameEndWaitTime;
    private float elapsedTime;

    [Inject]
    MainGameManager gameManager;

	
	private void Update () {

        elapsedTime += Time.deltaTime;
        Debug.Log("スポーン待機中");

        if (elapsedTime < gameEndWaitTime) { return; }
        elapsedTime = 0;
        gameManager.EndGame();
    }
}
