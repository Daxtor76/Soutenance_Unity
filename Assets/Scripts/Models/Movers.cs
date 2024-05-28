using UnityEngine;

public class StrafeMover : Mover
{
    private int _strafeDirection = 0;
    public StrafeMover(float pSideSpeed)
    {
        SetForwardSpeed(0.0f);
        SetSideSpeed(pSideSpeed);
        SetRotationSpeed(0.0f);
        
        while(_strafeDirection == 0)
            _strafeDirection = Random.Range(-1, 1);
    }

    public override void Enter(Actor actor)
    {
    }

    public override void Update(Actor actor)
    {
        if (actor.transform.position.x >= 4.0f)
            _strafeDirection = 1;
        else if (actor.transform.position.x <= -4.0f)
            _strafeDirection = -1;
        
        Strafe(actor, _strafeDirection);
        base.Update(actor);
    }
}

public class ForwardMover : Mover
{
    public ForwardMover(float pForwardSpeed)
    {
        SetForwardSpeed(pForwardSpeed);
        SetSideSpeed(0.0f);
        SetRotationSpeed(0.0f);
    }
}

public class ToTargetMover : Mover
{
    private Actor _target;
    public ToTargetMover(float pForwardSpeed, float pSideSpeed, Actor pTarget)
    {
        SetForwardSpeed(pForwardSpeed);
        SetSideSpeed(pSideSpeed);
        SetRotationSpeed(0.0f);
        _target = pTarget;
    }

    public override void Move(Actor actor)
    {
        // Make the actor move to a target
        actor.transform.LookAt(new Vector3(_target.transform.position.x,
            actor.transform.position.y, _target.transform.position.z));
        base.Move(actor);
    }
}

public class CharacterMover : Mover
{
    public CharacterMover(float pForwardSpeed, float pSideSpeed, float pRotationSpeed)
    {
        SetForwardSpeed(pForwardSpeed);
        SetSideSpeed(pSideSpeed);
        SetRotationSpeed(pRotationSpeed);
    }

    public override void Enter(Actor actor)
    {
        actor.CollisionController.OnCollisionWithRotator.AddListener(SetTargetRotation);
    }

    public override void Update(Actor actor)
    {
        if (IsGrounded(actor, out Transform hit))
        {
            if (Input.GetButtonDown(Const.JUMP_AXIS_NAME))
                Jump(Const.CHARACTER_JUMP_HEIGHT);
            else if (Input.GetButton(Const.SNEAK_AXIS_NAME))
                actor.StateController.ChangeState(actor.StateController.sneakyState);
        }
        Strafe(actor, Input.GetAxisRaw(Const.STRAFE_AXIS_NAME));
        base.Update(actor);
    }

    public override void Exit(Actor actor)
    {
        actor.CollisionController.OnCollisionWithRotator.RemoveListener(SetTargetRotation);
    }
}

public class CharacterSneakyMover : Mover
{
    public CharacterSneakyMover(float pForwardSpeed, float pSideSpeed, float pRotationSpeed)
    {
        SetForwardSpeed(pForwardSpeed);
        SetSideSpeed(pSideSpeed);
        SetRotationSpeed(pRotationSpeed);
    }

    public override void Enter(Actor actor)
    {
        actor.CollisionController.OnCollisionWithRotator.AddListener(SetTargetRotation);
    }

    public override void Update(Actor actor)
    {
        Strafe(actor, Input.GetAxisRaw(Const.STRAFE_AXIS_NAME));
        base.Update(actor);

        if (Input.GetButtonUp(Const.SNEAK_AXIS_NAME))
            actor.StateController.ChangeState(actor.StateController.runState);
    }

    public override void Exit(Actor actor)
    {
        actor.CollisionController.OnCollisionWithRotator.RemoveListener(SetTargetRotation);
    }
}
