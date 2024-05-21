using UnityEngine;

public interface IMover
{
    void Move(CharacterController characterController);
    void Strafe(CharacterController characterController, float pDir);
    public bool IsGrounded(CharacterController characterController);
    void Jump(float jumpHeight);
    void Enter(Actor actor);
    void Update(Actor actor);
    void Exit(Actor actor);
}