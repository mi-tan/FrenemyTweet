using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using UnityEngine.UI;

namespace Test
{
    public class TestUIController : MonoBehaviour
    {
        [Inject]
        [SerializeField]
        TestGameController testGameController;

        [SerializeField]
        private GameObject startText;

        [SerializeField]
        private Text positionText;

        [SerializeField]
        private float startTextTime = 1f;

        // Use this for initialization
        void Awake()
        {
            startText.SetActive(false);
            // ゲーム開始時に開始時のテキストを登録する
            testGameController.OnGameStart.Subscribe(_ => { ActiveStartText(); })
                .AddTo(gameObject);

            // 一定時間後に開始時テキストを非表示にする
            Observable.Timer(System.TimeSpan.FromSeconds(startTextTime))
                .Subscribe(_ => DisableStartText())
                .AddTo(gameObject);
            
            ShowPlayerPosition(Vector3.zero);

            // プレイヤーの座標が変化した場合、UIを更新する
            testGameController.ObserveEveryValueChanged(_ => testGameController.TestPlayer.transform.position)
                .Subscribe(pos => ShowPlayerPosition(pos));
        }

        private void ActiveStartText()
        {
            startText.SetActive(true);
        }

        private void DisableStartText()
        {
            startText.SetActive(false);
        }

        private void ShowPlayerPosition(Vector3 position)
        {
            positionText.text = $"PlayerPos : \nx = {position.x}\ny = {position.y}\nz = {position.z}";
        }

    }

}
