using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingForward : Actor
{
    private void Start()
    {
        SpecialFXController?.PopulateEnemyFXBank();

        StateController?.ChangeState(States.idle);

        MovementController.runMover = new ForwardMover(Const.ENEMY_FORWARD_SPEED);
        AnimationController?.SetActorAnimator(new EnemyAnimator());

        CollisionController?.OnCollisionWithCharacter?.AddListener(OnCharacterHit);
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState == GameManager.GameStates.Playing && StateController.CurrentState == States.idle)
            StateController.ChangeState(States.run);
    }

    public void OnCharacterHit(Actor other)
    {
        if (other.StateController.CurrentState == States.kyubi)
        {
            StateController.ChangeState(States.dead);
            GetMesh().SetActive(false);
        }
    }
}
