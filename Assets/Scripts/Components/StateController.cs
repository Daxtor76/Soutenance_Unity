using System;
using UnityEngine;
using UnityEngine.Events;

public class StateController : MonoBehaviour
{
    public Actor Actor { get; private set; }
    public IState CurrentState { get; set; }
    public UnityEvent<IState> StateChanged = new UnityEvent<IState>();

    private void Awake()
    {
        Actor = GetComponent<Actor>();
        Actor.CollisionController?.CollisionWithTriggerHappened.AddListener(OnCollisionWithTriggerHappened);
        
        ChangeState(Actor.idleState);
    }

    private void OnCollisionWithTriggerHappened(GameObject other)
    {
        CurrentState?.OnCollisionWithTriggerHappened(Actor, other);
    }

    private void Update()
    {
        CurrentState?.Update(Actor);
    }

    public void ChangeState(IState newState)
    {
        CurrentState?.Exit(Actor);
        CurrentState = newState;
        CurrentState?.Enter(Actor);
        
        StateChanged.Invoke(CurrentState);
    }
}