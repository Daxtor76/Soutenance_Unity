using UnityEngine;

public class EnemyIdleState : State
{
    public override void Enter(Actor actor)
    {
        actor.StateController.ChangeState(actor.StateController.runState);
    }
}

public class EnemyRunState : State
{
    public override void Enter(Actor actor)
    {
        actor.MovementController.ChangeMover(actor.MovementController.runMover);
    }
}