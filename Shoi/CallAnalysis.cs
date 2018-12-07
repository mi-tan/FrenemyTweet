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

            for (int i = 0; i < analysisContainer.categoryRetList.Count; i++)
            {
                Debug.Log("名前：" + analysisContainer.categoryRetList[i].categoryName +
                         " 数　：" + analysisContainer.categoryRetList[i].thisNameNum);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) {
                //analysisContainer
            }
        }
    }
}


