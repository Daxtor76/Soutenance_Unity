using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateController : MonoBehaviour
{
    public Actor.States CurrentState { get; private set;}
    public Actor.States PreviousState { get; private set;}
    
    public UnityEvent<Actor.States> OnStateChange = new UnityEvent<Actor.States>();

    public void ChangeState(Actor.States newstate)
    {
        PreviousState = CurrentState;
        CurrentState = newstate;
        OnStateChange?.Invoke(CurrentState);
        Debug.Log($"{transform.gameObject.name} has changed from {PreviousState} to {CurrentState} state");
    }
}
