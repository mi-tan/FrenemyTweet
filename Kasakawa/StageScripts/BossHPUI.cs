using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class BossHPUI : MonoBehaviour {

    [SerializeField]
    private Slider hpSlider;

    private BossParameter bossParameter;

    private void Awake()
    {
        bossParameter = GetComponent<BossParameter>();
    }

    // Use this for initialization
    void Start ()
    {
        if (!hpSlider) { return; }

        hpSlider.maxValue = bossParameter.maxHP;

        hpSlider.value = bossParameter.hp;

        bossParameter.ObserveEveryValueChanged(_ => bossParameter.hp)
            .Subscribe(hp => { UpdateHPValue(hp); })
            .AddTo(gameObject);
    }

    private void UpdateHPValue(int hp)
    {
        hpSlider.value = hp;
    }
}
