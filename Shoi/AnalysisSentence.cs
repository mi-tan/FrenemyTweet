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

        // 辞書を取得
        dictionaly = categoryData[0].ReturnDictionary();

        // 辞書にある文字数分配列を確保
        int[] count = new int[dictionaly.Length];

        for (int i = 0; i < dictionaly.Length; i++)
        {
            for (int j = 0; j < str.Length; j++)
            {
                // 指定した文字が存在するかどうか取得する
                MatchCollection matche = Regex.Matches(str[j], dictionaly[i]);

                foreach (Match m in matche)
                {
                    Debug.Log(m　+　"へ格納");
                    count[i]++;
                }
            }
        }

        for (int i = 0; i < dictionaly.Length; i++)
        {
            Debug.Log(dictionaly[i] + " は" + count[i] + "個ありました");
        }


        // AnalysisContainerにいろいろ入れる
        return testContainer;
    }
}
