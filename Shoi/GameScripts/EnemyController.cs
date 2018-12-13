using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵（ボス以外）の動きを制御するクラス
/// </summary>
public class EnemyController : NormalEnemy {

    private IMove iMove;

    int x = 0;
    int y = 0;

	private void Start () {
		
	}
	
	private void Update () {
        iMove.Move(x, y);
	}
}
