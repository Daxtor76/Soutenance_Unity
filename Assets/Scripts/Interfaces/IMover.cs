using UnityEngine;

public interface IMover
{
    void Move(GameObject go);
    void Strafe(int pDir);
    void Jump(float jumpHeight);
    bool IsGrounded(GameObject go);
}
