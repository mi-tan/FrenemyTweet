using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Stage")]
public class StageData : ScriptableObject {

    [SerializeField]
    private string stageName;

    [SerializeField]
    private GameObject gameObject;
}
