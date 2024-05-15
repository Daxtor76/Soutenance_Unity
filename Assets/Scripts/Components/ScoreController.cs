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
        actor.CollisionController?.CollisionWithTriggerHappened?.AddListener(OnCollisionWithTriggerHappened);
    }

    private void OnCollisionWithTriggerHappened(GameObject other)
    {
        if (other.CompareTag(Const.PATOUNE_TAG_NAME))
        {
            WinPoints(other.GetComponent<PatouneController>().scoreValue);
            Destroy(other);
        }
    }

    private void WinPoints(int value)
    {
        Points += value;
        ScoreChanged.Invoke(Points);
    }
}