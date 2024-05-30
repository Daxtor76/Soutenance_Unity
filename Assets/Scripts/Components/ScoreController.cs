using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class ScoreController : MonoBehaviour
{
    private Actor actor;
    public int TotalPatounesCount { get; private set; }
    private int _currentPatounesCount;
    private int _patouneThreshold;
    public UnityEvent<int> OnScoreChange = new UnityEvent<int>();
    public UnityEvent OnScoreThresholdReached = new UnityEvent();
    private void Awake()
    {
        actor = GetComponent<Actor>();
        _currentPatounesCount = 0;
        TotalPatounesCount = 0;
        _patouneThreshold = 1;
        actor.CollisionController?.OnCollisionWithPatoune?.AddListener(OnHitPatoune);
    }

    private void OnHitPatoune(GameObject other)
    {
        WinPoints(other.GetComponent<PatouneController>().scoreValue);
        Destroy(other);
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
            _currentPatounesCount = 0;
            OnScoreThresholdReached.Invoke();
        }
    }
}