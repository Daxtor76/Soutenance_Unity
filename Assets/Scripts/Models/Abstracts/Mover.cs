using UnityEngine;

public abstract class Mover : IMover
{
    protected float ForwardSpeed { get; set; }
    protected float SideSpeed { get; set; }
    private Vector3 _runVelocity = new Vector3();
    private Vector3 _strafeVelocity = new Vector3();
    private Vector3 _jumpVelocity = new Vector3();

    public virtual void Move(CharacterController characterController)
    {
        // Ground movement
        _runVelocity = characterController.transform.forward * ForwardSpeed;
        
        Vector3 movementVelocity = _runVelocity + _strafeVelocity;
        characterController.Move(movementVelocity * Time.deltaTime);
        
        // Jump movement
        _jumpVelocity.y += Const.GRAVITY * Time.deltaTime;
        if (IsGrounded(characterController) && _jumpVelocity.y <= 0f)
            _jumpVelocity.y = 0f;
        characterController.Move(_jumpVelocity * Time.deltaTime);
    }

    public virtual void Strafe(CharacterController characterController, float pDir)
    {
        switch (pDir)
        {
            case > 0:
                _strafeVelocity = characterController.transform.right * SideSpeed;
                break;
            case < 0:
                _strafeVelocity = -characterController.transform.right * SideSpeed;
                break;
            default:
                _strafeVelocity = Vector3.zero;
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
        Move(actor.CharacterController);
    }

    public virtual void Exit(Actor actor)
    {
    }

    public bool IsGrounded(CharacterController characterController)
    {
        if (characterController)
        {
            return Physics.Raycast(characterController.transform.position, Vector3.down, 0.05f);
        }
        return true;
    }

    public void SetForwardSpeed(float newSpeed)
    {
        ForwardSpeed = newSpeed;
    }

    public void SetSideSpeed(float newSpeed)
    {
        SideSpeed = newSpeed;
    }
}