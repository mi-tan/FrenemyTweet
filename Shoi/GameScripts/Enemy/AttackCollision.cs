using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 衝突したオブジェクトにダメージを与えるクラス
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class AttackCollision : MonoBehaviour {

    private int attackPower = 10;

    public int setAttackPower{ set { attackPower = value; } }

    private void Awake()
    {
        Rigidbody rb;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    /// <summary>
    /// 衝突判定
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        CallTakeDamage(other.gameObject);
    }

    /// <summary>
    /// TakeDamageを呼び出す
    /// </summary>
    /// <param name="targetObj"></param>
    private void CallTakeDamage(GameObject targetObj)
    {
        ExecuteEvents.Execute<IDamage>(
            target: targetObj.gameObject,
            eventData: null,
            functor: (iDamage, eventData) => iDamage.TakeDamage(attackPower)
        );
    }
}
