using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private AttackCollision attackCollision;

    [SerializeField]
    private int attackPower = 50;
    [SerializeField]
    private GameObject hitEffect;


    private void Awake()
    {
        attackCollision = GetComponent<AttackCollision>();

        attackCollision.SetAttackPower = attackPower;
        attackCollision.SetHitEffect = hitEffect;
    }

    private void Start()
    {
        Destroy(gameObject, 1f);
    }
}
