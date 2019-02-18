using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

/// <summary>
/// シーン遷移の管理クラス
/// </summary>
public sealed class SceneController
{

    //private const string playSceneName = "MainGameSample";

    private const string LOAD_SCENE_NAME = "LoadScene";

    private static string nextSceneName = "Title";

    private static bool photonFlag = false;


    private static AsyncOperation JumpLoadScene()
    {
        return SceneManager.LoadSceneAsync(LOAD_SCENE_NAME);
    }

    /// <summary>
    /// 指定したシーンに飛ぶ(非同期)
    /// </summary>
    /// <param name="sceneName"></param>
	public static AsyncOperation JumpSceneAsync(string sceneName,bool isPhoton = false)
    {
        nextSceneName = sceneName;

        photonFlag = isPhoton;

        if (!isPhoton)
        {
            PhotonNetwork.Disconnect();
        }

        //カーソル表示
        Cursor.visible = true;
        // マウスのロックを解除
        Cursor.lockState = CursorLockMode.None;
        //return SceneManager.LoadSceneAsync(sceneName);

        Debug.Log($"ロードシーンに飛ぶ 次のシーン名　= {nextSceneName}");
        return JumpLoadScene();
    }

    /// <summary>
    /// 次のシーンに飛ぶ
    /// </summary>
    /// <returns></returns>
    public static AsyncOperation JumpNextScene()
    {
        // Photon使用シーンの場合
        if (photonFlag)
        {
            photonFlag = false;
            PhotonNetwork.LoadLevel(nextSceneName);
            return null;
        }

        

        return SceneManager.LoadSceneAsync(nextSceneName);
    }

    /// <summary>
    /// 指定したシーンを追加で生成(非同期)
    /// </summary>
    /// <param name="sceneName"></param>
    public static AsyncOperation AddSceneAsync(string sceneName)
    {

        string sceneNameTemp = null;

        // 現在読み込まれているシーン数だけループ
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {

            // 読み込まれているシーンを取得する
            sceneNameTemp = SceneManager.GetSceneAt(i).name;

            if (sceneNameTemp.Equals(sceneName))
            {
                Debug.LogWarning($"シーン名:{sceneName}は既に読み込まれています");
                return null;
            }

        }

        return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    /// <summary>
    /// 現在のシーンを読み直す(リトライ)
    /// </summary>
    public static AsyncOperation ReloadSceneAsync(bool isPhoton = false)
    {
        photonFlag = isPhoton;
        return JumpSceneAsync(SceneManager.GetActiveScene().name);
    }

}
