using UnityEngine;

public interface IMover
{
    void Move(CharacterController characterController);
    void Strafe(int pDir);
    public bool IsGrounded(CharacterController characterController);
    void Jump(float jumpHeight);
}