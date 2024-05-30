﻿using System;
using UnityEngine;
using UnityEngine.Events;
public class Character : Actor
{
    public float kyubiTimer = 10.0f;
    public float initKyubiTime;
    private float _currentTimer;

    private void Start()
    {
        StateController?.ChangeState(States.idle);

        MovementController.runMover = new CharacterMover(
            Const.CHARACTER_FORWARD_SPEED,
            Const.CHARACTER_STRAFE_SPEED,
            Const.CHARACTER_ROTATION_SPEED
        );

        CollisionController?.OnCollisionWithObstacle?.AddListener(OnObstacleHit);
        CollisionController?.OnCollisionWithEnemy?.AddListener(OnEnemyHit);
        ScoreController?.OnScoreThresholdReached?.AddListener(GoKyubi);
    }

    private void Update()
    {
        // ????
        if (GameManager.Instance.CurrentState == GameManager.GameStates.Playing && StateController.CurrentState == States.idle)
            StateController.ChangeState(States.run);

        // TODO : TO REMOVE
        if (Input.GetButtonDown(Const.RUN_AXIS_NAME) && StateController?.CurrentState == States.dead)
            StateController?.ChangeState(States.run);

        if (StateController?.CurrentState == States.kyubi)
        {
            _currentTimer = Time.time;
            //Debug.Log(_currentTimer - initKyubiTime);
            if (_currentTimer - initKyubiTime > kyubiTimer)
                StateController?.ChangeState(States.run);
        }
    }

    void GoKyubi()
    {
        StateController?.ChangeState(States.kyubi);
        initKyubiTime = Time.time;
    }

    private void OnEnemyHit(GameObject other)
    {
        if (StateController?.CurrentState != States.kyubi)
            StateController?.ChangeState(States.dead);
        else
        {
            // Increase data here for final score calculation
            Destroy(other);
        }
    }

    void OnObstacleHit(GameObject other)
    {
        if (StateController?.CurrentState != States.kyubi)
            StateController?.ChangeState(States.dead);
    }
}