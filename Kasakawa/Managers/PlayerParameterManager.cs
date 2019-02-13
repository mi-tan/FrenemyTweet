using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParameterManager : SingletonMonoBehaviour<PlayerParameterManager> {

    [SerializeField]
    private PlayerParameter playerParameter;

    

    private int playerHP = 50;
    private int attackPower = 50;

    private float moveSpeed = 3.5f;

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

    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void SetPlayerAttack(int power)
    {
        attackPower = power;
    }

    public void SetPlayerHP(int hp)
    {
        playerHP = hp;
    }

    public void SetPlayerMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
