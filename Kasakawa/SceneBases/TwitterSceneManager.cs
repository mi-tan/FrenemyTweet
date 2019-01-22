using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwitterSceneManager : MonoBehaviour {

    [SerializeField]
    private string gameSceneName = "";

    [SerializeField]
    private Text hpText;

    [SerializeField]
    private Text attackText;

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

        if (hpText)
        {
            // プレイヤーのHPを表示する
            hpText.text = PlayerParameterManager.Instance.PlayerHP.ToString();
        }


        if (attackText)
        {
            // プレイヤーの攻撃力を表示する
            attackText.text = PlayerParameterManager.Instance.AttackPower.ToString();
        }
        

    }

}
