using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    private List<PlayerProvider> players;

    private Collider col;

    private float time = 0f;

    private float startTime = 2.2f;
    private bool start = false;

    private float destroyTime = 6f;

    /// <summary>
    /// 回復間隔
    /// </summary>
    private float recoveryInterval = 1f;
    /// <summary>
    /// 回復量
    /// </summary>
    private int recoveryValue = 10;


    private void Awake()
    {
        col = GetComponent<Collider>();
    }

    private void Start()
    {
        col.enabled = false;
        Destroy(gameObject, destroyTime);
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time >= startTime)
        {
            if (!start)
            {
                start = true;
                StartCoroutine(Recovery());
            }
        }
    }

    private IEnumerator Recovery()
    {
        yield return new WaitForSeconds(recoveryInterval);

        foreach (PlayerProvider player in players)
        {
            player.SetHp(player.GetHp() + recoveryValue);
        }

        yield return null;

        StartCoroutine(Recovery());
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    string layerName = LayerMask.LayerToName(other.gameObject.layer);

    //    // レイヤーがEnemyではなかったら、この先の処理を行わない
    //    if (layerName != "Player") { return; }

    //    bool isOverlap = false;

    //    if (players.Count > 0)
    //    {
    //        foreach (PlayerProvider target in players)
    //        {
    //            if (target == other.gameObject)
    //            {
    //                isOverlap = true;
    //            }
    //        }
    //    }

    //    // 既にリストに含まれていたら、この先の処理を行わない
    //    if (isOverlap) { return; }

    //    Debug.Log("プレイヤーを追加");
    //    players.Add(other.gameObject.GetComponent<PlayerProvider>());
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    string layerName = LayerMask.LayerToName(other.gameObject.layer);

    //    // レイヤーがEnemyではなかったら、この先の処理を行わない
    //    if (layerName != "Player") { return; }

    //    int num = -1;

    //    for (int i = 0; i < players.Count; i++)
    //    {
    //        if (players[i] == other.gameObject)
    //        {
    //            num = i;
    //        }
    //    }

    //    if (num < 0) { return; }

    //    players.RemoveAt(num);
    //}
}
