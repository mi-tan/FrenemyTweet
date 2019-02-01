using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameParameterManager : SingletonMonoBehaviour<GameParameterManager>
{

    private string stageSceneName;

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

    [SerializeField]
    private GameStageData stageData;

    public enum PlayerType
    {
        Sword = 0,
        Rifle
    }

    private int currentStageNum = 0;

    private const string RESULT_SCENE_NAME = "Result";

    protected override void Awake()
    {
        base.Awake();
        Debug.Assert(stageData);
        ResetStage();
        DontDestroyOnLoad(gameObject);
    }

    public void SetStageSceneName(string name)
    {
        stageSceneName = name;
    }

    public void SetPlayerType(PlayerType type)
    {
        spawnPlayerType = type;
    }

    public void JumpNextStage()
    {
        currentStageNum++;
        if (currentStageNum >= stageData.StageList.Length)
        {
            Debug.Log("最終ステージです");
            //SetCurrentStageName();
            SceneController.JumpSceneAsync(RESULT_SCENE_NAME);
            return;
        }
        SetCurrentStageName();
        SceneController.ReloadSceneAsync();
    }

    public void ResetStage()
    {
        currentStageNum = 0;
        SetCurrentStageName();

    }

    private void SetCurrentStageName()
    {
        stageSceneName = stageData.StageList[currentStageNum];
    }
}
