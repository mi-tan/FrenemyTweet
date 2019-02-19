using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.EventSystems;
using Photon.Pun;

[CreateAssetMenu(menuName = "ScriptableObject/EnemySkill/DounutAttack")]
public class DounutAttack : EnemySkillBase {

    private GameObject instantEffect;

    /// <summary>
    /// 攻撃するプレイヤーを格納
    /// </summary>
    private List<GameObject> attackPlayers = new List<GameObject>();

    public override void ActivateSkill(Transform thisTransform)
    {
        // エリア生成
        useAreaObj.SetActive(true);

        // 詠唱
        Observable.TimerFrame(getSkillChantFrame).Subscribe(_ =>
            AttackPlayerSearch(thisTransform.position, thisTransform.rotation)
        ).AddTo(thisTransform.gameObject);
    }

    /// <summary>
    /// 攻撃するプレイヤーを探す
    /// </summary>
    /// <param name="thisPosition"></param>
    private void AttackPlayerSearch(Vector3 instantPos, Quaternion instantRotate)
    {
        // エフェクト生成
        //instantEffect = PhotonNetwork.Instantiate(useEffect.name, instantPos, instantRotate);
        useAreaObj.SetActive(true);

        attackPlayers.Clear();
        // 攻撃するプレイヤーを取得
        attackPlayers = useAreaObj.GetComponent<AttackArea>().GetAcquisitionPlayerList;

        useAreaObj.SetActive(false);
        Attack(attackPlayers);
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    /// <param name="attackPlayers"></param>
    private void Attack(List<GameObject> attackPlayers)
    {
        for (int index = 0; attackPlayers.Count > index; index++)
        {
            ExecuteEvents.Execute<IDamage>(
                target: attackPlayers[index].gameObject,
                eventData: null,
                functor: (iDamage, eventData) => iDamage.TakeDamage(getAtackPower)
            );
            Debug.Log("外周攻撃：【" + attackPlayers[index].gameObject.name + "】へ【" + getAtackPower + "】ダメージ");
        }
    }
}
