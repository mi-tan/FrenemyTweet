using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlayerName : MonoBehaviour {

    public string[] multinamespace = new string[3];
    public string[] urlmulti = new string[3];
    public int multinamesacenumber = 0;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
