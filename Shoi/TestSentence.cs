using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// テスト用文章を取得するクラス
/// </summary>
public class TestSentence : MonoBehaviour, IGetSentence
{
    IAnalysis iAnalysis;

    public void Test()
    {
        Debug.Log("aaa");
        iAnalysis.Analysis();
    }
}
