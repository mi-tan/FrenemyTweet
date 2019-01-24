using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "ScriptableObject/Data/PlayerType")]
public class PlayerType : ScriptableObject
{
    [SerializeField]
    private string typeName;

    /// <summary>
    /// 最大値
    /// </summary>
    private const int maxValue = 90;
    /// <summary>
    /// 最小値
    /// </summary>
    private const int minValue = 11;
    /// <summary>
    /// 差分値
    /// </summary>
    private const int differenceValue = 10;

    [SerializeField,Range(minValue, maxValue)]
    private int hp;
    [SerializeField,Range(minValue, maxValue)]
    private int attackValue;
    [SerializeField,Range(minValue, maxValue)]
    private int moveSpeed;


    public string getType
    {
        get { return typeName; }
    }

    public int getHP
    {
        get { return Random.Range(hp - differenceValue, hp + differenceValue); }
    }

    public int getAttackValue
    {
        get { return Random.Range(attackValue - differenceValue, attackValue + differenceValue); }
    }

    public int getMoveSpeed
    {
        get { return Random.Range(moveSpeed - differenceValue, moveSpeed + differenceValue); }
    }
}
