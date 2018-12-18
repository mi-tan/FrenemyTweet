using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageDownloder : MonoBehaviour
{


    public IEnumerator Icon(Twitter.AccessTokenResponse response)
    {

        string strIcon = response.ScreenName.ToString();
        string IconUrl = strIcon;
        string url = "https://twitter.com/" + IconUrl + "/profile_image?size=original";
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
        r.material.EnableKeyword("_MainTex");
        r.material.SetTexture("_MainTex", www.textureNonReadable);
    }


}