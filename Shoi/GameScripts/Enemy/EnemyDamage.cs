using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Photon.Pun;
using Zenject;

/// <summary>
/// ダメージを計算するクラス
/// </summary>
public sealed class EnemyDamage : MonoBehaviour {

    [SerializeField]
    private GameObject damageEffect;
    [SerializeField]
    private Vector3 effectPos;

    private float offsetX = 0f;
    private float offsetY = 0f;
    const float MAX_OFFSET_X = 0.2f;
    const float MAX_OFFSET_Y = 0.2f;

    /// <summary>
    /// 防御力
    /// </summary>
    //private int deffencePower = 0;

    /// <summary>
    /// ダメージアニメーションの数
    /// </summary>
    private int damageAnimationNum = 3;
    /// <summary>
    /// 死亡アニメーションの数
    /// </summary>
    private int deathAnimationNum = 3;
    /// <summary>
    /// 死亡時のエフェクト
    /// </summary>
    private GameObject deathEffect;

    public GameObject setDeathEffect
    {
        set { deathEffect = value; }
    }
    private int useAnimationNum;

    private BossParameter bossParameter;
    private EnemyParameter enemyParameter;
    private EnemyAnimationController enemyAnimationController;
    private PhotonView thisPhotonView;

    [Inject]
    private MainGameManager mainGameManager;

    private void Awake()
    {
        bossParameter = GetComponent<BossParameter>();
        enemyParameter = GetComponent<EnemyParameter>();
        enemyAnimationController = GetComponent<EnemyAnimationController>();
        thisPhotonView = GetComponent<PhotonView>();

        // 使用するアニメーションを設定
        useAnimationNum = Random.Range(0, deathAnimationNum);
    }

    public void TakeDamage(int damage)
    {
        offsetX = Random.Range(0f, MAX_OFFSET_X);
        offsetY = Random.Range(0f, MAX_OFFSET_Y);
        Vector3 createPos = new Vector3(
            transform.position.x + effectPos.x + offsetX,
            transform.position.y + effectPos.y + offsetX,
            transform.position.z + effectPos.z);

        GameObject effect = Instantiate(damageEffect, createPos, transform.rotation);
        DamageCanvas damageCanvas = effect.GetComponent<DamageCanvas>();

        if (damageCanvas != null)
        {
            damageCanvas.SetDamageValue(damage);
        }
        else
        {
            Debug.LogWarning("だめーじきゃんばすない！");
        }

        if (bossParameter != null)
        {
            // ボスのHPを減らす
            bossParameter.hp -= damage;
        }
        else if(enemyParameter != null)
        {
            // 雑魚のHPを減らす
            enemyParameter.hp -= damage;
        }

        //Debug.Log($"受けたダメージ： {damage}");
    }
    public void TakeDamage(int damage, PhotonView photonView)
    {
        // 所有者変更
        thisPhotonView.TransferOwnership(photonView.Owner);

        offsetX = Random.Range(0f, MAX_OFFSET_X);
        offsetY = Random.Range(0f, MAX_OFFSET_Y);
        Vector3 createPos = new Vector3(
            transform.position.x + effectPos.x + offsetX,
            transform.position.y + effectPos.y + offsetX,
            transform.position.z + effectPos.z);

        DamageCanvas effect = PhotonNetwork.Instantiate(damageEffect.name, createPos, transform.rotation).GetComponent<DamageCanvas>();
        effect.SetDamageValue(damage);

        if (bossParameter != null)
        {
            // ボスのHPを減らす
            bossParameter.hp -= damage;
        }
        else if (enemyParameter != null)
        {
            // 雑魚のHPを減らす
            enemyParameter.hp -= damage;
        }
    }

    /// <summary>
    /// 被ダメージ時のアニメーション再生
    /// </summary>
    public void TakeDamageAnimation()
    {
        int randNum = Random.Range(0, damageAnimationNum);
        // 被ダメージ時のアニメーション再生
        enemyAnimationController.TakeDamage(randNum);
        
        //Observable.TimerFrame(enemyAnimationController.GetFlagOffFrame).Subscribe(_ =>
        //{
        //    // 被ダメージ時のアニメーション停止
        //    enemyAnimationController.TakeDamage(false);
        //}).AddTo(gameObject);
    }

    /// <summary>
    /// 死亡処理
    /// </summary>
    public void DeathEnemy()
    {
        // アニメーションを呼び出し
        enemyAnimationController.Death(useAnimationNum);
    }

    /// <summary>
    /// オブジェクトを削除
    /// </summary>
    public void DestroyEnemy()
    {
        if (deathEffect == null)
        {
            Debug.LogWarning("死亡時のエフェクトが設定されていません");
        }
        else
        {
            // 死亡エフェクト生成
            PhotonNetwork.Instantiate(deathEffect.name, transform.position, transform.rotation);
        }

        PhotonNetwork.Destroy(gameObject);
    }
}
