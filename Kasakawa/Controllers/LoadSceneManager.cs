using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneManager : MonoBehaviour
{
    [SerializeField]
    private float loadWaitTime = 0.5f;

    // Use this for initialization
    IEnumerator Start()
    {
        yield return new WaitForSeconds(loadWaitTime);
        SceneController.JumpNextScene();
    }


}
