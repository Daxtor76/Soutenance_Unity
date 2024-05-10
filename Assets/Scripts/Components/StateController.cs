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
        character.CollisionController.CollisionHappened.AddListener(OnCollisionHappened);
    }

    private void OnCollisionHappened(GameObject other)
    {
        CurrentState?.OnCollisionHappened(character, other);
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