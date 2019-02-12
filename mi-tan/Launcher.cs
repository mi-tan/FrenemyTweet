using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace HCS.FrenemyTweet
{

    public class Launcher : MonoBehaviourPunCallbacks
    {



        #region Private Serializable Fields
        /// <summary>
        /// 部屋当たりの最大プレイヤー数。
        /// 部屋がいっぱいになると、新しいプレイヤーが参加できないため、新しい部屋が作成されます。
        /// </summary>
        [Tooltip("部屋当たりの最大プレイヤー数")]
        [SerializeField]
        private byte maxPlayersPerRoom = 4;

        #endregion

        #region Private Fields

        string GameVersion = "1";

        bool isConnecting;

        #endregion

        #region MonBehaviour CallBacks

        void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        #endregion

        #region Public Methods


        public void Connect()
        {
            isConnecting = true;

            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.GameVersion = GameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }

            PhotonNetwork.NickName = TwitterParameterManager.Instance.UserID;
            Debug.Log("ニックネーム" + PhotonNetwork.NickName);
        }

        #endregion

        #region MonoBehaviourPunCallbacks Callbacks

        public override void OnConnectedToMaster()
        {
            if (isConnecting)
            {
                PhotonNetwork.JoinRandomRoom();
                Debug.Log("OnConnectedToMaster()がPUNによって呼び出されたよ");
            }
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarningFormat("OnDiconnected()が理由{0}によって呼び出されたよ", cause);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("OnJoinRandomFailed()がPUNによって呼び出されたよ。ランダム部屋が無かったので作成します。\nCalling: PhotonNetwork.CreateRoom");

            PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("OnJoinedRoom()がPUNによって呼び出されたよ。今クライアントはルームにいるよ");

            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                Debug.Log("We load the 'MainGameScene' ");

                PhotonNetwork.LoadLevel("MainGameScene");
            }
        }
        #endregion
    }
}
