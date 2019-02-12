using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Bullet : MonoBehaviour
{
    private AttackCollision attackCollision;
    [SerializeField]
    private GameObject hitObject;

    [SerializeField]
    private float bulletSpeed;


    private void Awake()
    {
        attackCollision = GetComponent<AttackCollision>();

        attackCollision.OnAttackCollision
            .Subscribe(_ => { CreateHitObject(); })
            .AddTo(gameObject);

        attackCollision.SetAttackPower = 0;
    }

    private void Update()
    {
        // ↓親のRigidbodyで移動に変更↓
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
    }

    void CreateHitObject()
    {
        if (hitObject)
        {
            Instantiate(hitObject, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
