using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingForward : Actor
{
    public EnemyMovingForward()
    {
        MovementController.CurrentMover = new ForwardMover(Const.ENEMY_FORWARD_SPEED);
        StateController.ChangeState(new RunState());
    }
}
