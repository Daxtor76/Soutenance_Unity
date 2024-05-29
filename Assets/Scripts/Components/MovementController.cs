using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class MovementController : MonoBehaviour
{
    public Actor Actor { get; private set; }
    public IMover CurrentMover { get; private set; }
    public IMover runMover { get; protected internal set; }

    private void Awake()
    {
        Actor = GetComponent<Actor>();
        Actor.StateController.OnStateChange.AddListener(AdaptMoverOnStateChange);
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

    void AdaptMoverOnStateChange(Actor.States state)
    {
        if (state == Actor.States.idle || state == Actor.States.sleep || state == Actor.States.dead)
        {
            ChangeMover(null);
            return;
        }
        else
        {
            if (CurrentMover == null)
            {
                ChangeMover(runMover);
                return;
            }
            CurrentMover?.AdaptMoverOnStateChange(state);
        }

    }
}
