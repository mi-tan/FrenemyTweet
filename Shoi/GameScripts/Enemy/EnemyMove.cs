using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 移動を制御するクラス
/// </summary>
public class EnemyMove : MonoBehaviour, IEnemyMove
{

    /// <summary>
    /// 向く速度
    /// </summary>
    const float FACE_SPEED = 1200f;
    /// <summary>
    /// 移動角度
    /// </summary>
    Quaternion moveQuaternion;

    private void Awake()
    {
        // 初期化
        moveQuaternion = transform.rotation;
    }

    public void Move(float x, float z)
    {
        float moveHorizontal = x;
        float moveVertical = z;


        // 移動角度を計算
        Vector3 moveDirection = new Vector3(moveHorizontal, 0, moveVertical);
        moveQuaternion = Quaternion.LookRotation(moveDirection);
        // 移動角度に向く
        UpdateFace(moveQuaternion);
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
