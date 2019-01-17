using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParameterManager : SingletonMonoBehaviour<PlayerParameterManager> {

    [SerializeField]
    private PlayerParameter playerParameter;

    private int playerHP = 10;

    private int attackPower = 10;

    public int PlayerHP
    {
        get
        {
            return playerHP;
        }
    }

    public int AttackPower
    {
        get
        {
            return attackPower;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void CalcStatus()
    {
        // HPをランダムに決定する
        playerHP = UnityEngine.Random.Range(0, 100);

        // 攻撃力をランダムに決定する
        attackPower = UnityEngine.Random.Range(0, 100);
    }
}
