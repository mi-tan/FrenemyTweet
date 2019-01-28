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
    /// <summary>
    /// 長い文章の基準値
    /// </summary>
    [SerializeField]
    private int redundancyCriteria = 20;
    /// <summary>
    /// 長い文章のほうが多いと判断する割合
    /// </summary>
    [SerializeField, Range(0, 1)]
    private float redundancyRatio = 0.5f;

    private int mui = 0;
    public int getMui
    {
        get { return mui; }
    }

    private string[] dictionaly = new string[100];

    /// <summary>
    /// 分析メソッド
    /// </summary>
    public AnalysisContainer Analysis(List<string> str)
    {
        AnalysisContainer container = new AnalysisContainer();
        AnalysisContainer.CategoryRet categoryRet = new AnalysisContainer.CategoryRet();

        for (int index = 0; index < categoryData.Length; index++)
        {
            //Debug.Log(categoryData[]);
            // カテゴリデータを取得
            dictionaly = categoryData[index].ReturnDictionary();

            // 辞書にある文字数分配列を確保
            //int[] count = new int[dictionaly.Length];
            int matchNum = 0;

            string sentence = "";

            Regex regex = new Regex("@[0-9,a-z,_]{0,} ");

            for (int i = 0; i < dictionaly.Length; i++)
            {
                for (int j = 0; j < str.Count; j++)
                {
                    // 正規表現
                    sentence = regex.Replace(str[j], "");
                    // Debug.Log($"@を消した後　= {sentence}");

                    // 指定した文字が存在するかどうか取得する
                    MatchCollection matche = Regex.Matches(sentence, dictionaly[i]);

                    int count = 0;
                    foreach (Match m in matche)
                    {
                        count++;
                    }

                    if (count == 0)
                    {
                        mui++;
                    }
                    matchNum += count;
                }
            }
            // Debug.Log(dictionaly[i] + " は" + count[i] + "個ありました");
            // Debug.Log(categoryData[index] + " は" + count + "個ありました");

            // マッチした名前を格納
            categoryRet.categoryName = categoryData[index].getCategoryName;
            // 名前がマッチした回数を格納
            categoryRet.thisNameNum = matchNum;
            // 構造体を格納
            container.categoryRetList.Add(categoryRet);

        }

        // 冗長な文章の数
        int redundancyNum = 0;
        for (int i = 0; i < str.Count; i++)
        {
            Debug.Log(str[i] + "：" + str[i].Length);

            if (str[i].Length <= redundancyCriteria)
            {
                redundancyNum++;
            }
        }

        // Debug.Log("長かったもの：" + redundancyNum + " 基準：" + str.Count * redundancyRatio);

        // 長いものが多いか判定
        if (redundancyNum > str.Count * redundancyRatio)
        {
            categoryRet.redundancyFlag = true;
        }
        else
        {
            categoryRet.redundancyFlag = false;
        }

        return container;
    }
}
