using UnityEngine;

public class StrafeMover : Mover
{
    private int _strafeDirection = 0;
    public StrafeMover(float pSideSpeed)
    {
        ForwardSpeed = 0.0f;
        SideSpeed = pSideSpeed;
        
        velocity.z = ForwardSpeed;
        
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
        
        Strafe(_strafeDirection);
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
