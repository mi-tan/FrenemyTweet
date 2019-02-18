using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{

    private GlobalGameParamaterManager globalParamaterManager;

    [SerializeField]
    private Text timeText;

    [SerializeField]
    private Text playerIDText;

    private void Awake()
    {
        globalParamaterManager = GlobalGameParamaterManager.Instance;
    }

    private void Start()
    {
        playerIDText.text = "@" + TwitterParameterManager.Instance.UserID;

        // 時間を表示する

        float time = globalParamaterManager.TimeCount; 

        //分を計算する
        int minute = Mathf.FloorToInt(time / 60f);
        //秒を計算する
        int seconds = (int)(time % 60f);
        //秒の小数点以下の数値を計算する
        int secondsDecimal = (int)((time % 60f - seconds) * 100);

        //string format = "d2";

        timeText.text = $"{minute.ToString("00")}:{seconds.ToString("00")}";
        //decimalText.text = $".{secondsDecimal.ToString("00")}";
    }

    public void BackTitle()
    {
        SceneController.JumpSceneAsync("Title");
    }
}
