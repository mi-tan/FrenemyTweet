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

    private float destroyTime = 9f;

    /// <summary>
    /// 回復間隔
    /// </summary>
    private float recoveryInterval = 1f;
    /// <summary>
    /// 回復量
    /// </summary>
    private int recoveryValue = 10;

    private float radius = 3f;


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
            player.SetHp(player.GetHp() + recoveryValue);
        }

        yield return null;

        StartCoroutine(Recovery());
    }
}
