using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Twitterから文章を取得するクラス
/// </summary>
public class TwitterSentence : MonoBehaviour, IGetSentence
{

    [SerializeField]
    private GameObject inputPINField;
    //[SerializeField]
    //private GameObject obj;
    [SerializeField]
    private PlayerTypeAnalysis playerTypeAnalysis;

    private IAnalysis iAnalysis;
    private TwitterComponent twitterHandler;

    /// <summary>
    /// ツイッターから取得した文字
    /// </summary>
    private List<string> tweetList = new List<string>();


    //private void Start()
    //{
    //    iAnalysis = obj.GetComponent<IAnalysis>();
    //    GetSentence();
    //}
    public void OnClickAuthPINButon()
    {
        StartCoroutine(CallAuthPINButon());
    }

    private IEnumerator CallAuthPINButon()
    {

        if (twitterHandler == null)
            twitterHandler = GetComponent<TwitterComponent>();

        string myPIN = inputPINField.GetComponent<InputField>().text;

        twitterHandler.AuthPINButon(myPIN);
        while (!twitterHandler.getIsSentence)
        {
            yield return null;
        }
        

        // ツイッターの文章を取得
        tweetList = twitterHandler.getSentenceList;
        // 取得した結果を送る
        playerTypeAnalysis.TypeReveal(GetSentence());
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        GetSentence();
    //        //Debug.Log("Push!!!");
    //        //for (int i = 0; i < tweetList.Count; i++)
    //        //{
    //        //    Debug.Log("要素数["+i+"]："+tweetList[i]);
    //        //}
    //    }
    //}


    public AnalysisContainer GetSentence()
    {
        if (iAnalysis == null)
            iAnalysis = GetComponent<IAnalysis>();

        return iAnalysis.Analysis(tweetList);
    }
}
