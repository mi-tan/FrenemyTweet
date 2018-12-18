using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconGet : MonoBehaviour {

    [SerializeField]
    ImageDownloder _imageDownLoder;
    public Twitter.AccessTokenResponse sendIcon;

    [SerializeField]
    TwitterComponent tc;

    void Start () {
        tc.GetComponent<TwitterComponent>();
    }
    


    public void OnClick()
    {
        sendIcon = tc.m_AccessTokenResponse;
        StartCoroutine(_imageDownLoder.Icon(sendIcon));
    }

}
