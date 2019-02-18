using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTypeAnalysis : MonoBehaviour {

    [SerializeField]
    private TwitterSceneManager twitterSceneManager;
    [SerializeField]
    private PlayerType[] playerType;

    private int lengthCriteria;

    [SerializeField]
    AnalysisSentence analysisSentence;

    private string categoryName = "";
    private int muiNum;

    /// <summary>
    /// プレイヤーのタイプを判断
    /// </summary>
    /// <param name="container"></param>
    public void TypeReveal(AnalysisContainer container)
    {
        muiNum = analysisSentence.getSentenceLength;

        // 使用するデータ
        AnalysisContainer.CategoryRet useContainer = container.categoryRetList[ReturnMostLengthCategoryNum(container)];

        Debug.Log(useContainer.categoryName);
        if (useContainer.thisNameNum < muiNum)
        {
            categoryName = "無為";
        }
        else
        {
            categoryName = useContainer.categoryName;
        }


        // 冗長
        if (useContainer.redundancyFlag == true)
        {
            if (categoryName == "善意")
            {
                // リア充
                twitterSceneManager.SetPlayerType = playerType[0];
                Debug.Log("神絵師");
            }
            else if (categoryName == "悪意")
            {
                // ネット弁慶
                twitterSceneManager.SetPlayerType = playerType[1];
                Debug.Log("KY");
            }
            else if (categoryName == "無為")
            {
                // サイコパス
                twitterSceneManager.SetPlayerType = playerType[2];
                Debug.Log("ヲタク");
            }
        }
        // 簡潔
        else
        {
            if (categoryName == "善意")
                {
                    // 陽キャ
                    twitterSceneManager.SetPlayerType = playerType[3];
                Debug.Log("陽キャ");
            }
            else if (categoryName == "悪意")
                {
                    // 陰キャ
                    twitterSceneManager.SetPlayerType = playerType[4];
                Debug.Log("ネット弁慶");
            }
            else if (categoryName == "無為")
                {
                    // 怠け者
                    twitterSceneManager.SetPlayerType = playerType[5];
                Debug.Log("陰キャ");
            }
        }
    }

    /// <summary>
    /// 一番長いカテゴリの番号を返す
    /// </summary>
    /// <param name="container"></param>
    private int ReturnMostLengthCategoryNum(AnalysisContainer container)
    {
        // 現在一番長いもの
        int mostLongNum = 0;
        // 現在一番長いカテゴリ番号
        int mostLongCategoryNum = 0;

        for (int index = 0; index < container.categoryRetList.Count; index++)
        {
            Debug.Log("名前：" + container.categoryRetList[index].categoryName +
                      "　量：" + container.categoryRetList[index].thisNameNum);
            //Debug.Log("コンテナ" + index + "：" + muiNum + "-" + container.categoryRetList[index].thisNameNum);
            muiNum -= container.categoryRetList[index].thisNameNum;


            if (mostLongNum <= container.categoryRetList[index].thisNameNum)
            {
                mostLongNum = container.categoryRetList[index].thisNameNum;
                mostLongCategoryNum = index;
            }
        }
        return mostLongCategoryNum;
    }
}
