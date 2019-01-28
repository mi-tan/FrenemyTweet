using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private PlayerProvider playerProvider;
    private PlayerStateManager playerStateManager;
    private PlayerAnimationManager playerAnimationManager;

    private CharacterController characterController;


    void Awake()
    {
        playerProvider = GetComponent<PlayerProvider>();
        playerStateManager = GetComponent<PlayerStateManager>();
        playerAnimationManager = GetComponent<PlayerAnimationManager>();

        characterController = GetComponent<CharacterController>();
    }

    public void Damage(int damage)
    {
        playerProvider.SetHp(playerProvider.GetHp() - damage);

        if (playerStateManager.GetPlayerState() != PlayerStateManager.PlayerState.DEATH &&
            playerProvider.GetHp() <= 0)
        {
            // プレイヤーの状態を死亡中に
            playerStateManager.SetPlayerState(PlayerStateManager.PlayerState.DEATH);
            playerAnimationManager.SetTriggerDeath();
            characterController.enabled = false;
        }

        //Debug.Log("プレイヤーに" + damage + "ダメージ");
        //Debug.Log("残りHP："+ playerParameter.Hp);
    }
}
