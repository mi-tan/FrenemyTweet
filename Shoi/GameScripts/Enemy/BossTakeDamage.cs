using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTakeDamage : MonoBehaviour, IDamage {

    [SerializeField]
    private BossEnemyController bossEnemyController;

    public void TakeDamage(int damage)
    {
        bossEnemyController.TakeDamage(damage);
    }
}
