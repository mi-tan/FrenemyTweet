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
    [SerializeField, Tooltip("移動速度")]
    public float enemyMoveSpeed = 3;
    [SerializeField, Tooltip("攻撃が当たった時に出エフェクト")]
    public GameObject hitEffect;
    [SerializeField,Tooltip("使用する武器")]
    public List<AttackCollision> useWeapon;



    public int getMaxHP { get { return maxHP; } }
    public bool AttackCollisionDebugFlag{ get { return attackCollisionDebugFlag; } }
}
