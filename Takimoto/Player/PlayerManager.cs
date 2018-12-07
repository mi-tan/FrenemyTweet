using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーを管理するクラス
/// </summary>
public class PlayerManager : MonoBehaviour {

    /// <summary>
    /// プレイヤーの状態一覧
    /// </summary>
    public enum PlayerState
    {
        /// <summary>
        /// 行動可能
        /// </summary>
        ACTABLE,
        /// <summary>
        /// 通常攻撃中
        /// </summary>
        ATTACK,
    }
    /// <summary>
    /// プレイヤーの状態
    /// </summary>
    private PlayerState playerState;
    /// <summary>
    /// プレイヤーの状態を取得
    /// </summary>
    /// <returns>プレイヤーの状態</returns>
    public PlayerState GetPlayerState()
    {
        return playerState;
    }
    public void SetPlayerState(PlayerState playerState)
    {
        this.playerState = playerState;
    }

    
}
