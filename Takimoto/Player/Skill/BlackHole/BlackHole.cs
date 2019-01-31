using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Skill/BlackHole")]
public class BlackHole : PlayerSkillBase
{
    [Header("消去時間")]
    [SerializeField]
    private float destroyTime = 2.4f;


    public override void ActivateSkill(Transform playerTrans, Vector3 skillCreationPos, Camera mainCamera, PlayerCamera playerCamera)
    {
        //Debug.Log("ブラックホール生成");
        Vector3 pos =
            playerTrans.position +
            playerTrans.right * skillCreationPos.x +
            playerTrans.up * skillCreationPos.y +
            playerTrans.forward * skillCreationPos.z;

        Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2);
        Ray ray = mainCamera.ScreenPointToRay(center);
        RaycastHit hit;

        Quaternion qua = new Quaternion();

        if (Physics.Raycast(ray, out hit, 1000.0f, LayerMask.GetMask(new string[] { "Field" })))
        {
            Vector3 vec = (hit.point - pos).normalized;
            qua = Quaternion.LookRotation(vec);
        }
        else
        {
            Debug.LogWarning("Rayで照準位置が取得できていない");
        }

        AttackCollision attackCollision = Instantiate(skillPrefab, pos, qua);
        Destroy(attackCollision.gameObject, destroyTime);
    }
}
