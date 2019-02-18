using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;
using Photon.Realtime;

public class MultiPlayerGet : MonoBehaviour {

    [SerializeField]
    private Text text;

    [SerializeField]
    private RawImage rawImage;

    MultiPlayerName _multiplayername;
    public string multiID;
    public string url;

    public Texture2D multiIconTexture;

    void Start()
    {
        _multiplayername = GameObject.Find("MultiPlayerName").GetComponent<MultiPlayerName>();
    }

    // Use this for initialization
    public void OnText () {

        multiID = _multiplayername.multinamespace[_multiplayername.multinamesacenumber - 1];
        text.text = multiID;

        url = "https://twitter.com/" + multiID + "/profile_image?size=original";
        StartCoroutine(DownloadMultiIcon(url));
        Debug.Log("ユーザーの名前" + multiID);
    }

    public IEnumerator DownloadMultiIcon(string url)
    {
        WWW www = new WWW(url);
        while (!www.isDone)
        {
            yield return null;
        }

        yield return url;

        Debug.Log("ゆーあーるえる" + url);

        multiIconTexture = www.textureNonReadable;
        rawImage.texture = multiIconTexture;
    }

}
