using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryData : MonoBehaviour {

    [SerializeField]
    private string[] dictionary;

    public string[] ReturnDictionary()
    {
        Debug.Log("辞書呼び出し");
        return dictionary;
    }
}
