using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 銃
/// </summary>
class Rifle : RangeWeapon
{
    private PlayerStateManager playerStateManager;
    private PlayerAnimationManager playerAnimationManager;
    private PlayerProvider playerProvider;

    /// <summary>
    /// 入力中か
    /// </summary>
    private bool isInput = false;

    /// <summary>
    /// 攻撃角度
    /// </summary>
    private Quaternion attackQuaternion;

    /// <summary>
    /// 構え移動速度
    /// </summary>
    const float STANCE_MOVE_SPEED = 1.5f;

    /// <summary>
    /// 最大弾数
    /// </summary>
    private int maxBulletNumber = 30;
    public int GetMaxBulletNumber()
    {
        return maxBulletNumber;
    }
    /// <summary>
    /// 弾数
    /// </summary>
    private int bulletNumber = 30;
    public int GetBulletNumber()
    {
        return bulletNumber;
    }

    /// <summary>
    /// 発射間隔
    /// </summary>
    private float shotInterval = 0.107f;
    private float time = 0f;

    private Coroutine reloadCoroutine;
    const float RELOAD_TIME = 1.2f;

    private Coroutine reloadCancelableCoroutine;
    const float RELOAD_CANCELABLE_TIME = 0.3f;

    private Coroutine muzzleFlashCoroutine;
    const float MUZZLE_FLASH_TIME = 0.04f;

    [SerializeField]
    private Transform muzzleTrans;

    [SerializeField]
    private GameObject bulletHitPrefab;

    [SerializeField]
    private GameObject muzzleFlash;


    void Awake()
    {
        // コンポーネントを取得
        playerStateManager = GetComponent<PlayerStateManager>();
        playerAnimationManager = GetComponent<PlayerAnimationManager>();
        playerProvider = GetComponent<PlayerProvider>();
    }

    private void Start()
    {
        bulletNumber = maxBulletNumber;

        // マズルフラッシュを非表示
        muzzleFlash.SetActive(false);
    }

