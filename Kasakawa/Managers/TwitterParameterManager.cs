using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ツイッターから取得したデータを格納するクラス
/// </summary>
public class TwitterParameterManager: SingletonMonoBehaviour<TwitterParameterManager>
{
    
    /// <summary>
    /// ツイッターアカウントのID
    /// </summary>
    private string userID = "";

    private Texture2D iconTexture = null;

    public string UserID
    {
        get
        {
            return userID;
        }
    }

    public Texture2D IconTexture
    {
        get
        {
            return iconTexture;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void SetUserID(string setID)
    {
        userID = setID;
    }

    public void SetUserIcon(Texture2D setTexture)
    {
        iconTexture = setTexture;
    }
	
	
}
