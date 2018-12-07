using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using System;

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

        // Use this for initialization
        void Start()
        {
            //yield return new WaitForSeconds(1f);
            // ゲーム開始時のイベントを実行
            startSubject.OnNext(Unit.Default);
        }
    }

}
