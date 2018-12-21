using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "ScriptableObject/Attack/Move")]
public class AttackMoveParameter : ScriptableObject
{
    [Header("移動時間")]
    [SerializeField]
    private float moveTime;
    /// <summary>
    /// 移動時間
    /// </summary>
    public float MoveTime
    {
        get
        {
            return moveTime;
        }
    }

    [Header("移動距離")]
    [SerializeField]
    private float moveDistance;
    /// <summary>
    /// 移動距離
    /// </summary>
    public float MoveDistance
    {
        get
        {
            return moveDistance;
        }
    }

    [Header("移動速度")]
    [SerializeField]
    private float moveSpeed;
    /// <summary>
    /// 移動速度
    /// </summary>
    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }
    }
}
