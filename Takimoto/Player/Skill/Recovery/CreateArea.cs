using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateArea : MonoBehaviour
{
    [SerializeField]
    private GameObject recoveryArea;

    private void Start()
    {
        Instantiate(recoveryArea, transform.position, recoveryArea.transform.rotation);
        Destroy(gameObject);
    }
}
