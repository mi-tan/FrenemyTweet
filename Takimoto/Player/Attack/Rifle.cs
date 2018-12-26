using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 銃
/// </summary>
class Rifle : RangeWeapon
{
    private PlayerStateManager playerStateManager;
    private PlayerAnimationManager playerAnimationManager;

    /// <summary>
    /// 入力中か
    /// </summary>
    private bool isInput = false;

    [SerializeField]
    private Transform muzzle;

    [SerializeField]
    private AttackCollision bulletPrefab;

    /// <summary>
    /// 攻撃力
    /// </summary>
    private int attackPower = 5;
    /// <summary>
    /// 弾の消滅時間
    /// </summary>
    private float destroyTime = 2.4f;

    private float time = 0f;
    /// <summary>
    /// 撃つ間隔時間
    /// </summary>
    private float shotTime = 0.1f;

    /// <summary>
    /// 最大弾数
    /// </summary>
    const int MAX_BULLET_NUM = 30;
    /// <summary>
    /// 弾数
    /// </summary>
    private int bulletNum = 3000;
    

    void Awake()
    {
        // コンポーネントを取得
        playerStateManager = GetComponent<PlayerStateManager>();
        playerAnimationManager = GetComponent<PlayerAnimationManager>();
    }

    public override void UpdateAttack(float inputAttack)
    {
        // 現在のプレイヤーの状態が行動可能か攻撃中ではなかったら、この先の処理を行わない
        if (playerStateManager.GetPlayerState() != PlayerStateManager.PlayerState.ACTABLE &&
            playerStateManager.GetPlayerState() != PlayerStateManager.PlayerState.ATTACK) { return; }

        if (inputAttack >= 1)
        {
            // プレイヤーの状態を攻撃中に変更
            playerStateManager.SetPlayerState(PlayerStateManager.PlayerState.ATTACK);

            // 攻撃方向に向く
            Vector3 attackDirection = Vector3.Scale(
                Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            transform.rotation = Quaternion.LookRotation(attackDirection);

            time += Time.deltaTime;
            if (time >= shotTime)
            {
                time = 0f;
                ShootBullet();
            }

            isInput = true;
        }
        else
        {
            // プレイヤーの状態を攻撃中に変更
            playerStateManager.SetPlayerState(PlayerStateManager.PlayerState.ACTABLE);

            time = 0f;

            isInput = false;
        }

        // 通常攻撃アニメーションを再生
        playerAnimationManager.SetBoolAttack(isInput);
    }

    void ShootBullet()
    {
        if (bulletNum > 0)
        {
            // 弾消費
            bulletNum--;

            Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2);
            Ray ray = Camera.main.ScreenPointToRay(center);
            RaycastHit hit;

            Quaternion qua = new Quaternion();

            if (Physics.Raycast(ray, out hit, 1000.0f, LayerMask.GetMask(new string[] { "Field", "Enemy" })))
            {
                Vector3 vec = (hit.point - muzzle.position).normalized;
                qua = Quaternion.LookRotation(vec);
            }
            else
            {
                Debug.LogWarning("Rayで照準位置が取得できていない(Rifle)");
            }

            AttackCollision attackCollision = Instantiate(bulletPrefab, muzzle.position, qua);
            attackCollision.SetAttackPower = attackPower;
            Destroy(attackCollision.gameObject, destroyTime);
        }
        else
        {
            Debug.Log("リロード処理");
        }
    }
}
