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

    public virtual void OnCollisionWithTriggerHappened(Actor actor, GameObject other)
    {
        if (other.CompareTag(Const.OBSTACLE_TAG_NAME))
        {
            actor.StateController.ChangeState(actor.idleState);
        }
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
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W))
            {
                actor.StateController.ChangeState(actor.smoothRunState);
            }
        }
        else
            actor.StateController.ChangeState(actor.smoothRunState);
    }
}

public class SmoothRunState : State
{
    public override void Enter(Actor actor)
    {
        actor.MovementController.CurrentMover = actor.groundMover;
        actor.AnimationController?.Animator?.SetInteger("CharacterState",1);
    }

    public override void Update(Actor actor)
    {
        if (actor.InputHandler)
        {
            if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A))
            {
                actor.MovementController.CurrentMover.Strafe(-1);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                actor.MovementController.CurrentMover.Strafe(1);
            }
            
            if (actor.MovementController.CurrentMover.IsGrounded(actor.CharacterController))
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    actor.AnimationController.Animator.SetTrigger("Jump");
                    actor.MovementController.CurrentMover.Jump(Const.CHARACTER_JUMP_HEIGHT);
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    actor.StateController.ChangeState(actor.idleState);
                }
            }
        }
    }
}