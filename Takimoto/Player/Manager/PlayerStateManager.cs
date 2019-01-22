using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの状態を管理するクラス
/// </summary>
public class PlayerStateManager : MonoBehaviour
{
    /// <summary>
    /// プレイヤーの状態
    /// </summary>
    public enum PlayerState
    {
        /// <summary>
        /// 行動可能
        /// </summary>
        ACTABLE,
        /// <summary>
        /// 攻撃中
        /// </summary>
        ATTACK,
        /// <summary>
        /// スキル中
        /// </summary>
        SKILL,
        /// <summary>
        /// リロード中
        /// </summary>
        RELOAD,
        /// <summary>
        /// 回避中
        /// </summary>
        DODGE,
    }
    /// <summary>
    /// 現在のプレイヤーの状態
    /// </summary>
    private PlayerState playerState;
    /// <summary>
    /// 現在のプレイヤーの状態を取得
    /// </summary>
    /// <returns>現在のプレイヤーの状態</returns>
    public PlayerState GetPlayerState()
    {
        return playerState;
    }
    /// <summary>
    /// 現在のプレイヤーの状態を変更
    /// </summary>
    /// <param name="playerState">プレイヤーの状態</param>
    /// <returns></returns>
    public void SetPlayerState(PlayerState playerState)
    {
        this.playerState = playerState;
    }
}
