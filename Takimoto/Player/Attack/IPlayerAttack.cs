using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerAttack
{
    void UpdateAttack(float inputAttack, float inputMoveHorizontal, float inputMoveVertical);
}
