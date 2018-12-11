using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

/// <summary>
/// 分析クラス
/// </summary>
public class AnalysisSentence : MonoBehaviour, IAnalysis
{

    [SerializeField]
    private CategoryData[] categoryData;
    
    private string[] dictionaly = new string[100];

    /// <summary>
    /// 分析メソッド
    /// </summary>
    public AnalysisContainer Analysis(string[] str)
    {
        AnalysisContainer testContainer = new AnalysisContainer();
        AnalysisContainer.CategoryRet categoryRet = new AnalysisContainer.CategoryRet();

        for (int index = 0; index < categoryData.Length; index++)
        {

            //Debug.Log(categoryData[]);

            // カテゴリデータを取得
            dictionaly = categoryData[index].ReturnDictionary();

            // 辞書にある文字数分配列を確保
            //int[] count = new int[dictionaly.Length];
            int count = 0;

            for (int i = 0; i < dictionaly.Length; i++)
            {
                for (int j = 0; j < str.Length; j++)
                {
                    // 指定した文字が存在するかどうか取得する
                    MatchCollection matche = Regex.Matches(str[j], dictionaly[i]);

                    foreach (Match m in matche)
                    {
                        // Debug.Log(m　+　"へ格納");
                        count++;
                    }
                }
            }


            // Debug.Log(dictionaly[i] + " は" + count[i] + "個ありました");
            Debug.Log(categoryData[index] + " は" + count + "個ありました");
            // マッチした名前を格納
            categoryRet.categoryName = categoryData[index].getCategoryName;
            // 名前がマッチした回数を格納
            categoryRet.thisNameNum = count;
            // 構造体を格納
            testContainer.categoryRetList.Add(categoryRet);

        }

        return testContainer;
    }
}
