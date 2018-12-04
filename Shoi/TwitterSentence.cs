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
    private string[] twitterChar = {"一行目", "二行目", "三行目", "四行目" , "五行目" , "六行目" };

    private void Start()
    {
        iAnalysis = obj.GetComponent<IAnalysis>();
        GetSentence();
    }

    

    public void GetSentence()
    {

        //Debug.Log("twitterから取得したものをIAnalysisへ受け渡す");
        iAnalysis.Analysis(twitterChar);
    }
}
