using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageDownloder : MonoBehaviour
{

    SceneTest test;
    SceneTest testname;

    [SerializeField]
    TwitterSceneManager twitterSceneManager;

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

        Renderer r = GetComponent<Renderer>();
        var texture2D = www.textureNonReadable;

        // _MainTex＝Albedoを書き換える
        r.material.EnableKeyword("_MainTex");
        r.material.SetTexture("_MainTex", texture2D);

        TwitterParameterManager.Instance.SetUserIcon(texture2D);
        TwitterParameterManager.Instance.SetUserID(ScreenName);


        twitterSceneManager.CalcParameter();
    }


}