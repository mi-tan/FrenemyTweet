using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System;

public class PhotonGameManager : MonoBehaviourPunCallbacks {

    MultiPlayerGet multiplayerget;

    MultiPlayerName _multiplayername;

    #region Photon CallBacks

    void Start()
    {
        _multiplayername = GameObject.Find("MultiPlayerName").GetComponent<MultiPlayerName>();
        multiplayerget = GameObject.Find("MultiPlayerGet").GetComponent<MultiPlayerGet>();
    }

    // 入室
    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom()" + other.NickName);

        // ユーザーアイコンをセットする
        other.userIconTexture = TwitterParameterManager.Instance.IconTexture;
        multiplayerget.multiIconTexture = other.userIconTexture;

        _multiplayername.multinamespace[_multiplayername.multinamesacenumber] = other.NickName;
        _multiplayername.multinamesacenumber++;

        multiplayerget.OnText();

        Debug.Log("アクターナンバー" + other.ActorNumber);
        Debug.Log("ふぉとんねっとわーく" + PhotonNetwork.NickName);

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", PhotonNetwork.IsMasterClient);
        }
    }


    // 退室
    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom()" + other.NickName);

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerleftRoom() {0}", PhotonNetwork.IsMasterClient);
        }
    }
    #endregion


}
