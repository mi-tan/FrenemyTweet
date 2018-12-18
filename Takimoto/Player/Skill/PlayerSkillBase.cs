using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class PlayerSkillBase : ScriptableObject
{
    [SerializeField]
    private string skillName;
    [SerializeField]
    private Sprite skillIcon;

    public string SkillName
    {
        get
        {
            return skillName;
        }
    }

    public Sprite SkillIcon
    {
        get
        {
            return skillIcon;
        }
    }

    public abstract void ActivateSkill();
}
