using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Stage")]
public class GameStageData : ScriptableObject {

    [Header("ステージのシーン名リスト")]
    [SerializeField]
    private string[] stageList;

    public string[] StageList
    {
        get
        {
            return stageList;
        }
    }
}
