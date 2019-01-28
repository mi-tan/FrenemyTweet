using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "ScriptableObject/Attack/Move")]
public class AttackMoveParameter : ScriptableObject
{
    [Header("移動開始時間")]
    [SerializeField]
    private float moveStartTime;
    /// <summary>
    /// 移動時間
    /// </summary>
    public float MoveStartTime
    {
        get
        {
            return moveStartTime;
        }
    }

    [Header("移動終了時間")]
    [SerializeField]
    private float moveEndTime;
    public float MoveEndTime
    {
        get
        {
            return moveEndTime;
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
