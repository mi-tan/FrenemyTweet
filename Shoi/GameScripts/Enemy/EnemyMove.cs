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
    private const float FACE_SPEED = 0.3f;

    private EnemyAnimationController enemyAnimationController;
    private Rigidbody rigidbody;

    //　攻撃した後のフリーズ時間
    //private float FREEZE_TIME = 0.3f;

    private void Awake()
    {
        enemyAnimationController = GetComponent<EnemyAnimationController>();
        rigidbody = GetComponent<Rigidbody>();
    }


    public void Move(Vector3 destination, float moveSpeed)
    {
        // 移動アニメーション開始
        enemyAnimationController.Run(true);

        Vector3 lookVector = destination - transform.position;
        lookVector.y = 0;

        //moveQuaternion = Quaternion.LookRotation(new Vector3(destination.x, transform.position.y, destination.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookVector), FACE_SPEED);
      
        // 位置を移動
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    //public void MoveStop()
    //{
    //    rigidbody.velocity = Vector3.zero;
    //    rigidbody.angularVelocity = Vector3.zero;
    //}
}