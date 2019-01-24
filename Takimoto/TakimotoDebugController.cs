using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TakimotoDebugController : MonoBehaviour {
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("TakimotoDebugScene");

            // マウスを表示
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
