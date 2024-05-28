using UnityEngine;

public abstract class Mover : IMover
{
    protected float ForwardSpeed { get; private set; }
    protected float SideSpeed { get; private set; }
    protected float RotationSpeed { get; private set; }
    private Vector3 _runVelocity = new Vector3();
    private Vector3 _strafeVelocity = new Vector3();
    private Vector3 _jumpVelocity = new Vector3();
    private Vector3 _currentRotation = new Vector3();
    private Vector3 _targetRotation = new Vector3();

    public virtual void Enter(Actor actor)
    {
    }

    public virtual void Update(Actor actor)
    {
        CalculateMovement(actor);
    }

    public virtual void FixedUpdate(Actor actor)
    {
        ApplyMovement(actor);
    }

    public virtual void Exit(Actor actor)
    {
    }

    public virtual void CalculateMovement(Actor actor)
    {
        ApplyGravity(actor);
        CalculateGroundMovement(actor);
        CalculateRotation(actor);
    }

    public virtual void ApplyMovement(Actor actor)
    {
        ApplyJumpMovement(actor);
        ApplyGroundMovement(actor);
        ApplyRotation(actor);
    }

    private void ApplyGroundMovement(Actor actor)
    {
        actor.transform.Translate(_runVelocity * Time.fixedDeltaTime, Space.World);
    }

    private void ApplyJumpMovement(Actor actor)
    {
        actor.transform.Translate(_jumpVelocity * Time.fixedDeltaTime, Space.World);
    }

    private void ApplyGravity(Actor actor)
    {
        _jumpVelocity.y += Const.GRAVITY * Time.deltaTime;
        if (IsGrounded(actor) && _jumpVelocity.y <= 0f)
            _jumpVelocity.y = 0f;
    }

    private void CalculateGroundMovement(Actor actor)
    {
        _runVelocity = actor.transform.forward * ForwardSpeed + _strafeVelocity;
    }

    public virtual void CalculateStrafe(Actor actor, float pDir)
    {
        switch (pDir)
        {
            case > 0:
                _strafeVelocity = actor.transform.right * SideSpeed;
                break;
            case < 0:
                _strafeVelocity = -actor.transform.right * SideSpeed;
                break;
            default:
                _strafeVelocity = Vector3.zero;
                break;
        }
    }

    private void ApplyRotation(Actor actor)
    {
        actor.transform.rotation = Quaternion.AngleAxis(_currentRotation.y, actor.transform.up);
    }

    private void CalculateRotation(Actor actor)
    {
        float angle = _targetRotation.y - _currentRotation.y;
        
        if(angle > 0.5f)
            _currentRotation.y += RotationSpeed * Time.deltaTime;
        else if (angle < -0.5f)
            _currentRotation.y -= RotationSpeed * Time.deltaTime;
        else
            _currentRotation = _targetRotation;
    }

    public virtual void SetTargetRotation(int pDir = 0)
    {
        switch (pDir)
        {
            case 1:
                _targetRotation.y += 90.0f;
                break;
            case -1:
                _targetRotation.y -= 90.0f;
                break;
        }
    }

    public void CalculateJump(float jumpHeight)
    {
        _jumpVelocity.y = jumpHeight;
    }

    public bool IsGrounded(Actor actor)
    {
        Vector3 startPosition = actor.transform.position + Vector3.up;
        Physics.Raycast(startPosition, Vector3.down, out RaycastHit hit, 1.5f, LayerMask.GetMask("Ground"));

        if (!hit.transform)
            return false;
        
        float dist = hit.distance;
        return dist < 1.05f;
    }

    public void SetForwardSpeed(float newSpeed)
    {
        ForwardSpeed = newSpeed;
    }

    public void SetStrafeSpeed(float newSpeed)
    {
        SideSpeed = newSpeed;
    }

    public void SetRotationSpeed(float newSpeed)
    {
        RotationSpeed = newSpeed;
    }
}