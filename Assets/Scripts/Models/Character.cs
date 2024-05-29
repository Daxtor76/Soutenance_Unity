using System;
using UnityEngine;
public class Character : Actor
{
    private void Start()
    {
        StateController.ChangeState(States.idle);

        MovementController.runMover = new CharacterMover(
            Const.CHARACTER_FORWARD_SPEED,
            Const.CHARACTER_STRAFE_SPEED,
            Const.CHARACTER_ROTATION_SPEED
        );

        CollisionController?.OnCollisionWithObstacle.AddListener(OnObstacleHit);
    }

    private void Update()
    {
    }

    public virtual void OnObstacleHit(GameObject other)
    {
        StateController.ChangeState(States.idle);
    }
}