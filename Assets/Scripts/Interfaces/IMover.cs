using UnityEngine;

public interface IMover
{
    void CalculateMovement(Actor actor);
    void ApplyMovement(Actor actor);
    void CalculateStrafe(Actor actor, float pDir);
    void SetTargetRotation(int pDir);
    public bool IsGrounded(Actor actor, out Transform hit);
    void CalculateJump(float jumpHeight);
    void Enter(Actor actor);
    void Update(Actor actor);
    void FixedUpdate(Actor actor);
    void Exit(Actor actor);
}