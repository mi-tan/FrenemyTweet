using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GetScreenName : MonoBehaviour {

    SceneTest test;
    [SerializeField]
    private Text text;

    // Use this for initialization
    public void Start () {
        test = GameObject.Find("URLInfo").GetComponent<SceneTest>();
        string name = test.name;
        StartCoroutine(ScreenName(name));
    }


	
    private IEnumerator ScreenName(string name)
    {
        text.text = name;

        yield return name;
    }
}
