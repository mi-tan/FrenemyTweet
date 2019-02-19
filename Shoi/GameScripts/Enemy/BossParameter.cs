using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossParameter : MonoBehaviour {

    [SerializeField, Tooltip("最大体力")]
    private int maxHP = 100;
    [SerializeField, Tooltip("体力")]
    public int hp = 100;
    [SerializeField, Tooltip("攻撃力")]
    public int power = 10;
    [SerializeField, Tooltip("死亡時のアニメーション")]
    public AnimationClip deathAnimation;
    [SerializeField]
    public GameObject destroyObject;

    //[SerializeField, Tooltip("使用する武器")]
    //public List<AttackCollision> useWeapon;
    //[SerializeField, Tooltip("攻撃後の硬直時間")]
    //public int freezeTime = 3;

}
