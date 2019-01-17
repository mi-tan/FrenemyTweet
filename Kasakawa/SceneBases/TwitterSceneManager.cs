using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitterSceneManager : MonoBehaviour {

    [SerializeField]
    private string gameSceneName = "";

    public void JumpScene(string sceneName)
    {
        SceneController.JumpSceneAsync(sceneName);
    }

    public void JumpGameScene()
    {
        SceneController.JumpSceneAsync(gameSceneName);
    }

}
