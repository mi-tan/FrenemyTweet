using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateClone : MonoBehaviour
{
    //[SerializeField]
    //private GameObject[] clones;
    [SerializeField]
    private GameObject clone;

    public void Create(int weaponNum)
    {
        //Instantiate(clones[weaponNum], transform.position, transform.rotation);
        Instantiate(clone, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
