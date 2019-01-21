using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageDownloder : MonoBehaviour
{

    SceneTest test;
    SceneTest testname;

    [SerializeField]
    TwitterSceneManager twitterSceneManager;

    [SerializeField]
    private Text text;

    private void Start()
    {
        test = GameObject.Find("URLInfo").GetComponent<SceneTest>();
        testname = GameObject.Find("URLInfo").GetComponent<SceneTest>();
    }


    public IEnumerator Icon(Twitter.AccessTokenResponse response)
    {



        string strIcon = response.ScreenName.ToString();
        string IconUrl = strIcon;
        string url = "https://twitter.com/" + IconUrl + "/profile_image?size=original";

        test.url = url;
        test.name = IconUrl;
        var ScreenName = IconUrl;
    

        // wwwクラスのコンストラクタに画像URLを指定
        WWW www = new WWW(url);

        // アイコンを取得するまでループ
        while (!www.isDone)
        {
            yield return null;
        }

        // 画像ダウンロード完了を待機
        yield return response;


        RawImage rawImage = GetComponent<RawImage>();
        var rawImageIcon = www.textureNonReadable;
        rawImage.texture = rawImageIcon;

        TwitterParameterManager.Instance.SetUserIcon(rawImageIcon);
        TwitterParameterManager.Instance.SetUserID(ScreenName);

        var UserName = TwitterParameterManager.Instance.UserID;
        text.text = "@" + UserName;


        twitterSceneManager.CalcParameter();
    }


}