using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

/// <summary>
/// 分析クラス
/// </summary>
public class AnalysisSentence : MonoBehaviour, IAnalysis
{
    /// <summary>
    /// はじく単語を集めたデータ
    /// </summary>
    [SerializeField]
    private CategoryData breakCategoryData;

    [SerializeField]
    private CategoryData[] categoryData;
    /// <summary>
    /// 長い文章の基準値
    /// </summary>
    [SerializeField, Tooltip("長い文章の基準値")]
    private int redundancyCriteria = 20;
    /// <summary>
    /// 長い文章のほうが多いと判断する割合
    /// </summary>
    [SerializeField, Range(0, 1), Tooltip("長い文章のほうが多いと判断する割合")]
    private float redundancyRatio = 0.5f;

    private int sentenceLength = 0;
    public int getSentenceLength
    {
        get { return sentenceLength; }
    }

    private string[] dictionaly = new string[100];

    /// <summary>
    /// 分析メソッド
    /// </summary>
    public AnalysisContainer Analysis(List<string> str)
    {
        AnalysisContainer container = new AnalysisContainer();
        AnalysisContainer.CategoryRet categoryRet = new AnalysisContainer.CategoryRet();

        // リストの長さを保持
        sentenceLength = str.Count;

        for (int index = 0; index < categoryData.Length; index++)
        {
            //Debug.Log(categoryData[]);
            // カテゴリデータを取得
            dictionaly = categoryData[index].ReturnDictionary();

            int matchNum = 0;
            string sentence = "";

            // リプライをはじく正規表現を作成
            Regex regex = new Regex("@[0-9,a-z,\\w]{0,} ");
            
            for (int i = 0; i < dictionaly.Length; i++)
            {
                for (int j = 0; j < str.Count; j++)
                {
                    // 正規表現
                    sentence = regex.Replace(str[j], "");
                    // Debug.Log($"@を消した後　= {sentence}");
                    
                    // 情報更新
                    str[j] = sentence;

                    // 文字検索
                    MatchCollection matche = Regex.Matches(str[j], dictionaly[i]);

                    int count = 0;
                    foreach (Match m in matche)
                    {
                        int matchCnt = 0;
                        //Debug.Log("match");

                        string[] breakDictionaly = breakCategoryData.ReturnDictionary();
                        for (int b = 0; b < breakDictionaly.Length; b++)
                        {
                            // 文字検索
                            MatchCollection breackWordMatche = Regex.Matches(str[j], breakDictionaly[b]);

                            foreach (Match bm in breackWordMatche)
                            {
                                matchCnt++;
                            }
                        }

                        if (matchCnt == 0)
                        {
                            count++;
                        }
                    }
                    matchNum += count;
                }
            }

            // マッチした名前を格納
            categoryRet.categoryName = categoryData[index].getCategoryName;
            // 名前がマッチした回数を格納
            categoryRet.thisNameNum = matchNum;
            // 構造体を格納
            container.categoryRetList.Add(categoryRet);
            //Debug.Log(categoryRet.categoryName + "：" + categoryRet.thisNameNum);
        }

        // 冗長な文章の数
        int redundancyNum = 0;
        for (int i = 0; i < str.Count; i++)
        {
            //Debug.Log(str[i].Length + " > " + redundancyCriteria);

            // 基準値よりも長いものがあった場合カウントする
            if (str[i].Length >= redundancyCriteria)
            {
                redundancyNum++;
            }
        }
        // 長いものが多いか判定

        //Debug.Log(redundancyNum + " : " + str.Count +" * "+ redundancyRatio+" = "+ str.Count * redundancyRatio);
        if (redundancyNum > str.Count * redundancyRatio)
        {
            // 長いものが多い
            categoryRet.redundancyFlag = true;
        }
        else
        {
            // 短いものが多い
            categoryRet.redundancyFlag = false;
        }

        // 構造体を格納
        container.categoryRetList.Add(categoryRet);

        return container;
    }
}
