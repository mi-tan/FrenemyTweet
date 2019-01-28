using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCanvas : MonoBehaviour {


    /// <summary>
    /// 自分を消す
    /// </summary>
    private void Delete()
    {
        // 削除
        transform.gameObject.SetActive(false);
    }
}
