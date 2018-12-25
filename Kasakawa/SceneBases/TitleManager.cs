using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトルシーン全体の管理クラス
/// </summary>
public class TitleManager : MonoBehaviour {

    [SerializeField]
    private string nextSceneName = "";
	
	// Update is called once per frame
	void Update ()
    {
        // 何かしらキーが押された時
        if (Input.anyKeyDown)
        {
            SceneController.JumpSceneAsync(nextSceneName);
        }
	}
}
