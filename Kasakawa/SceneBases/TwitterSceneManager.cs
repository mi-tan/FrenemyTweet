using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwitterSceneManager : MonoBehaviour {

    [SerializeField]
    private string gameSceneName = "";
    [SerializeField]
    private Text playerTypeText;
    [SerializeField]
    private Text hpText;
    [SerializeField]
    private Text attackText;
    [SerializeField]
    private Text moveSpeedText;

    private PlayerType playerType;
    public PlayerType SetPlayerType
    {
        set { playerType = value; }
    }


    public void JumpScene(string sceneName)
    {
        SceneController.JumpSceneAsync(sceneName);
    }

    public void JumpGameScene()
    {
        SceneController.JumpSceneAsync(gameSceneName);
    }

    public void CalcParameter()
    {
        PlayerParameterManager.Instance.CalcStatus();

        if (playerTypeText)
        {
            // プレイヤーの種類を表示する
            // playerTypeText.text = PlayerParameterManager.Instance.PlayerHP.ToString();
            string display = "あなたは" + playerType.getType + "です";
            playerTypeText.text = display;
        }

        if (hpText)
        {
            // プレイヤーのHPを表示する
            // hpText.text = PlayerParameterManager.Instance.PlayerHP.ToString();
            hpText.text = playerType.getHP.ToString();
        }

        if (attackText)
        {
            // プレイヤーの攻撃力を表示する
            // attackText.text = PlayerParameterManager.Instance.AttackPower.ToString();
            attackText.text = playerType.getAttackValue.ToString();
        }

        if (moveSpeedText)
        {
            // プレイヤーの移動速度を表示する
            // moveSpeedText.text = PlayerParameterManager.Instance.AttackPower.ToString();
            moveSpeedText.text = playerType.getMoveSpeed.ToString();
        }
    }
}
