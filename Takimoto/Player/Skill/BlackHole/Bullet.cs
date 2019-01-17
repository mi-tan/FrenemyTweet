using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Bullet : MonoBehaviour
{
    private AttackCollision attackCollision;
    [SerializeField]
    private Inhole blackHole;

    [SerializeField]
    private float bulletSpeed;


    private void Awake()
    {
        attackCollision = GetComponent<AttackCollision>();

        attackCollision.OnAttackCollision
            .Subscribe(_ => { CreateBlackHole(); })
            .AddTo(gameObject);
    }

    private void Update()
    {
        // ↓親のRigidbodyで移動に変更↓
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }

    void CreateBlackHole()
    {
        if (blackHole)
        {
            Instantiate(blackHole, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
