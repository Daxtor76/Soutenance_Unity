using UnityEngine;

public class CharacterIdleState : State
{
    public override void Enter(Actor actor)
    {
        actor.MovementController.ChangeMover(null);
    }

    public override void Update(Actor actor)
    {
        if (Input.GetButtonDown(Const.RUN_AXIS_NAME))
        {
            actor.StateController.ChangeState(actor.StateController.runState);
        }
    }

    public override void Exit(Actor actor)
    {
        actor.MovementController.ChangeMover(actor.MovementController.runMover);
    }
}

public class CharacterRunState : State
{
    public override void Enter(Actor actor)
    { 
        actor.MovementController.CurrentMover?.SetForwardSpeed(Const.CHARACTER_FORWARD_SPEED);
        actor.MovementController.CurrentMover?.SetStrafeSpeed(Const.CHARACTER_STRAFE_SPEED);
        
        actor.AnimationController?.Animator?.SetInteger("CharacterState",1);
    }
}

public class CharacterSneakyState : State
{
    public override void Enter(Actor actor)
    {
        actor.MovementController.CurrentMover?.SetForwardSpeed(Const.CHARACTER_SNEAKY_FORWARD_SPEED);
        actor.MovementController.CurrentMover?.SetStrafeSpeed(Const.CHARACTER_SNEAKY_STRAFE_SPEED);
        
        actor.AnimationController?.Animator?.SetInteger("CharacterState",1);
    }
}