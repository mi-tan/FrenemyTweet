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

    //private enum Weapon
    //{
    //    NONE = 0,
    //    /// <summary>
    //    /// 剣
    //    /// </summary>
    //    SWORD,
    //    /// <summary>
    //    /// 銃
    //    /// </summary>
    //    RIFLE,
    //}
    //private Weapon weapon = Weapon.NONE;

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

    private List<GameObject> targets = new List<GameObject>();

    //private Coroutine attackComboCoroutine;
    //private int comboNum = 3;

    private Coroutine attackCoroutine;

    //private float[] attackRange = new float[] { 0f, 2f, 10f };
    private float attackRange = 1f;

    private float disappearTime = 15f;

    [SerializeField]
    private Camera botCamera;


    void Awake()
    {
        Camera camera = Instantiate(
            botCamera, transform.position + PlayerCamera.INITIAL_POSITION, transform.rotation);

        // コンポーネントを取得
        iPlayerMove = GetComponent<IPlayerMove>();
        iPlayerAttack = GetComponent<IPlayerAttack>();
        playerCamera = GetComponent<PlayerCamera>();
        playerSkill = GetComponent<PlayerSkill>();

        playerCamera.SetMainCamera(camera);

        //SetWeapon();
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

    //void SetWeapon()
    //{
    //    if (iPlayerAttack is Sword)
    //    {
    //        //Debug.Log("剣");
    //        weapon = Weapon.SWORD;
    //    }
    //    else if (iPlayerAttack is Rifle)
    //    {
    //        //Debug.Log("銃");
    //        weapon = Weapon.RIFLE;
    //    }
    //}

    void TransitionState(BotState nextState)
    {
        if (botState == nextState) { return; }

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
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null)
            {
                targets.RemoveAt(i);
                i--;
            }
        }

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

        if (targets.Count > 0)
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

    private void OnTriggerEnter(Collider other)
    {
        string layerName = LayerMask.LayerToName(other.gameObject.layer);

        // レイヤーがEnemyではなかったら、この先の処理を行わない
        if (layerName != "Enemy") { return; }

        bool isOverlap = false;

        if (targets.Count > 0)
        {
            foreach (GameObject target in targets)
            {
                if (target == other.gameObject)
                {
                    isOverlap = true;
                }
            }
        }

        // 既にリストに含まれていたら、この先の処理を行わない
        if (isOverlap) { return; }

        targets.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        string layerName = LayerMask.LayerToName(other.gameObject.layer);

        // レイヤーがEnemyではなかったら、この先の処理を行わない
        if (layerName != "Enemy") { return; }

        int num = -1;

        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == other.gameObject)
            {
                num = i;
            }
        }

        if (num < 0) { return; }

        targets.RemoveAt(num);
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

        if (targets.Count > 0)
        {
            float targetDistance = 100f;

            // 一番近くにいる敵をリストの先頭にする
            for (int i = 0; i < targets.Count; i++)
            {
                float td = Vector3.Distance(transform.position, targets[i].transform.position);

                if (targetDistance > td)
                {
                    targetDistance = td;

                    GameObject temp = targets[0];
                    targets[0] = targets[i];
                    targets[i] = temp;
                }
            }

            inputMoveVertical = 1f;
            playerCamera.CaptureTarget(targets[0].transform);

            if (targetDistance <= attackRange)
            {
                TransitionState(BotState.BATTLE);
            }
        }
    }

    void Battle()
    {
        if (botState != BotState.BATTLE) { return; }

        if (targets.Count > 0)
        {
            playerCamera.CaptureTarget(targets[0].transform);

            Attack();
            //Debug.Log("戦闘中");

            float targetDistance = 100f;

            // 一番近くにいる敵をリストの先頭にする
            for (int i = 0; i < targets.Count; i++)
            {
                float td = Vector3.Distance(transform.position, targets[i].transform.position);

                if (targetDistance > td)
                {
                    targetDistance = td;

                    GameObject temp = targets[0];
                    targets[0] = targets[i];
                    targets[i] = temp;
                }
            }

            if (targetDistance > 5)
            {
                TransitionState(BotState.SEARCH_ENEMY);
            }
        }
        else
        {
            TransitionState(BotState.SEARCH_ENEMY);
        }
    }

    void Attack()
    {
        //switch ((int)weapon)
        //{
        //    // 剣
        //    case 1:
        //        if (attackComboCoroutine == null)
        //        {
        //            attackComboCoroutine = StartCoroutine(SwordAttackCombo());
        //        }
        //        break;

        //    // 銃
        //    case 2:
        //        inputAttack = 1f;
        //        break;
        //}

        if (attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        inputAttack = 1f;
        yield return new WaitForSeconds(0.1f);
        inputAttack = 0f;
        yield return new WaitForSeconds(0.4f);

        TransitionState(BotState.SEARCH_ENEMY);

        attackCoroutine = null;
    }

    //private IEnumerator SwordAttackCombo()
    //{
    //    for (int i = 0; i < comboNum; i++)
    //    {
    //        inputAttack = 1f;
    //        yield return new WaitForSeconds(0.1f);
    //        inputAttack = 0f;
    //        yield return new WaitForSeconds(0.4f);
    //    }

    //    TransitionState(BotState.SEARCH_ENEMY);

    //    attackComboCoroutine = null;
    //}
}
