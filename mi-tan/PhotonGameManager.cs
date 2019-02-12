using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class PhotonGameManager : MonoBehaviourPunCallbacks {

    #region Photon CallBacks
    // 入室
    public override void OnPlayerEnteredRoom(Player enteredother)
    {
        Debug.LogFormat("OnPlayerEnteredRoom()" + enteredother.NickName);

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", PhotonNetwork.IsMasterClient);
        }
    }

    // 退室
    public override void OnPlayerLeftRoom(Player leftother)
    {
        Debug.LogFormat("OnPlayerLeftRoom()" + leftother.NickName);

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerleftRoom() {0}", PhotonNetwork.IsMasterClient);
        }
    }
    #endregion


}
