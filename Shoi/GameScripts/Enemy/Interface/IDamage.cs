using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

/// <summary>
/// ダメージを受けるインターフェース
/// </summary>
public interface IDamage: IEventSystemHandler
{

    void TakeDamage(int damage);
    void TakeDamage(int damage, PhotonView photonView);

}
