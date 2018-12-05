using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Test
{
    /// <summary>
    /// 文字を取得する処理を呼び出すクラス
    /// </summary>
    public class CallAnalysis : MonoBehaviour
    {
        [SerializeField]
        private GameObject objSentence;

        private IGetSentence iGetSentence;
        AnalysisContainer analysisContainer;

        private void Start()
        {
            if (iGetSentence == null)
                iGetSentence = objSentence.GetComponent<IGetSentence>();

            analysisContainer = iGetSentence.GetSentence();
            Debug.Log(analysisContainer.testString);
        }
    }
}


