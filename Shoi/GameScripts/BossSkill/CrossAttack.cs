using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.EventSystems;


[CreateAssetMenu(menuName = "ScriptableObject/EnemySkill/CrossAttack")]
public class CrossAttack : EnemySkillBase
{

    private GameObject instantAreaObject;
    /// <summary>
    /// 攻撃するプレイヤーを格納
    /// </summary>
    private List<GameObject> attackPlayers = new List<GameObject>();


    public override void ActivateSkill(Transform thisTransform)
    {
        instantAreaObject = null;
        instantAreaObject = Instantiate(useAreaObj, thisTransform.position, thisTransform.rotation);

        // 詠唱
        Observable.TimerFrame(getSkillChantFrame).Subscribe(_ =>
            AttackPlayerSearch(thisTransform.position)
        ).AddTo(thisTransform.gameObject);
    }

    /// <summary>
    /// 攻撃するプレイヤーを探す
    /// </summary>
    /// <param name="thisPosition"></param>
    private void AttackPlayerSearch(Vector3 thisPosition)
    {
        attackPlayers.Clear();
        // 攻撃するプレイヤーを取得
        attackPlayers = instantAreaObject.GetComponent<AttackArea>().GetAcquisitionPlayerList;
        Attack(attackPlayers);
        Destroy(instantAreaObject);
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
            Debug.Log("十字攻撃：【" + attackPlayers[index].gameObject + "】へ【" + getAtackPower + "】ダメージ");
        }
    }
}
