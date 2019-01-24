using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 遠距離武器
/// </summary>
abstract class RangeWeapon : WeaponBase
{
    public override void UpdateAttack(float inputAttack, float inputMoveHorizontal, float inputMoveVertical)
    {
        Debug.LogError("overrideされていない(RangeWeapon)");
    }

    /// <summary>
    /// 最大弾数
    /// </summary>
    protected int maxBulletNumber = 30;
    public int GetMaxBulletNumber()
    {
        return maxBulletNumber;
    }
    /// <summary>
    /// 弾数
    /// </summary>
    protected int bulletNumber = 30;
    public int GetBulletNumber()
    {
        return bulletNumber;
    }
}
