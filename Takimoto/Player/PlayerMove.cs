using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの移動を行うクラス
/// </summary>
public class PlayerMove : MonoBehaviour
{
    private PlayerStateManager playerStateManager;
    private PlayerAnimationController playerAnimationController;

    /// <summary>
    /// 移動水平入力
    /// </summary>
    const string INPUT_MOVE_HORIZONTAL = "MoveHorizontal";
    /// <summary>
    /// 移動垂直入力
    /// </summary>
    const string INPUT_MOVE_VERTICAL = "MoveVertical";

    /// <summary>
    /// 入力の遊び値
    /// </summary>
    const float INPUT_IDLE_VALUE = 0.3f;
    /// <summary>
    /// 移動方向に向く速度
    /// </summary>
    const float FACE_SPEED = 1200f;
    /// <summary>
    /// 移動速度
    /// </summary>
    const float MOVE_SPEED = 5f;

    /// <summary>
    /// 移動方向
    /// </summary>
    private Quaternion moveQuaternion;


    void Awake()
    {
        // コンポーネントを取得
        playerStateManager = GetComponent<PlayerStateManager>();
        playerAnimationController = GetComponent<PlayerAnimationController>();

        // 移動方向を初期化
        moveQuaternion = transform.rotation;
    }

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 移動入力を取得
        float moveHorizontal = Input.GetAxisRaw(INPUT_MOVE_HORIZONTAL);
        float moveVertical = Input.GetAxisRaw(INPUT_MOVE_VERTICAL);

        // 移動方向に向く
        FaceMove(moveQuaternion);

        // 移動入力されていたら
        if (Mathf.Abs(moveHorizontal) > INPUT_IDLE_VALUE ||
            Mathf.Abs(moveVertical) > INPUT_IDLE_VALUE)
        {
            // 走るアニメーションを再生
            playerAnimationController.Animate(
                PlayerAnimationController.PARAMETER_BOOL_RUN, true);

            // 現在のプレイヤーの状態が行動可能だったら
            if (playerStateManager.GetPlayerState() == PlayerStateManager.PlayerState.ACTABLE)
            {
                // 移動方向を計算
                Vector3 moveDirection = new Vector3(moveHorizontal, 0, moveVertical);
                moveQuaternion = Quaternion.LookRotation(moveDirection);

                // 位置を移動
                transform.position += transform.forward * MOVE_SPEED * Time.deltaTime;
            }
            else
            {
                moveQuaternion = transform.rotation;
            }
        }
        else
        {
            // 走るアニメーションを停止
            playerAnimationController.Animate(
                PlayerAnimationController.PARAMETER_BOOL_RUN, false);
        }
    }

    /// <summary>
    /// 移動方向に向く
    /// </summary>
    /// <param name="moveQuaternion">移動方向</param>
    void FaceMove(Quaternion moveQuaternion)
    {
        // 現在のプレイヤーの状態が行動可能ではなかったら、この先の処理を行わない
        if (playerStateManager.GetPlayerState() != PlayerStateManager.PlayerState.ACTABLE) { return; }

        // 移動方向に向いていたら、この先の処理を行わない
        if (transform.rotation == moveQuaternion) { return; }

        // 移動方向に徐々に向く
        float step = FACE_SPEED * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation, moveQuaternion, step);
    }
}
