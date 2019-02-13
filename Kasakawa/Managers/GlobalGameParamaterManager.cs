using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameParamaterManager : MonoBehaviour {

    private float timeCount = 0;

    public float TimeCount
    {
        get
        {
            return timeCount;
        }

        set
        {
            timeCount = value;
        }
    }

    

    private List<GameObject> enemys = new List<GameObject>();

    public List<GameObject> Enemys
    {
        get
        {
            return enemys;
        }

        set
        {
            enemys = value;
        }
    }
}
