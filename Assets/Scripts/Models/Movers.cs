using UnityEngine;

public class StrafeMover : Mover
{
    private int _strafeDirection = 0;
    public StrafeMover(float pSideSpeed)
    {
        ForwardSpeed = 0.0f;
        SideSpeed = pSideSpeed;
        
        while(_strafeDirection == 0)
            _strafeDirection = Random.Range(-1, 1);
    }

    public override void Enter(Actor actor)
    {
    }

    public override void Update(Actor actor)
    {
        base.Update(actor);

        if (actor.transform.position.x >= 4.0f)
            _strafeDirection = -1;
        else if (actor.transform.position.x <= -4.0f)
            _strafeDirection = 1;
        
        Strafe(actor.CharacterController, _strafeDirection);
    }
}

public class ForwardMover : Mover
{
    public ForwardMover(float pForwardSpeed)
    {
        ForwardSpeed = pForwardSpeed;
        SideSpeed = 0.0f;
    }
}

public class ToTargetMover : Mover
{
    private Actor _target;
    public ToTargetMover(float pForwardSpeed, float pSideSpeed, Actor pTarget)
    {
        ForwardSpeed = pForwardSpeed;
        SideSpeed = pSideSpeed;
        _target = pTarget;
    }

    public override void Move(CharacterController characterController)
    {
        // Make the controller move to a target
        characterController.transform.LookAt(new Vector3(_target.transform.position.x,
            characterController.transform.position.y, _target.transform.position.z));
        base.Move(characterController);
    }
}

public class CharacterMover : Mover
{
    public CharacterMover(float pForwardSpeed, float pSideSpeed)
    {
        ForwardSpeed = pForwardSpeed;
        SideSpeed = pSideSpeed;
    }

    public override void Update(Actor actor)
    {
        base.Update(actor);
        Strafe(actor.CharacterController, Input.GetAxisRaw(Const.STRAFE_AXIS_NAME));
        
        if (IsGrounded(actor.CharacterController))
        {
            if (Input.GetButtonDown(Const.JUMP_AXIS_NAME))
                Jump(Const.CHARACTER_JUMP_HEIGHT);
            else if (Input.GetButton(Const.SNEAK_AXIS_NAME)) // TO DO: remove it when DISCRETION feature is done
            {
                actor.StateController.ChangeState(actor.StateController.sneakyState);
            }
        }
    }
}

public class CharacterSneakyMover : Mover
{
    public CharacterSneakyMover(float pForwardSpeed, float pSideSpeed)
    {
        ForwardSpeed = pForwardSpeed;
        SideSpeed = pSideSpeed;
    }

    public override void Update(Actor actor)
    {
        base.Update(actor);
        Strafe(actor.CharacterController, Input.GetAxisRaw(Const.STRAFE_AXIS_NAME));

        if (Input.GetButtonUp(Const.SNEAK_AXIS_NAME))
            actor.StateController.ChangeState(actor.StateController.runState);
    }
}
