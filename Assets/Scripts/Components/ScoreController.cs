using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using static UnityEngine.Rendering.DebugUI;

public class ScoreController : MonoBehaviour
{
    private Actor _actor;
    private GameObject _orbPrefab;
    private int _patouneThreshold;

    private RunData _runData;
    internal float lastRunSpeed;

    public UnityEvent<int> OnScoreChange = new UnityEvent<int>();
    public UnityEvent OnScoreThresholdReached = new UnityEvent();

    private void Awake()
    {
        _runData = new RunData(Time.time);
        _actor = GetComponent<Actor>();
        _orbPrefab = Resources.Load(Const.PATH_TO_FX_FOLDER + Const.ORB_NAME) as GameObject;

        _patouneThreshold = 3;

        _actor.CollisionController?.OnCollisionWithPatoune?.AddListener(OnHitPatoune);
        _actor.CollisionController?.OnCollisionWithEnemy?.AddListener(OnHitEnemy);
        _actor.StateController?.OnStateChange?.AddListener(OnStateChange);
    }

    private void OnStateChange(Actor.States state)
    {
        if (_actor.MovementController.CurrentMover != null)
            lastRunSpeed = _actor.MovementController.CurrentMover.GetForwardSpeed();

        if (state == Actor.States.dead)
        {
            DestroyOrbs();

            _runData.SetGameDuration(Time.time);
            _runData.SetDistance(_runData.GetDuration(), lastRunSpeed != 0.0f ? lastRunSpeed : _actor.MovementController.CurrentMover.GetForwardSpeed());

            GameManager.Instance.RegisterRunData(_runData);
        }
    }

    private void OnHitEnemy(Actor other)
    {
        if (_actor.StateController.CurrentState == Actor.States.kyubi)
            WinPoints(10);
    }

    private void OnHitPatoune(GameObject other)
    {
        CreateOrb();
        WinPatoune();
    }

    private GameObject CreateOrb()
    {
        Transform actorFX = _actor.transform.Find("FX");
        Transform orbsContainer = actorFX.Find("Orbs");

        Vector3 spawnPosition = new Vector3(
            orbsContainer.position.x,
            orbsContainer.position.y,
            orbsContainer.position.z + 0.4f);
        return Instantiate(_orbPrefab, spawnPosition, Quaternion.identity, orbsContainer);
    }

    private bool IsScoreThresholdReached()
    {
        return _runData.GetPatounesCount() % _patouneThreshold == 0;
    }

    private void WinPatoune()
    {
        _runData.AddPatounes(1);

        if (IsScoreThresholdReached())
        {
            Transform actorFX = _actor.transform.Find("FX");
            Transform orbsContainer = actorFX.Find("Orbs");
            DestroyOrbs();

            OnScoreThresholdReached.Invoke();
        }
    }

    private void DestroyOrbs()
    {
        Transform actorFX = _actor.transform.Find("FX");
        Transform orbsContainer = actorFX.Find("Orbs");
        foreach (Transform orb in orbsContainer)
            Destroy(orb.gameObject);
    }

    private void WinPoints(int value)
    {
        _runData.AddEnemiesKilled(1);
        _runData.SetTotalScore(value);

        OnScoreChange.Invoke(_runData.GetTotalScore());
    }
}