using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タイトルシーン全体の管理クラス
/// </summary>
public class TitleManager : MonoBehaviour {

    [SerializeField]
    private string nextSceneName = "";

    [SerializeField]
    TextContoller _textcontoller;

    bool keyflag = false;


	public void Update ()
    {
        if (_textcontoller.endFlag == true)
        {

            if (keyflag == false)
            {
                keyflag = true;
            }
            else
            {
                // 何かしらキーが押された時
                if (Input.anyKeyDown)
                {
                    SceneController.JumpSceneAsync(nextSceneName);
                }
            }
        }
	}
}
