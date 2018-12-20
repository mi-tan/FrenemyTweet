using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEnemyManager : MonoBehaviour {

    [SerializeField]
    private EnemyRegion[] enemyDataArray;

    [System.Serializable]
    public class EnemyRegion
    {
        [SerializeField]
        private GameObject[] enemyArray;

        //[SerializeField]
        //public bool isStart = false;

        public GameObject[] EnemyArray
        {
            get
            {
                return enemyArray;
            }
        }
    }

    /// <summary>
    /// 開始時にいる敵を有効化
    /// </summary>
    public void ActivateStartEnemy()
    {
        ActivateEnemy(0);
    }

    public void ActivateEnemy(int regionNum)
    {
        if(regionNum > enemyDataArray.Length - 1)
        {
            Debug.LogWarning($"敵のリストの要素数を超えています index : {regionNum}");
            return;
        }

        // 敵の集団を取得する
        foreach(var enemyRegion in enemyDataArray[regionNum].EnemyArray)
        {
            // 敵を有効化する
            enemyRegion.SetActive(true);
        }
    }

    public void DisableAllEnemy()
    {
        for(int i=0;i < enemyDataArray.Length; i++)
        {
            DisableEnemy(i);
        }
    }

    private void DisableEnemy(int regionNum)
    {

        if (regionNum > enemyDataArray.Length - 1)
        {
            Debug.LogWarning($"敵のリストの要素数を超えています index : {regionNum}");
            return;
        }

        // 敵の集団を取得する
        foreach (var enemyRegion in enemyDataArray[regionNum].EnemyArray)
        {
            // 敵を無効化する
            enemyRegion.SetActive(false);
            Debug.Log($"{enemyRegion}を無効化");
        }
    }
}
