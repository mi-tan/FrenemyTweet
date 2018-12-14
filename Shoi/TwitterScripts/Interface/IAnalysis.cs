using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 分析インターフェース
/// </summary>
public interface IAnalysis
{
    /// <summary>
    /// 分析メソッド
    /// </summary>
    AnalysisContainer Analysis(List<string> sentence);
}
