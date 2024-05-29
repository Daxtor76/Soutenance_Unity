using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemySleeping : Actor
{
    public Actor target;
    private void Start()
    {
        target = GameObject.Find("Character").GetComponent<Actor>();

        StateController.ChangeState(States.sleep);

        MovementController.runMover = new ToTargetMover(Const.ENEMY_FORWARD_SPEED, Const.ENEMY_SIDE_SPEED, target);
    }

    private void Update()
    {
        if (!IsCharacterSneaky() && IsCharacterTooClose(target.transform.position, 5.0f))
            StateController.ChangeState(States.run);
    }

    bool IsCharacterSneaky()
    {
        return target.StateController.CurrentState == States.sneak;
    }

    bool IsCharacterTooClose(Vector3 actorPos, float distance)
    {
        return Vector3.Distance(actorPos, target.transform.position) < distance;
    }
}
