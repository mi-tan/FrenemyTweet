using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.EventSystems;
using Photon.Pun;

[CreateAssetMenu(menuName = "ScriptableObject/EnemySkill/HalfCircleAttack")]
public class HalfCircleAttack : EnemySkillBase
{
    private GameObject instantAreaObject = null;
    private GameObject instantEffect = null;
    /// <summary>
    /// 攻撃するプレイヤーを格納
    /// </summary>
    private List<GameObject> attackPlayers = new List<GameObject>();

    /// <summary>
    /// 回転角度
    /// </summary>
    private int rotateAngle = 90;
    private enum AttackAngle
    {
        FrontAttack = 0,
        BackAttack  = 1,
        RightAttack = 2,
        LeftAttack  = 3
    }

    [SerializeField]
    private AttackAngle attackState;

    /// <summary>
    /// エリア生成時のQuaternion
    /// </summary>
    private Quaternion instantQua;

    /// <summary>
    /// 攻撃する角度
    /// </summary>
    private Quaternion[] attackAngles = {
        // front
        new Quaternion(0.0f, 1.0f, 0.0f, 0.0f),
        // back
        new Quaternion(0.0f, 0.0f, 0.0f, 1.0f),
        // right
        new Quaternion(0.0f, 0.7f, 0.0f, -0.7f),
        // left
        new Quaternion(0.0f, 0.7f, 0.0f, 0.7f)};

    //private Quaternion front = new Quaternion(0.0f, 1.0f, 0.0f, 0.0f);
    //private Quaternion back = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
    //private Quaternion right = new Quaternion(0.0f, 0.7f, 0.0f, -0.7f);
    //private Quaternion left =  new Quaternion(0.0f, 0.7f, 0.0f, 0.7f);


    public override void ActivateSkill(Transform thisTransform)
    {
        //Quaternion attackRotate = AttackAngleSet(attackState);
        int randNum = Random.Range(0, attackAngles.Length);

        //instantAreaObject = null;
        Vector3 instantPos = new Vector3(thisTransform.position.x, thisTransform.position.y + useAreaObj.transform.position.y, thisTransform.position.z);
        // エリア生成
        instantAreaObject = PhotonNetwork.Instantiate(useAreaObj.name, instantPos, thisTransform.rotation);

        // 詠唱
        Observable.TimerFrame(getSkillChantFrame).Subscribe(_ =>
            AttackPlayerSearch(instantPos, thisTransform.rotation)
        ).AddTo(thisTransform.gameObject);
    }

    /// <summary>
    /// 攻撃するプレイヤーを探す
    /// </summary>
    /// <param name="thisPosition"></param>
    private void AttackPlayerSearch(Vector3 instantPos ,Quaternion instantRotate)
    {
        PhotonNetwork.Destroy(instantAreaObject);
        // エフェクト生成
        instantEffect = PhotonNetwork.Instantiate(useEffect.name, useEffect.transform.position, useEffect.transform.rotation);

        attackPlayers.Clear();
        // 攻撃するプレイヤーを取得
        attackPlayers = instantAreaObject.GetComponent<AttackArea>().GetAcquisitionPlayerList;
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
            Debug.Log("半径攻撃：【" + attackPlayers[index].gameObject.name + "】へ【" + getAtackPower + "】ダメージ");
        }
    }
}
