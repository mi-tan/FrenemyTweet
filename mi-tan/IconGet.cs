using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconGet : MonoBehaviour {

    [SerializeField]
    ImageDownloder _imageDownLoder;

    Twitter.AccessTokenResponse TwitterIcon;
    string TwitterID = "";

    // Use this for initialization
    void Start () {

	}
	
    public void TwitterIconGet(Twitter.AccessTokenResponse response)
    {

        TwitterID = response.UserId;
        Debug.Log(TwitterID);
        _imageDownLoder.GetComponent<ImageDownloder>();
        _imageDownLoder.Icon("");
    }

    public void OnClic()
    {
        Debug.Log(TwitterID);
    }

}
