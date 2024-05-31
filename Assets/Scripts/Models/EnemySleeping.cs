using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemySleeping : Actor
{
    public Actor target;
    private void Start()
    {
        SpecialFXController?.PopulateEnemyFXBank();

        target = GameObject.Find("Character").GetComponent<Actor>();

        StateController?.ChangeState(States.sleep);

        MovementController.runMover = new ToTargetMover(Const.ENEMY_FORWARD_SPEED, Const.ENEMY_SIDE_SPEED, target);

        CollisionController?.OnCollisionWithCharacter?.AddListener(OnCharacterHit);
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState == GameManager.GameStates.Playing && 
            StateController.CurrentState == States.sleep &&
            !IsCharacterSneaky() && 
            IsCharacterTooClose(target.transform.position, 5.0f))
            StateController?.ChangeState(States.run);
    }

    bool IsCharacterSneaky()
    {
        return target.StateController.CurrentState == States.sneak;
    }

    bool IsCharacterTooClose(Vector3 targetPos, float distance)
    {
        return Vector3.Distance(transform.position, targetPos) < distance;
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
