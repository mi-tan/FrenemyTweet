using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParameterManager : SingletonMonoBehaviour<GameParameterManager>
{

    private string stageSceneName = "";

    public string StageSceneName
    {
        get
        {
            return stageSceneName;
        }
    }

    public PlayerType SpawnPlayerType
    {
        get
        {
            return spawnPlayerType;
        }
    }

    [SerializeField]
    private PlayerType spawnPlayerType;

    public enum PlayerType
    {
        Sword = 0,
        Rifle
    }

    public void SetStageSceneName(string name)
    {
        stageSceneName = name;
    }

    public void SetPlayerType(PlayerType type)
    {
        spawnPlayerType = type;
    }
}
