using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class ScoreController : MonoBehaviour
{
    private Actor _actor;
    private GameObject _orbPrefab;
    public int TotalPatounesCount { get; private set; }
    private int _currentPatounesCount;
    private int _patouneThreshold;
    public UnityEvent<int> OnScoreChange = new UnityEvent<int>();
    public UnityEvent OnScoreThresholdReached = new UnityEvent();
    private void Awake()
    {
        _actor = GetComponent<Actor>();
        _orbPrefab = Resources.Load(Const.PATH_TO_FX_FOLDER + Const.ORB_NAME) as GameObject;
        _currentPatounesCount = 0;
        TotalPatounesCount = 0;
        _patouneThreshold = 3;
        _actor.CollisionController?.OnCollisionWithPatoune?.AddListener(OnHitPatoune);
    }

    private void OnHitPatoune(GameObject other)
    {
        CreateOrb();
        WinPoints(other.GetComponent<PatouneController>().scoreValue);
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

    private void WinPoints(int value)
    {
        _currentPatounesCount += 1;
        TotalPatounesCount += value;
        OnScoreChange.Invoke(TotalPatounesCount);

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
}