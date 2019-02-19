using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.EventSystems;
using Photon.Pun;


[CreateAssetMenu(menuName = "ScriptableObject/EnemySkill/CrossAttack")]
public class CrossAttack : EnemySkillBase
{

    private GameObject instantEffect;
    /// <summary>
    /// 攻撃するプレイヤーを格納
    /// </summary>
    private List<GameObject> attackPlayers = new List<GameObject>();

    /// <summary>
    /// 攻撃する角度
    /// </summary>
    private Quaternion[] attackCrossAngles = {
        // 十字
        new Quaternion(0.0f, 1.0f, 0.0f, 0.0f),
        // Ⅹ字
        new Quaternion(0.0f, 0.9f, 0.0f, 0.4f),
    };


    public override void ActivateSkill(Transform thisTransform)
    {
        // エリア生成
        useAreaObj.SetActive(true);
        int randNum = Random.Range(0, attackCrossAngles.Length);
        Quaternion useAngle = attackCrossAngles[randNum];

        useAreaObj.transform.rotation = useAngle;
        // 詠唱
        Observable.TimerFrame(getSkillChantFrame).Subscribe(_ =>
            AttackPlayerSearch(useAngle)
        ).AddTo(thisTransform.gameObject);
    }


    /// <summary>
    /// 攻撃するプレイヤーを探す
    /// </summary>
    /// <param name="thisPosition"></param>
    private void AttackPlayerSearch(Quaternion useAngle)
    {

        // エフェクト生成
        instantEffect = PhotonNetwork.Instantiate(useEffect.name, useAreaObj.transform.position, useAngle);

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
            Debug.Log("十字攻撃：【" + attackPlayers[index].gameObject.name + "】へ【" + getAtackPower + "】ダメージ");
        }
    }
}