    public override void UpdateAttack(float inputAttack, float inputMoveHorizontal, float inputMoveVertical)
    {
        if (inputAttack >= 1)
        {
            if (!isInput)
            {
                isInput = true;
            }
        }
        else
        {
            isInput = false;
        }

        if (playerStateManager.GetPlayerState() == PlayerStateManager.PlayerState.DODGE &&
            reloadCoroutine != null)
        {
            StopCoroutine(reloadCoroutine);
            reloadCoroutine = null;
            playerStateManager.SetIsCancelable(false);
        }

        if (playerStateManager.GetPlayerState() != PlayerStateManager.PlayerState.ACTABLE &&
            playerStateManager.GetPlayerState() != PlayerStateManager.PlayerState.ATTACK) { return; }

        // 通常攻撃アニメーションを再生
        playerAnimationManager.SetBoolAttack(isInput);

        if (isInput)
        {
            Vector3 attackDirection = Vector3.Scale(
                Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            attackQuaternion = Quaternion.LookRotation(attackDirection);

            // 攻撃方向に向く
            FaceAttack(attackQuaternion);

            if (bulletNumber > 0)
            {
                // プレイヤーの状態を攻撃中に変更
                playerStateManager.SetPlayerState(PlayerStateManager.PlayerState.ATTACK);

                // 構え移動
                StanceMove(inputMoveHorizontal, inputMoveVertical);
            }
            else
            {
                if (reloadCoroutine == null)
                {
                    // プレイヤーの状態をリロード中に変更
                    playerStateManager.SetPlayerState(PlayerStateManager.PlayerState.RELOAD);

                    // 通常攻撃アニメーションを停止
                    playerAnimationManager.SetBoolAttack(false);

                    // リロードアニメーションを再生
                    playerAnimationManager.SetTriggerReload();

                    reloadCoroutine = StartCoroutine(Reload());

                    //Debug.Log("リロード");
                }
            }
        }
        else
        {
            if (playerStateManager.GetPlayerState() == PlayerStateManager.PlayerState.ATTACK)
            {
                // プレイヤーの状態を行動可能に変更
                playerStateManager.SetPlayerState(PlayerStateManager.PlayerState.ACTABLE);
            }
        }

        // 弾を発射
        ShootBullet(isInput);
    }

    /// <summary>
    /// 攻撃方向に向く
    /// </summary>
    /// <param name="attackQuaternion">攻撃角度</param>
    void FaceAttack(Quaternion attackQuaternion)
    {
        // 攻撃角度に向いていなかったら
        if (transform.rotation != attackQuaternion)
        {
            // 攻撃角度に向く
            transform.rotation = attackQuaternion;
        }
    }

    /// <summary>
    /// 構え移動
    /// </summary>
    void StanceMove(float inputMoveHorizontal, float inputMoveVertical)
    {
        if (playerStateManager.GetPlayerState() != PlayerStateManager.PlayerState.ATTACK) { return; }

        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveForward = cameraForward * inputMoveVertical + Camera.main.transform.right * inputMoveHorizontal;

        transform.position += moveForward * STANCE_MOVE_SPEED * Time.deltaTime;
    }

    void ShootBullet(bool isInput)
    {
        if (isInput)
        {
            if (bulletNumber > 0)
            {
                time += Time.deltaTime;

                if (time >= shotInterval)
                {
                    time = 0f;

                    //Debug.Log("弾発射");

                    bulletNumber--;

                    //Debug.Log("弾数：" + bulletNumber);

                    muzzleFlashCoroutine = StartCoroutine(MuzzleFlash());

                    Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2);
                    Ray ray = Camera.main.ScreenPointToRay(center);
                    RaycastHit hit;

                    Quaternion qua = new Quaternion();

                    if (Physics.Raycast(ray, out hit, 1000.0f, LayerMask.GetMask(new string[] { "Field", "Enemy" })))
                    {
                        // 壁に当たった処理

                        Vector3 vec = (hit.point - muzzleTrans.position).normalized;
                        qua = Quaternion.LookRotation(vec);

                        GameObject g = Instantiate(bulletHitPrefab, hit.point, transform.rotation);
                        Destroy(g, 1f);

                        ExecuteEvents.Execute<IDamage>(
                            target: hit.collider.gameObject,
                            eventData: null,
                            functor: (iDamage, eventData) => iDamage.TakeDamage(playerProvider.GetBasicAttackPower())
                        );
                    }
                    else
                    {
                        Debug.LogWarning("Rayで照準位置が取得できていない(Laser)");
                    }
                }
            }
        }
        else
        {
            time = 0f;
        }
    }

    private IEnumerator Reload()
    {
        if (reloadCoroutine != null) { yield break; }

        reloadCancelableCoroutine =  StartCoroutine(ReloadCancelable());

        yield return new WaitForSeconds(RELOAD_TIME);

        if (playerStateManager.GetPlayerState() == PlayerStateManager.PlayerState.RELOAD)
        {
            // プレイヤーの状態を行動可能に変更
            playerStateManager.SetPlayerState(PlayerStateManager.PlayerState.ACTABLE);
            playerStateManager.SetIsCancelable(false);

            bulletNumber = maxBulletNumber;
        }

        reloadCoroutine = null;
    }

    private IEnumerator ReloadCancelable()
    {
        if (reloadCancelableCoroutine != null) { yield break; }

        yield return new WaitForSeconds(RELOAD_CANCELABLE_TIME);

        playerStateManager.SetIsCancelable(true);

        reloadCancelableCoroutine = null;
    }

    private IEnumerator MuzzleFlash()
    {
        if (muzzleFlashCoroutine != null) { yield break; }

        muzzleFlash.SetActive(true);

        yield return new WaitForSeconds(MUZZLE_FLASH_TIME);

        muzzleFlash.SetActive(false);

        muzzleFlashCoroutine = null;
    }
}
