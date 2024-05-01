using System;
using UnityEngine;
using UnityEngine.Events;

public class StateController : MonoBehaviour
{
    public IState CurrentState { get; set; }

    private void Update()
    {
        CurrentState?.Update();
    }

    public void ChangeState(IState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }
}