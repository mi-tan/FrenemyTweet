using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CreateClone : MonoBehaviour
{
    //[SerializeField]
    //private GameObject[] clones;
    //[SerializeField]
    //private GameObject clone;

    const string PREFAB_NAME = "BotPrefab";

    public void Create(int weaponNum)
    {
        //Instantiate(clones[weaponNum], transform.position, transform.rotation);
        PhotonNetwork.Instantiate(PREFAB_NAME, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
