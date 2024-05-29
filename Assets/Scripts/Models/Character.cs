using System;
using UnityEngine;
public class Character : Actor
{
    private void Start()
    {
        StateController?.ChangeState(States.idle);

        MovementController.runMover = new CharacterMover(
            Const.CHARACTER_FORWARD_SPEED,
            Const.CHARACTER_STRAFE_SPEED,
            Const.CHARACTER_ROTATION_SPEED
        );

        CollisionController?.OnCollisionWithObstacle.AddListener(OnObstacleHit);
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState == GameManager.GameStates.Playing && StateController.CurrentState == States.idle)
            StateController.ChangeState(States.run);

        if (Input.GetKeyDown(KeyCode.K) && StateController.CurrentState != States.kyubi)
            StateController.ChangeState(States.kyubi);

        if (Input.GetButtonDown(Const.RUN_AXIS_NAME) && StateController.CurrentState == States.dead)
            StateController.ChangeState(States.run);
    }

    public virtual void OnObstacleHit(GameObject other)
    {
        StateController.ChangeState(States.dead);
    }
}