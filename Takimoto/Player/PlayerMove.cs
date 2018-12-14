using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの移動を行うクラス
/// </summary>
public class PlayerMove : MonoBehaviour {

    private PlayerManager playerManager;
    private Animator playerAnimator;

    /// <summary>
    /// 移動水平入力
    /// </summary>
    const string INPUT_MOVE_HORIZONTAL = "MoveHorizontal";
    /// <summary>
    /// 移動垂直入力
    /// </summary>
    const string INPUT_MOVE_VERTICAL = "MoveVertical";

    /// <summary>
    /// 向く速度
    /// </summary>
    const float FACE_SPEED = 1200f;
    /// <summary>
    /// 入力の遊び値
    /// </summary>
    const float INPUT_IDLE_VALUE = 0.3f;

    /// <summary>
    /// 走るパラメータ
    /// </summary>
    const string PARAMETER_RUN = "Run";

    /// <summary>
    /// 移動角度
    /// </summary>
    Quaternion moveQuaternion;


    void Awake()
    {
        // コンポーネントを取得
        playerManager = GetComponent<PlayerManager>();
        playerAnimator = GetComponent<Animator>();

        // 移動角度を初期化
        moveQuaternion = transform.rotation;
    }

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update()
    {
        // 移動入力を取得
        float moveHorizontal = Input.GetAxisRaw(INPUT_MOVE_HORIZONTAL);
        float moveVertical = Input.GetAxisRaw(INPUT_MOVE_VERTICAL);

        // 移動入力されていたら
        if (moveHorizontal > INPUT_IDLE_VALUE || moveHorizontal < -INPUT_IDLE_VALUE ||
            moveVertical > INPUT_IDLE_VALUE || moveVertical < -INPUT_IDLE_VALUE)
        {
            // 走るアニメーションを再生
            playerAnimator.SetBool(PARAMETER_RUN, true);

            // 移動角度を計算
            Vector3 moveDirection = new Vector3(moveHorizontal, 0, moveVertical);
            moveQuaternion = Quaternion.LookRotation(moveDirection);
        }
        else
        {
            // 走るアニメーションを停止
            playerAnimator.SetBool(PARAMETER_RUN, false);
        }

        // 移動角度に向く
        UpdateFace(moveQuaternion);
    }

    /// <summary>
    /// 移動角度に向く
    /// </summary>
    /// <param name="moveQuaternion">移動角度</param>
    void UpdateFace(Quaternion moveQuaternion)
    {
        // プレイヤーの状態が行動可能ではなかったら、この先の処理を行わない
        if (playerManager.GetPlayerState() != PlayerManager.PlayerState.ACTABLE) { return; }

        // 移動角度に向いていたら、この先の処理を行わない
        if (transform.rotation == moveQuaternion) { return; }

        // 移動角度に徐々に向く
        float step = FACE_SPEED * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, moveQuaternion, step);
    }
}
