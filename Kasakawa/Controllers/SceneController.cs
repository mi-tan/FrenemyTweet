using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移の管理クラス
/// </summary>
public sealed class SceneController{

    /// <summary>
    /// 指定したシーンに飛ぶ(非同期)
    /// </summary>
    /// <param name="sceneName"></param>
	public static void JumpSceneAsync(int sceneNum)
    {
        SceneManager.LoadSceneAsync(sceneNum);
    }

    /// <summary>
    /// 指定したシーンに飛ぶ(非同期)
    /// </summary>
    /// <param name="sceneName"></param>
	public static void JumpSceneAsync(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    /// <summary>
    /// 指定したシーンを追加で生成(非同期)
    /// </summary>
    /// <param name="sceneName"></param>
    public static void AddSceneAsync(string sceneName)
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
                return;
            }

        }

        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    /// <summary>
    /// 現在のシーンを読み直す(リトライ)
    /// </summary>
    public static void ReloadSceneAsync()
    {
        JumpSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

}
