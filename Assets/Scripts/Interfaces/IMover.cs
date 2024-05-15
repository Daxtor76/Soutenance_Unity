using UnityEngine;

public interface IMover
{
    void Run(CharacterController characterController);
    void Strafe(float pDir);
    public bool IsGrounded(CharacterController characterController);
    void Jump(float jumpHeight);
}