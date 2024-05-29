using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrafing : Actor
{
    private void Start()
    {
        StateController.ChangeState(States.run);

        MovementController.runMover = new StrafeMover(Const.ENEMY_SIDE_SPEED);
        MovementController.ChangeMover(MovementController.runMover);
    }
}
