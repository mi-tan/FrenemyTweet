using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移動を制御するクラス
/// </summary>
public sealed class EnemyMove : MonoBehaviour, IEnemyMove
{
    /// <summary>
    /// 入力を測定する値
    /// </summary>
    private const int INPUT_VALUE = 1;
    /// <summary>
    /// 向く速度
    /// </summary>
    private const float FACE_SPEED = 100000f;
    /// <summary>
    /// 移動角度
    /// </summary>
    private Quaternion moveQuaternion;

    private EnemyAnimationController enemyAnimationController;

    //　攻撃した後のフリーズ時間
    private float FREEZE_TIME = 0.3f;

    private void Awake()
    {
        // 初期化
        moveQuaternion = transform.rotation;
        enemyAnimationController = GetComponent<EnemyAnimationController>();
    }


    public void Move(Vector3 destination, float moveSpeed)
    {
        //Debug.Log("H：" + x + " V：" + z);
        Debug.Log("Move");

        // 移動アニメーション開始
        enemyAnimationController.Run(true);
        
        moveQuaternion = Quaternion.LookRotation(destination);

        // 位置を移動
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        // 移動角度に向く
        UpdateFace(moveQuaternion);
        //// 移動アニメーション停止
        //enemyAnimationController.Run(false);
    }

    /// <summary>
    /// 移動角度に向く
    /// </summary>
    /// <param name="moveQuaternion">移動角度</param>
    private void UpdateFace(Quaternion moveQuaternion)
    {
        // 移動角度に向いていたら、この先の処理を行わない
        if (transform.rotation == moveQuaternion) { return; }

        // 移動角度に徐々に向く
        float step = FACE_SPEED * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, moveQuaternion, step);

    }
}
