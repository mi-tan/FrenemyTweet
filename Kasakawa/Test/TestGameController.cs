using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UniRx.Triggers;
using System;
using UnityEngine.SceneManagement;

namespace Test
{
    public class TestGameController : MonoBehaviour
    {
        [Inject]
        [SerializeField]
        private TestPlayer testPlayer;

        public TestPlayer TestPlayer
        {
            get
            {
                return testPlayer;
            }
        }

        // イベントを登録するインスタンス
        private Subject<Unit> startSubject = new Subject<Unit>();

        // サブジェクトを公開する
        public IObservable<Unit> OnGameStart
        {
            get { return startSubject; }
        }

        [SerializeField]
        [Header("サブシーン名のリスト")]
        private string[] subSceneNames;

        public float TimeCount { get; private set; } = 0f;

        // Use this for initialization
        void Start()
        {
            // ゲーム開始時のイベントを実行
            startSubject.OnNext(Unit.Default);

            foreach (var sceneName in subSceneNames)
            {
                //SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
                SceneController.AddSceneAsync(sceneName);
            }

            // 時間をカウントする
            (this).UpdateAsObservable()
            .Subscribe(_ => { TimeCount += Time.deltaTime; });
        }
    }

}
