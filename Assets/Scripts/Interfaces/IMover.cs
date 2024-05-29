using UnityEngine;

public interface IMover
{
    void CalculateMovement(Actor actor);
    void ApplyMovement(Actor actor);
    void CalculateStrafe(Actor actor, float pDir);
    void SetTargetRotation(int pDir);
    void SetForwardSpeed(float newSpeed);
    void SetStrafeSpeed(float newSpeed);
    void SetRotationSpeed(float newSpeed);
    float GetForwardSpeed();
    float GetStrafeSpeed();
    float GetRotationSpeed();
    public bool IsGrounded(Actor actor);
    void CalculateJump(float jumpHeight);
    void Enter(Actor actor);
    void UpdateMover(Actor actor);
    void FixedUpdateMover(Actor actor);
    void Exit(Actor actor);
    void AdaptMoverOnStateChange(Actor.States state);
}