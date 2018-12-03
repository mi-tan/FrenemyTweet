using UnityEngine;
using System.Text.RegularExpressions;

/// <summary>
/// 分析クラス
/// </summary>
public class AnalysisSentence : MonoBehaviour, IAnalysis
{
    [SerializeField]
    private string[] matchString;
    [SerializeField]
    private CategoryData obj;

    private string[] dictionary;
    private CategoryData categoryData;

    private void Awake()
    {
        categoryData = obj.GetComponent<CategoryData>();
        dictionary = categoryData.ReturnDictionary();
    }

    /// <summary>
    /// 分析メソッド
    /// </summary>
    public void Analysis()
    {
        int[] count = new int[dictionary.Length];

        for (int i = 0; i < dictionary.Length; i++)
        {
            string dictionaryStr = dictionary[i];

            for (int j = 0; j < matchString.Length; j++)
            {
                // 指定した文字が存在するかどうか取得する
                MatchCollection matche = Regex.Matches(matchString[j], dictionaryStr);
                foreach (Match m in matche)
                {
                    //Console.WriteLine(m.Value);
                    count[i]++;
                }
            }
        }

        for (int i = 0; i < dictionary.Length; i++)
        {
            Debug.Log(dictionary[i] + " は" + count[i] + "個ありました");
        }
    }
}
