using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTest : MonoBehaviour {

    public string url;
    public string name;


// Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick()
    {
        SceneManager.LoadScene("mi-tanSceneTest");
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
