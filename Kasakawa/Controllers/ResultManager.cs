using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : MonoBehaviour {

	public void BackTitle()
    {
        SceneController.JumpSceneAsync("Title");
    }
}
