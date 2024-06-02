using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using static UnityEngine.Rendering.DebugUI;

public class ScoreController : MonoBehaviour
{
    private Actor _actor;
    private GameObject _orbPrefab;
    private int _currentPatounesCount;
    private int _patouneThreshold;
    public int TotalPatounesValue { get; private set; }
    public UnityEvent<int> OnScoreChange = new UnityEvent<int>();
    public UnityEvent OnScoreThresholdReached = new UnityEvent();
    private void Awake()
    {
        _actor = GetComponent<Actor>();
        _orbPrefab = Resources.Load(Const.PATH_TO_FX_FOLDER + Const.ORB_NAME) as GameObject;
        _currentPatounesCount = 0;
        _patouneThreshold = 3;
        TotalPatounesValue = 0;
        _actor.CollisionController?.OnCollisionWithPatoune?.AddListener(OnHitPatoune);
        _actor.CollisionController?.OnCollisionWithEnemy?.AddListener(OnHitEnemy);
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
        return _currentPatounesCount >= _patouneThreshold;
    }

    private void WinPatoune()
    {
        _currentPatounesCount += 1;

        if (IsScoreThresholdReached())
        {
            Transform actorFX = _actor.transform.Find("FX");
            Transform orbsContainer = actorFX.Find("Orbs");

            foreach (Transform orb in orbsContainer)
                Destroy(orb.gameObject);

            _currentPatounesCount = 0;
            OnScoreThresholdReached.Invoke();
        }
    }

    private void WinPoints(int value)
    {
        TotalPatounesValue += value;
        OnScoreChange.Invoke(TotalPatounesValue);
    }
}