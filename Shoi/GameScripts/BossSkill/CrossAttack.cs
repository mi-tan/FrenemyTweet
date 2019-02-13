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
        int randNum = Random.Range(0, attackCrossAngles.Length);

        instantAreaObject = null;
        instantAreaObject = Instantiate(useAreaObj, thisTransform.position, attackCrossAngles[randNum]);

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
            Debug.Log("十字攻撃：【" + attackPlayers[index].gameObject.name + "】へ【" + getAtackPower + "】ダメージ");
        }
    }
}
