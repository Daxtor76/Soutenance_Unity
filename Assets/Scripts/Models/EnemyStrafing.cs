using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrafing : Actor
{
    private void Start()
    {
        StateController?.ChangeState(States.idle);

        MovementController.runMover = new StrafeMover(Const.ENEMY_SIDE_SPEED);
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState == GameManager.GameStates.Playing && StateController.CurrentState == States.idle)
            StateController.ChangeState(States.run);
    }
}
