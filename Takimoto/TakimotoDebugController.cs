using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TakimotoDebugController : MonoBehaviour {
    private bool stop = false;

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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (stop)
            {
                stop = false;
                Time.timeScale = 1f;
            }
            else
            {
                stop = true;
                Time.timeScale = 0f;
            }
        }
    }
}
