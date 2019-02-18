using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageCanvas : MonoBehaviour {
    private MainGameManager mainGameManager;
    private Transform playerCamera;
    [SerializeField]
    private Text damageText;
    private Rigidbody rb;

    private int damageValue = 0;
    public void SetDamageValue(int value)
    {
        damageValue = value;
    }

	// Use this for initialization
	void Start () {
        mainGameManager = GameObject.Find(
            "MainGameManager").GetComponent<MainGameManager>();
        playerCamera = mainGameManager.GetPlayer(0).GetMainCamera().transform;
        damageText.text = "" + damageValue;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * 2f, ForceMode.Impulse);
        Destroy(gameObject, 2f);
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(playerCamera);
	}
}
