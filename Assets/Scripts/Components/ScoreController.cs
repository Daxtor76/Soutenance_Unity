using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class ScoreController : MonoBehaviour
{
    private Actor actor;
    public int Points { get; private set; }
    public int scoreThreshold = 5;
    public UnityEvent<int> OnScoreChange = new UnityEvent<int>();
    public UnityEvent OnScoreThresholdReached = new UnityEvent();
    private void Awake()
    {
        actor = GetComponent<Actor>();
        Points = 0;
        actor.CollisionController?.OnCollisionWithPatoune?.AddListener(OnHitPatoune);
    }

    private void OnHitPatoune(GameObject other)
    {
        WinPoints(other.GetComponent<PatouneController>().scoreValue);
        Destroy(other);
    }

    private bool IsScoreThresholdReached()
    {
        if (Points >= scoreThreshold)
        {
            scoreThreshold *= 2;
            return true;
        }
        return false;
    }

    private void WinPoints(int value)
    {
        Points += value;
        OnScoreChange.Invoke(Points);

        if (IsScoreThresholdReached())
            OnScoreThresholdReached.Invoke();
    }
}