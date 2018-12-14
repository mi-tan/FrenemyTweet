using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private IMove iMove;
    private IPlayerAttack iPlayerAttack;

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


    void Awake()
    {
        // コンポーネントを取得
        iMove = GetComponent<IMove>();
        iPlayerAttack = GetComponent<IPlayerAttack>();
    }

	void Update ()
    {
        // 入力を取得
        float inputMoveHorizontal = Input.GetAxisRaw(INPUT_MOVE_HORIZONTAL);
        float inputMoveVertical = Input.GetAxisRaw(INPUT_MOVE_VERTICAL);
        float inputAttack = Input.GetAxisRaw(INPUT_ATTACK);

        // 移動
        iMove.UpdateMove(inputMoveHorizontal, inputMoveVertical);
        // 攻撃
        iPlayerAttack.UpdateAttack(inputAttack);
    }
}
