using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    /// <summary>
    /// テスト用文章を取得するクラス
    /// </summary>
    public class TestSentence : MonoBehaviour, IGetSentence
    {
        [SerializeField]
        private GameObject obj;
        /// <summary>
        /// ツイッターから取得した文字
        /// </summary>
        [SerializeField]
        private string[] testChar = { "一行目", "ばか", "馬鹿アホ", "四行目", "あほあほあほあほ", "六行目" };

        private IAnalysis iAnalysis;

        private void Start()
        {
            if (iAnalysis == null)
                iAnalysis = obj.GetComponent<IAnalysis>();
        }


        public AnalysisContainer GetSentence()
        {
            return iAnalysis.Analysis(testChar);
        }
    }
}
