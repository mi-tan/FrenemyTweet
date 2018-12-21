using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移動するインターフェース
/// </summary>
public interface IEnemyMove {


    void Move(Vector3 destination,float moveSpeed);
}
