using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Twitterから文章を取得するクラス
/// </summary>
public class TwitterSentence : MonoBehaviour, IGetSentence
{
    [SerializeField]
    private GameObject obj;
    private IAnalysis iAnalysis;

    /// <summary>
    /// ツイッターから取得した文字
    /// </summary>
    private string[] twitterChar = {"twitter" };

    private void Start()
    {
        iAnalysis = obj.GetComponent<IAnalysis>();
        GetSentence();
    }

    public AnalysisContainer GetSentence()
    {
        return iAnalysis.Analysis(twitterChar);
    }
}
