using UnityEngine;

public interface IMover
{
    void Move(CharacterController characterController);
    void Strafe(int pDir);
    public bool isGrounded(CharacterController characterController);
    void Jump(float jumpHeight);
}
