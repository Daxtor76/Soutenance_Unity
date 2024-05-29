using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingForward : Actor
{
    private void Start()
    {
        StateController?.ChangeState(States.idle);

        MovementController.runMover = new ForwardMover(Const.ENEMY_FORWARD_SPEED);
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState == GameManager.GameStates.Playing && StateController.CurrentState == States.idle)
            StateController.ChangeState(States.run);
    }
}
