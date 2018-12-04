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
    private CategoryData obj;

    /// <summary>
    /// 文字を認識するための辞書
    /// </summary>
    //private string[] dictionary;

    private CategoryData categoryData = null;

    private string[] dictionaly = new string[100];

    /// <summary>
    /// 分析メソッド
    /// </summary>
    public void Analysis(string[] str)
    {

        if (categoryData == null)
            categoryData = obj.GetComponent<CategoryData>();

        // 辞書を取得
        dictionaly = categoryData.ReturnDictionary();

        int[] count = new int[dictionaly.Length];

        for (int i = 0; i < dictionaly.Length; i++)
        {
            for (int j = 0; j < str.Length; j++)
            {
                // 指定した文字が存在するかどうか取得する
                MatchCollection matche = Regex.Matches(str[j], dictionaly[i]);
                foreach (Match m in matche)
                {
                    count[i]++;
                }
            }
        }

        for (int i = 0; i < dictionaly.Length; i++)
        {
            Debug.Log(dictionaly[i] + " は" + count[i] + "個ありました");
        }
    }
}
