using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingForward : Actor
{
    private void Start()
    {
        StateController.ChangeState(States.run);

        MovementController.runMover = new ForwardMover(Const.ENEMY_FORWARD_SPEED);
        MovementController.ChangeMover(MovementController.runMover);
    }
}
