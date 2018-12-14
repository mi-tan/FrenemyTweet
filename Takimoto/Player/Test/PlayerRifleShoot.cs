using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRifleShoot : MonoBehaviour
{
    private Animator playerAnimator;

    /// <summary>
    /// 通常攻撃入力
    /// </summary>
    const string INPUT_ATTACK = "Attack";

    /// <summary>
    /// 通常攻撃パラメータ
    /// </summary>
    const string PARAMETER_RIFLE_SHOOT = "Shoot";


    void Awake()
    {
        // コンポーネントを取得
        playerAnimator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw(INPUT_ATTACK) >= 1)
        {
            // 通常攻撃アニメーションを再生
            playerAnimator.SetBool(PARAMETER_RIFLE_SHOOT, true);
        }
        else
        {
            playerAnimator.SetBool(PARAMETER_RIFLE_SHOOT, false);
        }
    }
}
