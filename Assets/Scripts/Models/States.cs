using UnityEngine;

public abstract class State : IState
{
    public virtual void Enter(Actor actor)
    {
    }

    public virtual void Update(Actor actor)
    {
    }

    public virtual void Exit(Actor actor)
    {
    }

    public virtual void OnObstacleHit(Actor actor, GameObject other)
    {
        actor.StateController.ChangeState(actor.StateController.idleState);
    }
}

public class IdleState : State
{
    public override void Enter(Actor actor)
    {
        actor.MovementController.CurrentMover = null;
        actor.AnimationController?.Animator?.SetInteger("CharacterState",0);
    }

    public override void Update(Actor actor)
    {
        if (actor.InputHandler)
        {
            if (Input.GetKeyDown(Const.RUN_AXIS_NAME))
            {
                actor.StateController.ChangeState(actor.StateController.smoothRunState);
            }
        }
        else
            actor.StateController.ChangeState(actor.StateController.smoothRunState);
    }
}

public class RunState : State
{
    public override void Enter(Actor actor)
    {
        actor.MovementController.CurrentMover = new CharacterMover(
            Const.CHARACTER_FORWARD_SPEED,
            Const.CHARACTER_SIDE_SPEED
        );
        actor.AnimationController?.Animator?.SetInteger("CharacterState",1);
    }

    public override void Update(Actor actor)
    {
    }
}