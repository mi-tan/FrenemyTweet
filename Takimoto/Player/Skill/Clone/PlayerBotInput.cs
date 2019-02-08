using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBotInput : MonoBehaviour
{
    private enum BotState
    {
        /// <summary>
        /// 敵を探索中
        /// </summary>
        SEARCH_ENEMY,
        /// <summary>
        /// ターゲットに接近中
        /// </summary>
        APPROACH,
        /// <summary>
        /// 戦闘中
        /// </summary>
        BATTLE,
    }
    private BotState botState = BotState.SEARCH_ENEMY;

    private enum Weapon
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

    private IPlayerMove iPlayerMove;
    private IPlayerAttack iPlayerAttack;
    private PlayerCamera playerCamera;
    private PlayerSkill playerSkill;

    private float inputMoveHorizontal = 0f;
    private float inputMoveVertical = 0f;
    private float inputAttack = 0f;
    private float inputMouseX = 0f;
    private float inputMouseY = 0f;
    private float inputRotationHorizontal = 0f;
    private float inputRotationVertical = 0f;
    private float inputActivateSkill = 0f;
    private bool inputSelectSkill1 = false;
    private bool inputSelectSkill2 = false;
    private bool inputSelectSkill3 = false;
    private bool inputDodge = false;

    private GameObject target;

    private Coroutine attackComboCoroutine;
    private int comboNum = 3;

    private float[] attackRange = new float[] { 0f, 2f, 10f };

    private float disappearTime = 15f;


    void Awake()
    {
        // コンポーネントを取得
        iPlayerMove = GetComponent<IPlayerMove>();
        iPlayerAttack = GetComponent<IPlayerAttack>();
        playerCamera = GetComponent<PlayerCamera>();
        playerSkill = GetComponent<PlayerSkill>();

        SetWeapon();
    }

    void Start()
    {
        StartCoroutine(Disappear(disappearTime));
    }

    private IEnumerator Disappear(float time)
    {
        yield return new WaitForSeconds(time);

        Disappear();
    }

    void Disappear()
    {
        Destroy(gameObject);
    }

    void SetWeapon()
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
    }

    void TransitionState(BotState nextState)
    {
        //Debug.Log(botState + " → " + nextState);
        botState = nextState;

        ResetInput();
    }

    void ResetInput()
    {
        inputMoveHorizontal = 0f;
        inputMoveVertical = 0f;
        inputAttack = 0f;
        inputMouseX = 0f;
        inputMouseY = 0f;
        inputRotationHorizontal = 0f;
        inputRotationVertical = 0f;
        inputActivateSkill = 0f;
        inputSelectSkill1 = false;
        inputSelectSkill2 = false;
        inputSelectSkill3 = false;
        inputDodge = false;
    }

    void Update()
    {
        SearchEnemy();
        Approach();
        Battle();

        // 移動
        iPlayerMove.UpdateMove(
            inputMoveHorizontal,
            inputMoveVertical);

        // 通常攻撃
        iPlayerAttack.UpdateAttack(
            inputAttack,
            inputMoveHorizontal,
            inputMoveVertical);

        // カメラ
        playerCamera.UpdateCamera(
            inputMouseX,
            inputMouseY,
            inputRotationHorizontal,
            inputRotationVertical);

        // スキル発動、切り替え
        playerSkill.UpdateSkill(
            inputActivateSkill,
            inputSelectSkill1,
            inputSelectSkill2,
            inputSelectSkill3);

        // 回避
        iPlayerMove.UpdateDodge(
            inputDodge,
            inputMoveHorizontal,
            inputMoveVertical);
    }

    void SearchEnemy()
    {
        if (botState != BotState.SEARCH_ENEMY) { return; }

        //Debug.Log("敵を探索中");

        inputMoveVertical = 1f;

        if (target)
        {
            TransitionState(BotState.APPROACH);
        }

        Ray ray = new Ray(transform.position + transform.up, transform.forward);
        RaycastHit hit2;
        if (Physics.Raycast(ray, out hit2, 1f, LayerMask.GetMask("Field")))
        {
            //Debug.Log("壁に衝突");
            playerCamera.DestroyCamera();
            Disappear();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (target) { return; }

        string layerName = LayerMask.LayerToName(other.gameObject.layer);

        if (layerName == "Enemy")
        {
            target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!target) { return; }

        if (other.gameObject == target)
        {
            target = null;
            TransitionState(BotState.SEARCH_ENEMY);
        }
    }

    //private void OnDrawGizmos()
    //{
    //    // Cubeのレイを疑似的に視覚化
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position + transform.forward * searchDistance, rangeSize);
    //}

    void Approach()
    {
        if (botState != BotState.APPROACH) { return; }

        if (target != null)
        {
            //Debug.Log("ターゲットに接近中");

            inputMoveVertical = 1f;
            playerCamera.CaptureTarget(target.transform);

            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance <= attackRange[(int)weapon])
            {
                TransitionState(BotState.BATTLE);
            }
        }
        else
        {
            TransitionState(BotState.SEARCH_ENEMY);
        }
    }

    void Battle()
    {
        if (botState != BotState.BATTLE) { return; }

        if (target != null)
        {
            playerCamera.CaptureTarget(target.transform);

            Attack();
            //Debug.Log("戦闘中");
        }
        else
        {
            TransitionState(BotState.SEARCH_ENEMY);
        }
    }

    void Attack()
    {
        switch ((int)weapon)
        {
            // 剣
            case 1:
                if (attackComboCoroutine == null)
                {
                    attackComboCoroutine = StartCoroutine(SwordAttackCombo());
                }
                break;

            // 銃
            case 2:
                inputAttack = 1f;
                break;
        }
    }

    private IEnumerator SwordAttackCombo()
    {
        for (int i = 0; i < comboNum; i++)
        {
            inputAttack = 1f;
            yield return new WaitForSeconds(0.1f);
            inputAttack = 0f;
            yield return new WaitForSeconds(0.4f);
        }

        TransitionState(BotState.SEARCH_ENEMY);

        attackComboCoroutine = null;
    }
}
