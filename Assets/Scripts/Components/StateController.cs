using System;
using UnityEngine;
using UnityEngine.Events;

public class StateController : MonoBehaviour
{
    public Character character { get; private set; }
    public IState CurrentState { get; set; }
    public UnityEvent<IState> StateChanged = new UnityEvent<IState>();

    private void Awake()
    {
        character = GetComponent<Character>();
        character.CollisionController.CollisionWithTriggerHappened.AddListener(OnCollisionWithTriggerHappened);
    }

    private void OnCollisionWithTriggerHappened(GameObject other)
    {
        CurrentState?.OnCollisionWithTriggerHappened(character, other);
    }

    private void Update()
    {
        CurrentState?.Update(character);
    }

    public void ChangeState(IState newState)
    {
        CurrentState?.Exit(character);
        CurrentState = newState;
        CurrentState?.Enter(character);
        
        StateChanged.Invoke(CurrentState);
    }
}