using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySleeping : Actor
{
    public Actor target;
    //public float fuck = 5.0f;
    private void Start()
    {
        StateController.idleState = new EnemyIdleState();
        StateController.runState = new EnemyRunState();
        StateController.sleepState = new EnemySleepState();
        
        MovementController.runMover = new ToTargetMover(Const.ENEMY_FORWARD_SPEED, Const.ENEMY_SIDE_SPEED);
        
        StateController?.ChangeState(StateController.sleepState);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(transform.position, fuck);
    }
}
