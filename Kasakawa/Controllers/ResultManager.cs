using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour {

    //private Text

	public void BackTitle()
    {
        SceneController.JumpSceneAsync("Title");
    }
}
