using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Bullet : MonoBehaviour
{
    private AttackCollision attackCollision;
    [SerializeField]
    private Inhole blackHole;


    private void Awake()
    {
        attackCollision = GetComponent<AttackCollision>();

        attackCollision.OnAttackCollision
            .Subscribe(_ => { CreateBlackHole(); })
            .AddTo(gameObject);
    }

    private void Update()
    {
        transform.position += transform.forward * 30 * Time.deltaTime;
    }

    void CreateBlackHole()
    {
        Instantiate(blackHole, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
