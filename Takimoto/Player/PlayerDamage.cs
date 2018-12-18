using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private PlayerParameter playerParameter;


    void Awake()
    {
        playerParameter = GetComponent<PlayerParameter>();
    }

    public void Damage(int damage)
    {
        playerParameter.Hp -= damage;

        Debug.Log("プレイヤーに" + damage + "ダメージ");
        //Debug.Log("残りHP："+ playerParameter.Hp);
    }
}
