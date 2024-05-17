using UnityEngine;

public class StrafeMover : Mover
{
    public StrafeMover(float pSideSpeed)
    {
        ForwardSpeed = 0.0f;
        SideSpeed = pSideSpeed;
        
        velocity.z = ForwardSpeed;
    }
    public override void Run(CharacterController characterController)
    {
        // TO DO: Make this mover move the GO from left to right looping
    }
}

public class ForwardMover : Mover
{
    public ForwardMover(float pForwardSpeed)
    {
        ForwardSpeed = pForwardSpeed;
        SideSpeed = 0.0f;
        
        velocity.z = ForwardSpeed;
    }

    public override void Strafe(float pDir)
    {
        // I don't strafe as I just move forward
    }
}

public class CharacterMover : Mover
{
    public CharacterMover(float pForwardSpeed, float pSideSpeed)
    {
        ForwardSpeed = pForwardSpeed;
        SideSpeed = pSideSpeed;
        
        velocity.z = ForwardSpeed;
    }

    public override void Update(Actor actor)
    {
        base.Update(actor);
        Strafe(Input.GetAxisRaw(Const.STRAFE_AXIS_NAME));
        
        if (IsGrounded(actor.CharacterController))
        {
            if (Input.GetButtonDown(Const.JUMP_AXIS_NAME))
                Jump(Const.CHARACTER_JUMP_HEIGHT);
            else if (Input.GetKeyDown(KeyCode.S)) // TO DO: remove it when DISCRETION feature is done
                actor.StateController.ChangeState(actor.StateController.idleState);
        }
    }
}
