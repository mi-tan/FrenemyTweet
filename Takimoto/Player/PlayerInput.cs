using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private IPlayerMove iPlayerMove;
    private IPlayerAttack iPlayerAttack;
    private PlayerCamera playerCamera;
    private PlayerSkill playerSkill;

    /// <summary>
    /// 移動水平入力
    /// </summary>
    const string INPUT_MOVE_HORIZONTAL = "MoveHorizontal";
    /// <summary>
    /// 移動垂直入力
    /// </summary>
    const string INPUT_MOVE_VERTICAL = "MoveVertical";
    /// <summary>
    /// 攻撃入力
    /// </summary>
    const string INPUT_ATTACK = "Attack";
    /// <summary>
    /// カメラ水平マウス入力
    /// </summary>
    const string INPUT_MOUSE_X = "Mouse X";
    /// <summary>
    /// カメラ垂直マウス入力
    /// </summary>
    const string INPUT_MOUSE_Y = "Mouse Y";
    /// <summary>
    /// カメラ水平コントローラ入力
    /// </summary>
    const string INPUT_ROTATION_HORIZONTAL = "RotationHorizontal";
    /// <summary>
    /// カメラ垂直コントローラ入力
    /// </summary>
    const string INPUT_ROTATION_VERTICAL = "RotationVertical";


    void Awake()
    {
        // コンポーネントを取得
        iPlayerMove = GetComponent<IPlayerMove>();
        iPlayerAttack = GetComponent<IPlayerAttack>();
        playerCamera = GetComponent<PlayerCamera>();
        playerSkill = GetComponent<PlayerSkill>();
    }

    void Update()
    {
        // 入力を取得
        float inputMoveHorizontal = Input.GetAxisRaw(INPUT_MOVE_HORIZONTAL);
        float inputMoveVertical = Input.GetAxisRaw(INPUT_MOVE_VERTICAL);
        float inputAttack = Input.GetAxisRaw(INPUT_ATTACK);
        float inputMouseX = Input.GetAxis("Mouse X");
        float inputMouseY = Input.GetAxis("Mouse Y");
        float inputRotationHorizontal = Input.GetAxisRaw("RotationHorizontal");
        float inputRotationVertical = Input.GetAxisRaw("RotationVertical");

        // 移動
        iPlayerMove.UpdateMove(inputMoveHorizontal, inputMoveVertical);
        // 通常攻撃
        iPlayerAttack.UpdateAttack(inputAttack);
        // カメラ
        playerCamera.UpdateCamera(inputMouseX, inputMouseY, inputRotationHorizontal, inputRotationVertical);
        // スキル発動、切り替え
        playerSkill.UpdateSkill(inputAttack);
    }
}
