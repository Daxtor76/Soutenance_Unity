using UnityEngine;

public abstract class State : IState
{
    public virtual void Enter(Actor actor)
    {
    }

    public virtual void UpdateState(Actor actor)
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