using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove
{
    void UpdateMove(float inputMoveHorizontal, float inputMoveVertical);
}
