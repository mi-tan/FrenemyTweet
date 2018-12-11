using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UnityEngine.UI;

namespace Test
{
    public class TestTimerUIController : MonoBehaviour
    {
        [Inject]
        TestGameController testGameController;

        [SerializeField]
        private Text timeText;

        // Use this for initialization
        void Awake()
        {

            // 時間カウントが変化した場合、表示を更新する
            testGameController.ObserveEveryValueChanged(_ => testGameController.TimeCount)
                .Subscribe(time => { ShowTimeCount(time); });
        }

        // 時間を表示する
        public void ShowTimeCount(float time)
        {
            timeText.text = $"Time : {time}";
        }

    }

}
