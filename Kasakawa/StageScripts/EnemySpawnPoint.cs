using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour {

    [SerializeField]
    private StageEnemyManager enemyManager;

    [Header("出現する敵の番号")]
    [SerializeField]
    private int spawnRegionNum;

    private void OnTriggerEnter(Collider other)
    {
        enemyManager.ActivateEnemy(spawnRegionNum);
    }
}
