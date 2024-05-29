using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateController : MonoBehaviour
{
    public Actor.States CurrentState { get; private set;}
    
    public UnityEvent<Actor.States> OnStateChange = new UnityEvent<Actor.States>();

    public void ChangeState(Actor.States newstate)
    {
        CurrentState = newstate;
        OnStateChange?.Invoke(CurrentState);
    }
}
