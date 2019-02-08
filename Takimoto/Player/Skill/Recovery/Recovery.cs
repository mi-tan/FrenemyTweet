using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Skill/Recovery")]
public class Recovery : PlayerSkillBase
{
    public override void ActivateSkill(PlayerProvider playerProvider, Vector3 skillCreationPos)
    {
        //Debug.Log("範囲回復生成");
        Vector3 pos =
            playerProvider.transform.position +
            playerProvider.transform.right * skillCreationPos.x +
            playerProvider.transform.up * skillCreationPos.y +
            playerProvider.transform.forward * skillCreationPos.z;

        Instantiate(skillPrefab, pos, skillPrefab.transform.rotation);
    }
}
