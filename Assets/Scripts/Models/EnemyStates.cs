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

public class EnemySleepState : State
{
    private Transform _characterTransform;
    public override void Enter(Actor actor)
    {
        _characterTransform = GameObject.Find("Character").transform;
    }

    public override void Update(Actor actor)
    {
        // TO DO: add if player not in discreat stance
        if (IsCharacterTooClose(actor.transform.position, 5.0f))
            actor.MovementController.ChangeMover(actor.MovementController.runMover);
    }

    bool IsCharacterTooClose(Vector3 actorPos, float distance)
    {
        return Vector3.Distance(actorPos, _characterTransform.position) < distance;
    }
}