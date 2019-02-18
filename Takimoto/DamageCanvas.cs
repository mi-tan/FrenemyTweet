using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageCanvas : MonoBehaviour
{
    private MainGameManager mainGameManager;
    private Transform playerCamera;
    [SerializeField]
    private Text damageText;

    private int damageValue = 0;
    public void SetDamageValue(int value)
    {
        damageValue = value;
    }

    private float time = 0f;
    private float alphaTime = 0.1f;

    private Color color = Color.red;
    private float alpha = 0.8f;
    private float alphaSpeed = 2f;

    Vector3 baseScale = Vector3.zero;


    // Use this for initialization
    void Start()
    {
        mainGameManager = GameObject.Find(
            "MainGameManager").GetComponent<MainGameManager>();
        playerCamera = mainGameManager.GetPlayer(0).GetMainCamera().transform;
        damageText.text = "" + damageValue;
        Destroy(gameObject, 2f);

        color = damageText.color;

        baseScale = damageText.rectTransform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerCamera);

        //damageText.transform.localScale = baseScale * GetDistance();

        time += Time.deltaTime;
        if (time >= alphaTime)
        {
            transform.position += Vector3.up * Time.deltaTime;

            alpha -= Time.deltaTime * alphaSpeed;
            damageText.color = new Color(color.r, color.g, color.b, alpha);

            damageText.rectTransform.localScale = baseScale * GetDistance();
        }
        else
        {
            damageText.rectTransform.localScale = baseScale * GetDistance() * 1.5f;
        }
    }

    private float GetDistance()
    {
        return (damageText.rectTransform.position - playerCamera.position).magnitude;
    }
}
