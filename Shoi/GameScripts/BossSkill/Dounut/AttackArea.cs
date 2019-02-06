using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AttackArea : MonoBehaviour {

    [Inject]
    protected MainGameManager gameManager;

    /// <summary>
    /// 衝突しているプレイヤー
    /// </summary>
    private List<GameObject> acquisitionPlayerList = new List<GameObject>();

    public List<GameObject> GetAcquisitionPlayerList
    {
        get { return acquisitionPlayerList; }
    }

    public GameObject AddAcquisitonPlayerList
    {
        set { acquisitionPlayerList.Add(value); }
    }
    public GameObject RemoveAcquisitonPlayerList
    {
        set { acquisitionPlayerList.Remove(value); }
    }
}
