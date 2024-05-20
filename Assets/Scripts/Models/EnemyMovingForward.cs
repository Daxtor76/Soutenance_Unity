using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingForward : Actor
{
    private void Start()
    {
        StateController.idleState = new EnemyIdleState();
        StateController.runState = new EnemyRunState();
        
        MovementController.runMover = new ForwardMover(Const.ENEMY_FORWARD_SPEED);
        
        StateController?.ChangeState(StateController?.idleState);
    }
}
