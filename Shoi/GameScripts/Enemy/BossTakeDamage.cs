using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class BossTakeDamage : MonoBehaviour, IDamage {

    [SerializeField]
    private BossEnemyController bossEnemyController;

    public void TakeDamage(int damage)
    {
        bossEnemyController.TakeDamage(damage);
    }
    public void TakeDamage(int damage, PhotonView photonView)
    {
        bossEnemyController.TakeDamage(damage);
    }
}
