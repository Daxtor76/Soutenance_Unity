using System;
using UnityEngine;
using UnityEngine.Events;

public class ScoreController : MonoBehaviour
{
    public Character character;
    public int Points { get; private set; }
    public UnityEvent<int> ScoreChanged = new UnityEvent<int>();
    private void Awake()
    {
        character = GetComponent<Character>();
        Points = 0;
    }

    private void Start()
    {
        character.CollisionController.CollisionHappened.AddListener(OnCollisionHappened);
    }

    private void OnCollisionHappened(GameObject other)
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