using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerMove
{
    void UpdateMove(float inputMoveHorizontal, float inputMoveVertical);

    void UpdateDodge(bool inputDodge);
}
