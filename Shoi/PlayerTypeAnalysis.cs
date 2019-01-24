using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTypeAnalysis : MonoBehaviour {

    [SerializeField]
    private PlayerType[] playerType;

    /// <summary>
    /// プレイヤーのタイプを判断
    /// </summary>
    /// <param name="container"></param>
    public void TypeReveal(AnalysisContainer container)
    {
        Debug.Log("長さ" + container.categoryRetList.Count);

        for (int index = 0; index < container.categoryRetList.Count; index++)
        {
            Debug.Log("名前：" + container.categoryRetList[index].categoryName +
                      "　量：" + container.categoryRetList[index].thisNameNum);
        }

        // container.categoryName;
        // container.thisNameNum;
    }

}
