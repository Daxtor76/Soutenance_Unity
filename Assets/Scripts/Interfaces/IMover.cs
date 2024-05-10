using UnityEngine;

public interface IMover
{
    void Move(CharacterController characterController);
    void Strafe(int pDir);
    void Jump(float jumpHeight);
}
