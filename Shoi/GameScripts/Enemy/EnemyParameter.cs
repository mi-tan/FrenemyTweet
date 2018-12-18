using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParameter : MonoBehaviour {

    [SerializeField, Tooltip("体力")]
    public int hp = 100;
    [SerializeField, Tooltip("攻撃力")]
    public int power = 10;
    [SerializeField, Tooltip("移動速度")]
    public float enemyMoveSpeed = 3;
}
