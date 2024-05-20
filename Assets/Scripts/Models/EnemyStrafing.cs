using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrafing : Actor
{
    private void Start()
    {
        StateController.idleState = new EnemyIdleState();
        StateController.runState = new EnemyRunState();
        
        MovementController.runMover = new StrafeMover(Const.ENEMY_SIDE_SPEED);
        
        StateController?.ChangeState(StateController.idleState);
    }
}
