using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwitterSceneManager : MonoBehaviour {

    [SerializeField]
    private string gameSceneName = "";
    [SerializeField]
    private Text playerTypeText;
    //[SerializeField]
    //private Text hpText;
    //[SerializeField]
    //private Text attackText;
    //[SerializeField]
    //private Text moveSpeedText;
    [SerializeField]
    private Slider hpSlider;
    [SerializeField]
    private Slider attackSlider;
    [SerializeField]
    private Slider moveSpeedSlider;

    private int hpValue = 0;
    private int attackValue = 0;
    private float moveSpeedValue = 0;

    public int getHP
    {
        get
        {
            if (hpValue == 0)
            {
                hpValue=playerType.getHP;
            }
            return hpValue;
        }
    }

    public int getAttack
    {
        get
        {
            if (attackValue == 0)
            {
                attackValue = playerType.getAttackValue;
            }
            return attackValue;
        }
    }

    public float getMoveSpeed
    {
        get
        {
            if (moveSpeedValue == 0)
            {
                moveSpeedValue = playerType.getMoveSpeed;
            }
            return moveSpeedValue;
        }
    }

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

        if (playerTypeText)
        {
            // プレイヤーの種類を表示する
            // playerTypeText.text = PlayerParameterManager.Instance.PlayerHP.ToString();
            string display = "あなたは" + playerType.getType + "です";
            playerTypeText.text = display;

            
        }

        if (hpSlider)
        {
            // プレイヤーのHPを表示する
            // hpText.text = PlayerParameterManager.Instance.PlayerHP.ToString();
            //hpText.text = getHP.ToString();
            hpSlider.value = getHP;

            PlayerParameterManager.Instance.SetPlayerHP(getHP);
        }

        if (attackSlider)
        {
            // プレイヤーの攻撃力を表示する
            // attackText.text = PlayerParameterManager.Instance.AttackPower.ToString();
            //attackText.text = getAttack.ToString();
            attackSlider.value = getAttack;
            PlayerParameterManager.Instance.SetPlayerAttack(getAttack);
        }

        if (moveSpeedSlider)
        {
            // プレイヤーの移動速度を表示する
            // moveSpeedText.text = PlayerParameterManager.Instance.AttackPower.ToString();
            //moveSpeedText.text = getMoveSpeed.ToString();
            moveSpeedSlider.value = getMoveSpeed;
            PlayerParameterManager.Instance.SetPlayerMoveSpeed(getMoveSpeed);
        }
    }
}
