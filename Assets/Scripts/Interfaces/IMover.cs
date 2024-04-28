using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMover
{
    void Move(Character character);
    void Strafe(Character character, int direction);
    void Jump(Character character);
    bool IsGrounded(Character character);
}
