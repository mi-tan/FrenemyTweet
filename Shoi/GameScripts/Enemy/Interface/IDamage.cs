using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ダメージを受けるインターフェース
/// </summary>
public interface IDamage: IEventSystemHandler
{

    void TakeDamage(int damage);

}
