using UnityEngine;

public class StrafeMover : Mover
{
    private int _strafeDirection = 0;
    public StrafeMover(float pSideSpeed)
    {
        SetForwardSpeed(0.0f);
        SetStrafeSpeed(pSideSpeed);
        SetRotationSpeed(0.0f);
        
        while(_strafeDirection == 0)
            _strafeDirection = Random.Range(-1, 2);
    }

    public override void UpdateMover(Actor actor)
    {
        CalculateStrafe(actor, GetStrafeDirection(actor));
        base.UpdateMover(actor);
    }

    private int GetStrafeDirection(Actor actor)
    {
        if (actor.transform.localPosition.x >= 4.0f)
            _strafeDirection = 1;
        else if (actor.transform.localPosition.x <= -4.0f)
            _strafeDirection = -1;
        return _strafeDirection;
    }
}

public class ForwardMover : Mover
{
    public ForwardMover(float pForwardSpeed)
    {
        SetForwardSpeed(pForwardSpeed);
        SetStrafeSpeed(0.0f);
        SetRotationSpeed(0.0f);
    }
}

public class ToTargetMover : Mover
{
    private Actor _target;
    public ToTargetMover(float pForwardSpeed, float pSideSpeed, Actor pTarget)
    {
        SetForwardSpeed(pForwardSpeed);
        SetStrafeSpeed(pSideSpeed);
        SetRotationSpeed(0.0f);
        _target = pTarget;
    }

    public override void CalculateMovement(Actor actor)
    {
        // Make the actor move to a target
        actor.transform.LookAt(new Vector3(_target.transform.position.x,
            actor.transform.position.y, _target.transform.position.z));
        base.CalculateMovement(actor);
    }
}

public class CharacterMover : Mover
{
    private bool _canJump = true;
    private bool _canSneak = true;
    public CharacterMover(float pForwardSpeed, float pSideSpeed, float pRotationSpeed)
    {
        SetForwardSpeed(pForwardSpeed);
        SetStrafeSpeed(pSideSpeed);
        SetRotationSpeed(pRotationSpeed);
    }

    public override void Enter(Actor actor)
    {
        actor.CollisionController.OnCollisionWithRotator.AddListener(SetTargetRotation);
    }

    public override void UpdateMover(Actor actor)
    {
        if (IsGrounded(actor))
        {
            if (_canJump)
            {
                if (Input.GetButtonDown(Const.JUMP_AXIS_NAME))
                    CalculateJump(Const.CHARACTER_JUMP_HEIGHT);
            }

            if (_canSneak)
            {
                if (Input.GetButton(Const.SNEAK_AXIS_NAME))
                    actor.StateController.ChangeState(Actor.States.sneak);
                else if (Input.GetButtonUp(Const.SNEAK_AXIS_NAME))
                    actor.StateController.ChangeState(Actor.States.run);
            }
        }
        CalculateStrafe(actor, Input.GetAxisRaw(Const.STRAFE_AXIS_NAME));
        base.UpdateMover(actor);
    }

    public override void Exit(Actor actor)
    {
        actor.CollisionController.OnCollisionWithRotator.RemoveListener(SetTargetRotation);
    }

    public override void AdaptMoverOnStateChange(Actor.States state)
    {
        _previousForwardSpeed = ForwardSpeed;
        _previousStrafeSpeed = StrafeSpeed;
        _previousRotationSpeed = RotationSpeed;

        if (state == Actor.States.sneak)
        {
            _canJump = false;
            _canSneak = true;
        }
        else if (state == Actor.States.run)
        {
            _canJump = true;
            _canSneak = true;
            
        }
        else if (state == Actor.States.kyubi)
        {
            _canJump = true;
            _canSneak = false;

            float increaseFactor = 1.2f;
            SetForwardSpeed(_previousForwardSpeed * increaseFactor);
            SetStrafeSpeed(_previousStrafeSpeed * increaseFactor);
            SetRotationSpeed(_previousRotationSpeed * increaseFactor);
        }
    }
}
