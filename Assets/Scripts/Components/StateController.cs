using System;
using UnityEngine;
using UnityEngine.Events;

public class StateController : MonoBehaviour
{
    public IState CurrentState { get; set; }
    public UnityEvent<IState> StateChanged = new UnityEvent<IState>();

    private void Update()
    {
        CurrentState?.Update();
    }

    public void ChangeState(IState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
        
        StateChanged.Invoke(CurrentState);
    }
}