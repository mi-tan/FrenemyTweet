using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TakimotoDebugController : MonoBehaviour
{
    //private bool stop = false;

    //public void LoadSwordPlayer()
    //{
    //    GameParameterManager.Instance.SetPlayerType(GameParameterManager.PlayerType.Sword);

    //    SceneController.JumpSceneAsync("MainGameScene");
    //}

    //public void LoadRiflePlayer()
    //{
    //    GameParameterManager.Instance.SetPlayerType(GameParameterManager.PlayerType.Rifle);

    //    SceneController.JumpSceneAsync("MainGameScene");
    //}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // マウス表示
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            SceneController.JumpSceneAsync("WeaponSelectScene");
        }

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    if (stop)
        //    {
        //        stop = false;
        //        Time.timeScale = 1f;
        //    }
        //    else
        //    {
        //        stop = true;
        //        Time.timeScale = 0f;
        //    }
        //}
    }
}
