using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class ScoreController : MonoBehaviour
{
    private Actor actor;
    public int Points { get; private set; }
    public UnityEvent<int> ScoreChanged = new UnityEvent<int>();
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

    private void WinPoints(int value)
    {
        Points += value;
        ScoreChanged.Invoke(Points);
    }
}