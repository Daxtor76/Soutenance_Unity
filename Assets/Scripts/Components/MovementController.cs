using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MovementController : MonoBehaviour
{
    public Actor Actor { get; private set; }
    public IMover CurrentMover { get; private set; }
    public IMover runMover { get; protected internal set; }

    private void Awake()
    {
        Actor = GetComponent<Actor>();
    }

    void Update()
    {
        CurrentMover?.UpdateMover(Actor);
    }

    private void FixedUpdate()
    {
        CurrentMover?.FixedUpdateMover(Actor);
    }

    public void ChangeMover(IMover newMover)
    {
        CurrentMover?.Exit(Actor);
        CurrentMover = newMover;
        CurrentMover?.Enter(Actor);
    }
}
