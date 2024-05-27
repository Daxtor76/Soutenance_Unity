using UnityEngine;

public interface IMover
{
    void Move(Actor actor);
    void Strafe(Actor actor, float pDir);
    void SetTargetRotation(int pDir);
    public bool IsGrounded(Actor actor, out Transform hit);
    void Jump(float jumpHeight);
    void Enter(Actor actor);
    void Update(Actor actor);
    void Exit(Actor actor);
}