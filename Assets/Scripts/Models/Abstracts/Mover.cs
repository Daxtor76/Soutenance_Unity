using UnityEngine;

public abstract class Mover : IMover
{
    protected float ForwardSpeed { get; set; }
    protected float SideSpeed { get; set; }
    protected float RotationSpeed { get; set; }
    private Vector3 _runVelocity = new Vector3();
    private Vector3 _strafeVelocity = new Vector3();
    private Vector3 _jumpVelocity = new Vector3();
    private Quaternion _targetRotation = new Quaternion();

    public virtual void Move(Actor actor)
    {
        _jumpVelocity.y += Const.GRAVITY * Time.deltaTime;
        // Jump movement
        if (IsGrounded(actor, out Transform hit) && _jumpVelocity.y <= 0f)
            _jumpVelocity.y = 0f;
        
        actor.transform.Translate(_jumpVelocity * Time.deltaTime, Space.World);
        
        // Ground movement
        _runVelocity = actor.transform.forward * ForwardSpeed;
        
        Vector3 movementVelocity = _runVelocity + _strafeVelocity;
        actor.transform.Translate(movementVelocity * Time.deltaTime, Space.World);
        
        /*if (actor.gameObject.name == "Character")
            Debug.Log($"movement velocity: {movementVelocity} // jumpvelocity: {_jumpVelocity} // position: {actor.transform.position}");*/
        
        // Rotation
        ApplyRotation(actor);
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
        actor.transform.rotation = Quaternion.RotateTowards(actor.transform.rotation, _targetRotation, RotationSpeed * Time.deltaTime);
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
        Debug.DrawRay(actor.transform.position, Vector3.down * 0.05f, hit.collider ? Color.green : Color.red, 0.1f);
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