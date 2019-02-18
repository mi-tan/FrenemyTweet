using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

/// <summary>
/// 銃
/// </summary>
class Rifle : RangeWeapon
{
    private PlayerStateManager playerStateManager;
    private PlayerAnimationManager playerAnimationManager;
    private PlayerProvider playerProvider;
    private PlayerCamera playerCamera;

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
    /// 発射間隔
    /// </summary>
    private float shotInterval = 0.107f;
    private float time = 0f;

    private Coroutine reloadCoroutine;
    const float RELOAD_TIME = 1.15f;

    private Coroutine cancelableCoroutine;
    const float CANCELABLE_TIME = 0.2f;

    private Coroutine muzzleFlashCoroutine;
    const float MUZZLE_FLASH_TIME = 0.02f;

    [SerializeField]
    private Transform muzzleTrans;

    //[SerializeField]
    //private GameObject bulletHitPrefab;
    const string BULLET_HIT_PREFAB_NAME = "BulletHitPrefab";

    [SerializeField]
    private GameObject muzzleFlash;

    private bool isFirstBullet;

    private CharacterController characterController;

    private float shakeTime = 0.1f;
    private float shakeX = 0.03f;
    private float shakeY = 0.06f;

    private SoundManager soundManager;

    [SerializeField]
    private AudioClip shotSound;
    [SerializeField]
    private float shotVolume;


    void Awake()
    {
        // コンポーネントを取得
        playerStateManager = GetComponent<PlayerStateManager>();
        playerAnimationManager = GetComponent<PlayerAnimationManager>();
        playerProvider = GetComponent<PlayerProvider>();
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponent<PlayerCamera>();

        soundManager = GetComponent<SoundManager>();
    }

    private void Start()
    {
        bulletNumber = maxBulletNumber;

        // マズルフラッシュを非表示
        muzzleFlash.SetActive(false);
    }

    public override void UpdateAttack(float inputAttack, float inputMoveHorizontal, float inputMoveVertical)
    {
        if (playerStateManager.GetPlayerState() == PlayerStateManager.PlayerState.DEATH) { return; }

        if (inputAttack >= 1)
        {
            if (!isInput)
            {
                isInput = true;
                isFirstBullet = false;
            }
        }
        else
        {
            isInput = false;
        }

        playerCamera.SetIsAim(isInput);

        if (playerStateManager.GetPlayerState() == PlayerStateManager.PlayerState.DODGE &&
            reloadCoroutine != null)
        {
            StopCoroutine(reloadCoroutine);
            reloadCoroutine = null;
            playerStateManager.SetIsCancelable(false);
        }

        if(playerStateManager.GetPlayerState() != PlayerStateManager.PlayerState.ATTACK)
        {
            isFirstBullet = false;

            // マズルフラッシュを非表示
            muzzleFlash.SetActive(false);
        }

        if (playerStateManager.GetPlayerState() != PlayerStateManager.PlayerState.ACTABLE &&
            playerStateManager.GetPlayerState() != PlayerStateManager.PlayerState.ATTACK) { return; }

        // 通常攻撃アニメーションを再生
        playerAnimationManager.SetBoolAttack(isInput);

        if (isInput)
        {
            Vector3 attackDirection = Vector3.Scale(
                playerProvider.GetMainCamera().transform.forward, new Vector3(1, 0, 1)).normalized;
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

        Vector3 cameraForward = Vector3.Scale(playerProvider.GetMainCamera().transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveForward = cameraForward * inputMoveVertical + playerProvider.GetMainCamera().transform.right * inputMoveHorizontal;

        characterController.Move(moveForward * STANCE_MOVE_SPEED * Time.deltaTime);
    }

    void ShootBullet(bool isInput)
    {
        if (isInput)
        {
            if (bulletNumber > 0)
            {
                time += Time.deltaTime;

                if (time >= shotInterval || !isFirstBullet)
                {
                    if (!isFirstBullet)
                    {
                        cancelableCoroutine = StartCoroutine(Cancelable());
                        isFirstBullet = true;
                    }

                    time = 0f;

                    //Debug.Log("弾発射");

                    bulletNumber--;

                    muzzleFlashCoroutine = StartCoroutine(MuzzleFlash());

                    // カメラを揺らす
                    playerCamera.ShakeCamera(shakeTime, shakeX, shakeY);

                    if (soundManager && shotSound)
                    {
                        // 発射音再生
                        soundManager.PlaySound(shotSound, shotVolume);
                    }

                    Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2);
                    Ray ray = playerProvider.GetMainCamera().ScreenPointToRay(center);
                    RaycastHit hit;

                    //Quaternion qua = new Quaternion();

                    if (Physics.Raycast(ray, out hit, 1000.0f, LayerMask.GetMask(new string[] { "Field", "Enemy" })))
                    {
                        //Vector3 vec = (hit.point - muzzleTrans.position).normalized;
                        //qua = Quaternion.LookRotation(vec);

                        GameObject g = PhotonNetwork.Instantiate(BULLET_HIT_PREFAB_NAME, hit.point, transform.rotation);
                        Destroy(g, 1f);

                        ExecuteEvents.Execute<IDamage>(
                            target: hit.collider.gameObject,
                            eventData: null,
                            functor: (iDamage, eventData) => iDamage.TakeDamage((int)(playerProvider.GetBasicAttackPower() * 0.5f))
                        );
                    }
                    else
                    {
                        Debug.LogWarning("Rayで照準位置が取得できていない(Rifle)");
                    }
                }
            }
        }
        else
        {
            time = -0.1f;
        }
    }

    private IEnumerator Reload()
    {
        if (reloadCoroutine != null) { yield break; }

        cancelableCoroutine =  StartCoroutine(Cancelable());

        yield return new WaitForSeconds(RELOAD_TIME);

        if (playerStateManager.GetPlayerState() == PlayerStateManager.PlayerState.RELOAD)
        {
            // プレイヤーの状態を行動可能に変更
            playerStateManager.SetPlayerState(PlayerStateManager.PlayerState.ACTABLE);

            bulletNumber = maxBulletNumber;
        }

        reloadCoroutine = null;
    }

    private IEnumerator Cancelable()
    {
        if (cancelableCoroutine != null) { yield break; }

        playerStateManager.SetIsCancelable(false);

        yield return new WaitForSeconds(CANCELABLE_TIME);

        playerStateManager.SetIsCancelable(true);

        cancelableCoroutine = null;
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
