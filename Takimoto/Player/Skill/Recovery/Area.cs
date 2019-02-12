using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    private List<PlayerProvider> players = new List<PlayerProvider>();

    private Collider col;

    private float time = 0f;

    private float startTime = 2.2f;
    private bool start = false;

    private float endTime = 8.5f;
    private bool end = false;

    private float destroyTime = 9f;

    /// <summary>
    /// 回復間隔
    /// </summary>
    private float recoveryInterval = 0.3f;
    /// <summary>
    /// 回復量
    /// </summary>
    private int recoveryValue = 1;

    private float radius = 2.4f;


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

        if(time >= endTime)
        {
            if (!end)
            {
                end = true;
            }
        }
    }

    private IEnumerator Recovery()
    {
        yield return new WaitForSeconds(recoveryInterval);

        int layerMask = LayerMask.GetMask(new string[] { "Player" });

        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, radius, transform.up, 2f, layerMask);

        players = new List<PlayerProvider>();

        foreach (RaycastHit hit in hits)
        {
            players.Add(hit.transform.gameObject.GetComponent<PlayerProvider>());
        }

        foreach (PlayerProvider player in players)
        {
            if (player.GetHp() <= 0) { continue; }

            player.SetHp(player.GetHp() + recoveryValue);
        }

        yield return null;

        if (!end)
        {
            StartCoroutine(Recovery());
        }
    }
}
