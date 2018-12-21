using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using System;

/// <summary>
/// 衝突したオブジェクトにダメージを与えるクラス
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public sealed class AttackCollision : MonoBehaviour {

    /// <summary>
    /// 攻撃力
    /// </summary>
    private int attackPower = 10;
    public int SetAttackPower{ set { attackPower = value; } }

    /// <summary>
    /// 攻撃時のエフェクト
    /// </summary>
    private GameObject hitEffect;
    public GameObject SetHitEffect { set { hitEffect = value; } }

    private Collider myCollider;
    private bool debugFlag = false;
    public bool SetDebugFlag { set { debugFlag = value; } }

    public MeshRenderer MyMeshRenderer
    {
        get
        {
            if (!myMeshRenderer){
                myMeshRenderer = GetComponent<MeshRenderer>();
            }
            return myMeshRenderer;
        }
    }

    /// <summary>
    /// イベントを入れるところ
    /// </summary>
    private Subject<Unit> attackCollisionSubject = new Subject<Unit>();

    public IObservable<Unit> OnAttackCollision
    {
        get { return attackCollisionSubject; }
    }

    private MeshRenderer myMeshRenderer;

    private void Awake()
    {
        Rigidbody rb;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        myCollider = GetComponent<Collider>();
        
        myCollider.isTrigger = true;
        // myCollider.enabled = false;
    }

    /// <summary>
    /// 衝突判定
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        // 登録されたイベント実行
        attackCollisionSubject.OnNext(Unit.Default);
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

    /// <summary>
    /// 攻撃開始
    /// </summary>
    public void AttackStart()
    {
        // attackStartSubject.OnNext(Unit.Default);
        // デバッグフラグがオフの場合は通常時の処理
        if (debugFlag)
        {
            // デバッグ処理
            MyMeshRenderer.enabled = true;
        }

        myCollider.enabled = true;
       
    }

    /// <summary>
    /// 攻撃終了
    /// </summary>
    public void AttackEnd()
    {
        // デバッグフラグがオフの場合は通常時の処理
        if (debugFlag)
        {
            // デバッグ処理
            MyMeshRenderer.enabled = false;
        }

        myCollider.enabled = false;
    }
}
