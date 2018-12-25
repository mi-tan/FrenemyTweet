using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconURL : MonoBehaviour {

    SceneTest test;
    public void Start()
    {

        test = GameObject.Find("URLInfo").GetComponent<SceneTest>();
        string icon = test.url;
        icon = icon.ToString();

        string url = icon;


        WWW web = new WWW(url);
        StartCoroutine(GetIcon(web));


    }


    private IEnumerator GetIcon(WWW web)
    {
   
        while (!web.isDone)
        {
            yield return null;

        }

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.EnableKeyword("_MainTex");
        renderer.material.SetTexture("_MainTex", web.textureNonReadable);
    }

}
