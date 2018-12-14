using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移の管理クラス
/// </summary>
public class SceneController{

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
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

}
