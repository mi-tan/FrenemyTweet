using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TakimotoDebugController : MonoBehaviour {
    public void LoadScene(string sceneName)
    {
        SceneController.JumpSceneAsync(sceneName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //SceneManager.LoadScene("TakimotoDebugScene");

            SceneController.JumpSceneAsync("TakimotoDebugScene");

            // マウスを表示
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
