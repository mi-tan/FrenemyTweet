using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

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
    const float FACE_SPEED = 1100f;

    private Quaternion quaternion;


    // Use this for initialization
    void Start () {
        // コンポーネントを取得
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 移動入力を取得
        float moveHorizontal = Input.GetAxisRaw(INPUT_MOVE_HORIZONTAL);
        float moveVertical = Input.GetAxisRaw(INPUT_MOVE_VERTICAL);

        // 移動方向に向く
        UpdateFace();

        // 移動入力されていたら
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            // 走るアニメーションを再生
            playerAnimator.SetBool("run", true);

            // 移動方向を計算
            Vector3 moveDirection = new Vector3(moveHorizontal, 0, moveVertical);
            quaternion = Quaternion.LookRotation(moveDirection);
        }
        else
        {
            // 走るアニメーションを停止
            playerAnimator.SetBool("run", false);
        }
    }

    void UpdateFace()
    {
        // 移動方向に向いていたら、この先の処理を行わない
        if(transform.rotation == quaternion) { return; }

        // 移動方向に徐々に向く
        float step = FACE_SPEED * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternion, step);
    }
}
