using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private PlayerProvider playerProvider;
    private PlayerStateManager playerStateManager;
    private PlayerAnimationManager playerAnimationManager;
    private PlayerCamera playerCamera;

    //private CharacterController characterController;

    private float shakeTime = 0.1f;
    private float shakeX = 0.1f;
    private float shakeY = 0.1f;



    void Awake()
    {
        playerProvider = GetComponent<PlayerProvider>();
        playerStateManager = GetComponent<PlayerStateManager>();
        playerAnimationManager = GetComponent<PlayerAnimationManager>();
        playerCamera = GetComponent<PlayerCamera>();

        //characterController = GetComponent<CharacterController>();
    }

    public void Damage(int damage)
    {
        // カメラを揺らす
        playerCamera.ShakeCamera(shakeTime, shakeX, shakeY);

        playerProvider.SetHp(playerProvider.GetHp() - damage);

        if (playerStateManager.GetPlayerState() != PlayerStateManager.PlayerState.DEATH &&
            playerProvider.GetHp() <= 0)
        {
            // プレイヤーの状態を死亡中に
            playerStateManager.SetPlayerState(PlayerStateManager.PlayerState.DEATH);
            playerAnimationManager.SetTriggerDeath();
            playerProvider.OnDeathPlayer();
            //characterController.enabled = false;
        }

        //Debug.Log("プレイヤーに" + damage + "ダメージ");
        //Debug.Log("残りHP："+ playerParameter.Hp);
    }
}
