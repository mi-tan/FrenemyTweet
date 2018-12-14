using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName ="ScriptableObject/Data/Category")]
public class CategoryData : ScriptableObject {

    [SerializeField]
    private string categoryName;

    [SerializeField]
    public string[] dictionary;

    public string getCategoryName
    {
        get { return categoryName; }
        //private set { categoryName = value; }
    }

    public string[] ReturnDictionary()
    {
        return dictionary;
    }
}
