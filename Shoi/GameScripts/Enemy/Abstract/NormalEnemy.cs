using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵（ボス以外）の基底クラス
/// </summary>
public class NormalEnemy : EnemyBase {

    [SerializeField, Tooltip("移動速度")]
    protected int moveSpeed;

}
