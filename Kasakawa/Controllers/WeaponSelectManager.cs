using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectManager : MonoBehaviour {

    [SerializeField]
    private Animator[] buttonAnimatorArray;

    [SerializeField]
    private GameObject[] characterArray;

    private int currentTypeNumber = 0;

    private const string SELECT_PARAM_NAME = "IsSelect";

	// Use this for initialization
	void Awake ()
    {
        SetPlayerType((int)GameParameterManager.Instance.SpawnPlayerType);
	}

    public void SetPlayerType(int type)
    {
        GameParameterManager.Instance.SetPlayerType((GameParameterManager.PlayerType)type);
        currentTypeNumber = type;
        ReActivateUI();
        ActivateCurrentTypeUI();
    }

    public void ActivateCurrentTypeUI()
    {

        if(currentTypeNumber < buttonAnimatorArray.Length)
        {
            buttonAnimatorArray[currentTypeNumber].SetBool(SELECT_PARAM_NAME, true);
        }
        

        if (currentTypeNumber < characterArray.Length)
        {
            characterArray[currentTypeNumber].SetActive(true);
        }
        
    }

    public void ReActivateUI()
    {
        foreach(var anim in buttonAnimatorArray)
        {
            anim.SetBool(SELECT_PARAM_NAME, false);
        }

        foreach(var character in characterArray)
        {
            character.SetActive(false);
        }
    }
	
	public void JumpGameScene()
    {
        SceneController.JumpSceneAsync("MainGameScene");
    }
}
