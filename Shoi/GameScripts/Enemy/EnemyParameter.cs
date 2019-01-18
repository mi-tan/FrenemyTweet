using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParameter : MonoBehaviour {

    [SerializeField]
    private bool attackCollisionDebugFlag = false;

    [SerializeField,Tooltip("最大体力")]
    private int maxHP = 100;
    [SerializeField, Tooltip("体力")]
    public int hp = 100;
    [SerializeField, Tooltip("攻撃力")]
    public int power = 10;

    [SerializeField, Tooltip("歩く移動速度")]
    public float enemyWalkSpeed = 2;
    [SerializeField, Tooltip("移動速度")]
    public float enemyRunSpeed = 4;
    [SerializeField, Tooltip("攻撃時のエフェクト")]
    public GameObject attackEffect;
    [SerializeField, Tooltip("被ダメージ時のエフェクト")]
    public GameObject damageEffect;
    [SerializeField,Tooltip("使用する武器")]
    public List<AttackCollision> useWeapon;
    [SerializeField,Tooltip("攻撃を開始する距離")]
    public float attackDistance = 1;
    [SerializeField, Tooltip("プレイヤーを追いかけ始める距離")]
    public float chaseDistance = 9f;
    [SerializeField,Tooltip("見回り時の待ち時間")]
    public float waitTime = 4;
    [SerializeField,Tooltip("攻撃後の硬直時間")]
    public float freezeTime = 1.5f;
    [SerializeField, Tooltip("巡回範囲")]
    public int movingRange = 5;
    [SerializeField]
    public float attackAngle = 20f;


    public int getMaxHP { get { return maxHP; } }
    public bool AttackCollisionDebugFlag{ get { return attackCollisionDebugFlag; } }
}
