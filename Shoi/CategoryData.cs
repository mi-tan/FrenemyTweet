using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName ="ScriptableObject/Data/Category")]
public class CategoryData : ScriptableObject {

    [SerializeField]
    private string categoryName;

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
