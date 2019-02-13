using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameParamaterManager : SingletonMonoBehaviour<GlobalGameParamaterManager>, IPunObservable {

    private float timeCount = 0;

    private PhotonView photonView;

    public float TimeCount
    {
        get
        {
            return timeCount;
        }

        set
        {
            timeCount = value;
        }
    }

    private List<GameObject> enemys = new List<GameObject>();

    public List<GameObject> Enemys
    {
        get
        {
            return enemys;
        }

        set
        {
            enemys = value;
        }
    }

    //protected override void Awake()
    //{
    //    base.Awake();
    //    photonView = GetComponent<PhotonView>();
    //}

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //データの送信
            stream.SendNext(TimeCount);
        }
        else
        {
            //データの受信
            float time = (float)stream.ReceiveNext();
            TimeCount = time;
        }
    }
}
