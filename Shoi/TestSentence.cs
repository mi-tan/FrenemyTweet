using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// テスト用文章を取得するクラス
/// </summary>
public class TestSentence : MonoBehaviour, IGetSentence
{
    [SerializeField, Tooltip("解析するテキストファイル")]
    private TextAsset file;

    public void GetSentence()
    {

    }
}
