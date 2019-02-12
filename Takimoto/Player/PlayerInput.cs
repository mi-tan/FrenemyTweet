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
    /// <summary>
    /// スキル発動入力
    /// </summary>
    const string INPUT_ACTIVATE_SKILL = "ActivateSkill";
    /// <summary>
    /// スキル1切り替え入力
    /// </summary>
    const string INPUT_SELECT_SKILL_1 = "SelectSkill1";
    /// <summary>
    /// スキル2切り替え入力
    /// </summary>
    const string INPUT_SELECT_SKILL_2 = "SelectSkill2";
    /// <summary>
    /// スキル3切り替え入力
    /// </summary>
    const string INPUT_SELECT_SKILL_3 = "SelectSkill3";
    /// <summary>
    /// 回避入力
    /// </summary>
    const string INPUT_DODGE = "Dodge";


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
        float inputMouseX = Input.GetAxis(INPUT_MOUSE_X);
        float inputMouseY = Input.GetAxis(INPUT_MOUSE_Y);
        float inputRotationHorizontal = Input.GetAxisRaw(INPUT_ROTATION_HORIZONTAL);
        float inputRotationVertical = Input.GetAxisRaw(INPUT_ROTATION_VERTICAL);
        float inputActivateSkill = Input.GetAxisRaw(INPUT_ACTIVATE_SKILL);
        bool inputSelectSkill1 = Input.GetButtonDown(INPUT_SELECT_SKILL_1);
        bool inputSelectSkill2 = Input.GetButtonDown(INPUT_SELECT_SKILL_2);
        bool inputSelectSkill3 = Input.GetButtonDown(INPUT_SELECT_SKILL_3);
        bool inputDodge = Input.GetButtonDown(INPUT_DODGE);

        // カメラ初期化
        playerCamera.InitCamera();

        // 移動
        iPlayerMove.UpdateMove(
            inputMoveHorizontal, 
            inputMoveVertical);

        // 通常攻撃
        iPlayerAttack.UpdateAttack(
            inputAttack,
            inputMoveHorizontal,
            inputMoveVertical);

        // スキル発動、切り替え
        playerSkill.UpdateSkill(
            inputActivateSkill,
            inputSelectSkill1,
            inputSelectSkill2,
            inputSelectSkill3);

        // 回避
        iPlayerMove.UpdateDodge(
            inputDodge,
            inputMoveHorizontal,
            inputMoveVertical);

        // カメラ
        playerCamera.UpdateCamera(
            inputMouseX,
            inputMouseY,
            inputRotationHorizontal,
            inputRotationVertical);
    }
}
