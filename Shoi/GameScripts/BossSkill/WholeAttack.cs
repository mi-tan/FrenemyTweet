using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;


[CreateAssetMenu(menuName = "ScriptableObject/EnemySkill/WholeAttack")]
public class WholeAttack : EnemySkillBase
{

    /// <summary>
    /// 攻撃するプレイヤーを格納
    /// </summary>
    private List<GameObject> attackPlayers = new List<GameObject>();

    /// <summary>
    /// スキル発動
    /// </summary>
    /// <param name="thisPosition"></param>
    public override void ActivateSkill(Transform thisTransform)
    {
        // スキルのエフェクトをここで出す
        // エフェクトにアニメーションつけてAttackPlayerSerch呼び出すでもいいかもしれない

        // 詠唱
        Observable.TimerFrame(getSkillChantFrame).Subscribe(_ =>
                    AttackPlayerSearch(thisTransform.position)
            ).AddTo(thisTransform.gameObject);
    }

    /// <summary>
    /// 攻撃するプレイヤーを探す
    /// </summary>
    /// <param name="thisPosition"></param>
    private void AttackPlayerSearch(Vector3 thisPosition)
    {
        attackPlayers.Clear();
        PlayerProvider[] players = gameManager.GetPlayerArray();

        // 足元からRayを出さないように+1
        Vector3 thisPos = new Vector3(thisPosition.x, thisPosition.y + 1, thisPosition.z);

        for (int index = 0; players.Length > index; index++)
        {
            RaycastHit hit;
            // 足元からRayを出さないように+1
            Vector3 playerPos = new Vector3(players[index].transform.position.x, players[index].transform.position.y + 1, players[index].transform.position.z);
            // ターゲットオブジェクトとの差分
            Vector3 temp = playerPos - thisPos;
            // 差分を正規化して方向ベクトルを求める
            Vector3 normal = temp.normalized;


            if (Physics.Raycast(thisPos, normal, out hit))
            {
                Debug.DrawRay(thisPos, normal, Color.red);

                if (hit.transform.gameObject == players[index].gameObject)
                {
                    Debug.Log("Player見つけた");
                    attackPlayers.Add(players[index].gameObject);
                }
                else
                {
                    // TargetObject以外を見つけた
                    Debug.Log(hit.transform.gameObject.name + "が間にある");
                }
            }
        }

        // 攻撃するプレイヤーがいた場合
        if (attackPlayers.Count > 0)
        {
            Attack(attackPlayers);
        }
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    /// <param name="attackPlayers"></param>
    private void Attack(List<GameObject> attackPlayers)
    {
        for (int index = 0; attackPlayers.Count > index; index++)
        {
            ExecuteEvents.Execute<IDamage>(
                target: attackPlayers[index].gameObject,
                eventData: null,
                functor: (iDamage, eventData) => iDamage.TakeDamage(getAtackPower)
            );
            Debug.Log("全体攻撃：" + attackPlayers[index].gameObject + "へ" + getAtackPower + "ダメージ");
        }
    }
}
