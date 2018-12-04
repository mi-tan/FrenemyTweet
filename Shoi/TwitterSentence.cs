using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Twitterから文章を取得するクラス
/// </summary>
public class TwitterSentence : MonoBehaviour, IGetSentence
{
    [SerializeField]
    private AnalysisSentence iAnalysis;

    private void Start()
    {
        Test();
    }

    public void Test()
    {
        iAnalysis.Analysis();
    }
}
