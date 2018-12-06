using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 分析結果を保持するクラス
/// </summary>
public class AnalysisContainer {

    public string testString = "空";
    /// <summary>
    /// カテゴリを分析した結果を格納するリスト
    /// </summary>
    public List<CategoryRet> categoryRetList = new List<CategoryRet>();
    /// <summary>
    /// カテゴリを分析した結果を格納する構造体
    /// </summary>
    public struct CategoryRet{
        public string categoryName;
        public int thisNameNum;
    }
}
