using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UnityEngine.UI;

public class MainGameTimerUIManager : MonoBehaviour
{
    [Inject]
    private MainGameManager gameManager;

    [SerializeField]
    private Text timeText;

    [Header("小数点以下表示用テキスト")]
    [SerializeField]
    private Text decimalText;

    void Awake()
    {

        // 時間カウントが変化した場合、表示を更新する
        gameManager.ObserveEveryValueChanged(_ => GlobalGameParamaterManager.Instance.TimeCount)
            .Subscribe(time => { ShowTimeCount(time); });
    }

    /// <summary>
    /// 時間を表示する
    /// </summary>
    /// <param name="time"></param>
    public void ShowTimeCount(float time)
    {
        //分を計算する
        int minute = Mathf.FloorToInt(time / 60f);
        //秒を計算する
        int seconds = (int)(time % 60f);
        //秒の小数点以下の数値を計算する
        int secondsDecimal = (int)((time % 60f - seconds) * 100);

        //string format = "d2";

        timeText.text = $"{minute.ToString("00")}:{seconds.ToString("00")}";
        decimalText.text = $".{secondsDecimal.ToString("00")}";
    }

}
