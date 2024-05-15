using System;
using UnityEngine;
using UnityEngine.Events;

public class StateController : MonoBehaviour
{
    public Actor Actor { get; private set; }
    public IState CurrentState { get; set; }
    public UnityEvent<IState> StateChanged = new UnityEvent<IState>();
    
    public IState idleState { get; private set; }
    public IState smoothRunState { get; private set; }

    private void Awake()
    {
        Actor = GetComponent<Actor>();
        Actor.CollisionController?.OnCollisionWithObstacle.AddListener(OnObstacleHit);
        
        idleState = new IdleState();
        smoothRunState = new SmoothRunState();
        
        ChangeState(idleState);
    }

    private void OnObstacleHit(GameObject other)
    {
        CurrentState?.OnObstacleHit(Actor, other);
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