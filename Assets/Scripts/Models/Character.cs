using System;
using UnityEngine;
using UnityEngine.Events;
public class Character : Actor
{
    public float kyubiTimer = 10.0f;
    public float initKyubiTime;
    private float _currentTimer;
    private GameObject _mainMenuLighting;

    public UnityEvent OnCharacterHit = new UnityEvent();

    private void Start()
    {
        SpecialFXController.PopulateCharacterFXBank();

        StateController?.ChangeState(States.idle);

        MovementController.runMover = new CharacterMover(
            Const.CHARACTER_FORWARD_SPEED,
            Const.CHARACTER_STRAFE_SPEED,
            Const.CHARACTER_ROTATION_SPEED
        );
        AnimationController?.SetActorAnimator(new CharacterAnimator());

        CollisionController?.OnCollisionWithObstacle?.AddListener(OnObstacleHit);
        CollisionController?.OnCollisionWithEnemy?.AddListener(OnEnemyHit);
        ScoreController?.OnScoreThresholdReached?.AddListener(GoKyubi);
        GameManager.Instance.OnGameStateChange?.AddListener(OnGameStateChange);

        _mainMenuLighting = transform.Find("MainMenuLighting").gameObject;
    }

    private void OnGameStateChange(GameManager.GameStates state)
    {
        if (state == GameManager.GameStates.Playing)
        {
            _mainMenuLighting.SetActive(false);
            StateController.ChangeState(States.run);
        }
    }

    private void Update()
    {
        // TODO : TO REMOVE
        if (Input.GetButtonDown(Const.RUN_AXIS_NAME) && StateController?.CurrentState == States.dead)
            StateController?.ChangeState(States.run);

        if (StateController?.CurrentState == States.kyubi)
        {
            _currentTimer = Time.time;
            if (_currentTimer - initKyubiTime > kyubiTimer)
                StateController?.ChangeState(States.run);
        }
    }

    void GoKyubi()
    {
        StateController?.ChangeState(States.kyubi);
        initKyubiTime = Time.time;
    }

    void OnEnemyHit(Actor other)
    {
        if (StateController?.CurrentState != States.kyubi)
            StateController?.ChangeState(States.dead);
    }

    void OnObstacleHit(GameObject other)
    {
        if (StateController?.CurrentState != States.kyubi)
            StateController?.ChangeState(States.dead);
    }
}