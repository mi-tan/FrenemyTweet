using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UnityEngine.UI;

public class MainGameTimerUIManager : MonoBehaviour
{
    [Inject]
    MainGameManager gameManager;

    [SerializeField]
    private Text timeText;

    void Awake()
    {

        // 時間カウントが変化した場合、表示を更新する
        gameManager.ObserveEveryValueChanged(_ => gameManager.TimeCount)
            .Subscribe(time => { ShowTimeCount(time); });
    }

    /// <summary>
    /// 時間を表示する
    /// </summary>
    /// <param name="time"></param>
    public void ShowTimeCount(float time)
    {
        timeText.text = $"Time : {time}";
    }

}
