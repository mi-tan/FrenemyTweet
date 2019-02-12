using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
public class PlayerProvider : CharacterBase
{
    private PlayerDamage playerDamage;
    private PlayerParameter playerParameter;
    private PlayerSkill playerSkill;
    private PlayerCamera playerCamera;
    private WeaponBase weaponBase;
    [Inject]
    private MainGameManager mainGameManager;

    [SerializeField]
    private SkinnedMeshRenderer faceMat;

    private IPlayerAttack iPlayerAttack;

    public enum Weapon
    {
        NONE = 0,
        /// <summary>
        /// 剣
        /// </summary>
        SWORD,
        /// <summary>
        /// 銃
        /// </summary>
        RIFLE,
    }
    private Weapon weapon = Weapon.NONE;


    void Awake()
    {
        // コンポーネントを取得
        playerDamage = GetComponent<PlayerDamage>();
        playerParameter = GetComponent<PlayerParameter>();
        playerSkill = GetComponent<PlayerSkill>();
        playerCamera = GetComponent<PlayerCamera>();
        weaponBase = GetComponent<WeaponBase>();

        iPlayerAttack = GetComponent<IPlayerAttack>();
    }

    private void Start()
    {
        if (iPlayerAttack is Sword)
        {
            //Debug.Log("剣");
            weapon = Weapon.SWORD;
        }
        else if (iPlayerAttack is Rifle)
        {
            //Debug.Log("銃");
            weapon = Weapon.RIFLE;
        }

        SetFaceTexture(TwitterParameterManager.Instance.IconTexture);
    }

    public Weapon GetWeapon()
    {
        return weapon;
    }

    public override void TakeDamage(int damage)
    {
        playerDamage.Damage(damage);
    }

    public int GetHp()
    {
        return playerParameter.Hp;
    }

    public void SetHp(int value)
    {
        playerParameter.SetHp(value);
    }

    public int GetMaxHp()
    {
        return playerParameter.MaxHp;
    }

    public void SetMaxHp(int value)
    {
        playerParameter.SetMaxHp(value);
    }

    public int GetBasicAttackPower()
    {
        return playerParameter.BasicAttackPower;
    }

    public void SetBasicAttackPower(int value)
    {
        playerParameter.SetBasicAttackPower(value);
    }

    public int GetPlayerAttackPower()
    {
        return playerParameter.PlayerAttackPower;
    }

    public void SetPlayerAttackPower(int value)
    {
        playerParameter.SetPlayerAttackPower(value);
    }

    public int GetSkillNumber()
    {
        return playerSkill.SkillNumber;
    }

    public PlayerSkillBase[] GetSkillList()
    {
        return playerSkill.GetSkillList();
    }

    public PlayerSkillBase GetSelectSkill()
    {
        return playerSkill.GetSelectSkill();
    }

    public float[] GetSkillCoolTimes()
    {
        return playerSkill.GetSkillCoolTimes();
    }

    public float[] GetSkillBaseCoolTimes()
    {
        float[] skillBaseCoolTimes = new float[PlayerParameter.SKILL_QUANTITY];

        for(int i = 0; i < skillBaseCoolTimes.Length; i++)
        {
            if (GetSkillList()[i] == null) { continue; }

            skillBaseCoolTimes[i] = GetSkillList()[i].SkillCoolTime;
        }

        return skillBaseCoolTimes;
    }

    public void SetFaceTexture(Texture2D tex)
    {
        if (!tex) { return; }
        faceMat.material.EnableKeyword("_MainTex");
        faceMat.material.SetTexture("_MainTex", tex);
    }

    public PlayerCamera GetPlayerCamera()
    {
        return playerCamera;
    }

    public void SetMainCamera(Camera camera)
    {
        playerCamera.SetMainCamera(camera);
    }

    public Camera GetMainCamera()
    {
        return playerCamera.GetMainCamera();
    }

    public WeaponBase GetWeaponBase()
    {
        return weaponBase;
    }

    public Sprite GetWeaponIcon()
    {
        return weaponBase.GetWeaponIcon();
    }

    public float GetMoveSpeed()
    {
        return playerParameter.MoveSpeed;
    }

    public void SetMoveSpeed(float value)
    {
        playerParameter.SetMoveSpeed(value);
    }

    public void OnDeathPlayer()
    {
        mainGameManager.OnDeathPlayer(this);
    }

    public void SetSkyBox(Material skyBoxMat)
    {
        playerCamera.SetSkyBox(skyBoxMat);
    }
}
