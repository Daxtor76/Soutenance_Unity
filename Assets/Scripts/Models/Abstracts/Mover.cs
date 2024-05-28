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

    public virtual void Move(Actor actor)
    {
        ApplyGravity(actor);
        ApplyJumpMovement(actor);
        ApplyGroundMovemement(actor);
        ApplyRotation(actor);

        /*if (actor.gameObject.name == "Character")
            Debug.Log($"movement velocity: {movementVelocity} // jumpvelocity: {_jumpVelocity} // position: {actor.transform.position}");*/
    }

    private void ApplyJumpMovement(Actor actor)
    {
        actor.transform.Translate(_jumpVelocity * Time.deltaTime, Space.World);
    }

    private void ApplyGravity(Actor actor)
    {
        _jumpVelocity.y += Const.GRAVITY * Time.deltaTime;
        if (IsGrounded(actor, out Transform hit) && _jumpVelocity.y <= 0f)
            _jumpVelocity.y = 0f;
    }

    private void ApplyGroundMovemement(Actor actor)
    {
        _runVelocity = actor.transform.forward * ForwardSpeed;
        
        Vector3 movementVelocity = _runVelocity + _strafeVelocity;
        actor.transform.Translate(movementVelocity * Time.deltaTime, Space.World);
    }

    public virtual void Strafe(Actor actor, float pDir)
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
        float angle = _targetRotation.y - _currentRotation.y;
        
        if(angle > 0.5f)
            _currentRotation.y += RotationSpeed * Time.deltaTime;
        else if (angle < -0.5f)
            _currentRotation.y -= RotationSpeed * Time.deltaTime;
        else
            _currentRotation = _targetRotation;
        
        actor.transform.rotation = Quaternion.AngleAxis(_currentRotation.y, actor.transform.up);
        //Debug.Log($"angle: {angle}, chara: {actor.transform.rotation}, current: {_currentRotation}");
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

    public void Jump(float jumpHeight)
    {
        _jumpVelocity.y = jumpHeight;
    }

    public virtual void Enter(Actor actor)
    {
    }

    public virtual void Update(Actor actor)
    {
        Move(actor);
    }

    public virtual void Exit(Actor actor)
    {
    }

    public bool IsGrounded(Actor actor, out Transform hitTransform)
    {
        Physics.Raycast(actor.transform.position, Vector3.down, out RaycastHit hit, 0.05f, LayerMask.GetMask("Ground"));
        hitTransform = hit.transform;
        //Debug.DrawRay(actor.transform.position, Vector3.down * 0.05f, hit.collider ? Color.green : Color.red, 0.1f);
        return hit.collider;
    }

    public void SetForwardSpeed(float newSpeed)
    {
        ForwardSpeed = newSpeed;
    }

    public void SetSideSpeed(float newSpeed)
    {
        SideSpeed = newSpeed;
    }

    public void SetRotationSpeed(float newSpeed)
    {
        RotationSpeed = newSpeed;
    }
}