using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private PlayerProvider playerProvider;


    void Awake()
    {
        playerProvider = GetComponent<PlayerProvider>();
    }

    public void Damage(int damage)
    {
        playerProvider.SetHp(playerProvider.GetHp() - damage);

        Debug.Log("プレイヤーに" + damage + "ダメージ");
        //Debug.Log("残りHP："+ playerParameter.Hp);
    }
}
