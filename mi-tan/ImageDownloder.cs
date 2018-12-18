using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageDownloder : MonoBehaviour
{


    public IEnumerator Icon(string icon_test)
    {
        const string url = "";
        // wwwクラスのコンストラクタに画像URLを指定
        WWW www = new WWW(url);
        icon_test = www.ToString();

        // 画像ダウンロード完了を待機
        yield return icon_test;

        // webサーバから取得した画像をRaw Imaugで表示する
        RawImage rawImage = GetComponent<RawImage>();
        rawImage.texture = www.textureNonReadable;

        //ピクセルサイズ等倍に
        rawImage.SetNativeSize();
    }


}