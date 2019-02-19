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

    public GameObject playerCanvas;

    #region Photon CallBacks

    void Start()
    {
        _multiplayername = GameObject.Find("MultiPlayerName").GetComponent<MultiPlayerName>();
        multiplayerget = GameObject.Find("MultiPlayerGet").GetComponent<MultiPlayerGet>();
        playerCanvas.SetActive(false);
    }

    // 入室
    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom()" + other.NickName);

        playerCanvas.SetActive(true);
        // ユーザーアイコンをセットする
        other.userIconTexture = TwitterParameterManager.Instance.IconTexture;
        multiplayerget.multiIconTexture = other.userIconTexture;

        _multiplayername.multinamespace[_multiplayername.multinamesacenumber] = other.NickName;
        _multiplayername.multinamesacenumber++;

        multiplayerget.OnText();

        Debug.Log("アクターナンバー" + other.ActorNumber);
        Debug.Log("ふぉとんねっとわーく" + PhotonNetwork.NickName);
        Debug.Log("ぷれいやーりすとー : " + PhotonNetwork.PlayerList[0]);

        int testlist = 0;
        string aplayertest = PhotonNetwork.PlayerList[testlist].ToString();
        testlist++;
        Debug.Log("プレイヤーリストの０番目" + aplayertest);
        string aaplayertest = PhotonNetwork.PlayerList[testlist].ToString();
        Debug.Log("プレイヤーリストの１番目" + aaplayertest);
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", PhotonNetwork.IsMasterClient);
        }
    }


    // 退室
    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom()" + other.NickName);
        playerCanvas.SetActive(false);

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerleftRoom() {0}", PhotonNetwork.IsMasterClient);
        }
    }
    #endregion


}
