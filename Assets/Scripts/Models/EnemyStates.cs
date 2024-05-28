using System;
using UnityEngine;

public class EnemyIdleState : State
{
    public override void Update(Actor actor)
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
    private Actor _character;
    public override void Enter(Actor actor)
    {
        _character = GameObject.Find("Character").GetComponent<Character>();
    }

    public override void Update(Actor actor)
    {
        if (!IsCharacterSneaky() && IsCharacterTooClose(actor.transform.position, 5.0f))
            actor.MovementController.ChangeMover(actor.MovementController.runMover);
    }

    bool IsCharacterSneaky()
    {
        return _character.StateController.CurrentState.GetType() == typeof(CharacterSneakyState);
    }

    bool IsCharacterTooClose(Vector3 actorPos, float distance)
    {
        return Vector3.Distance(actorPos, _character.transform.position) < distance;
    }
}