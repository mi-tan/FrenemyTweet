using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryData : MonoBehaviour {

    [SerializeField]
    private string[] dictionary;

    public string[] ReturnDictionary()
    {
        for (int i = 0; i < dictionary.Length; i++) {
            Debug.Log("dic = "+dictionary[i]);
        }
        return dictionary;
    }
}
