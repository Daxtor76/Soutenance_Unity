using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateController : MonoBehaviour
{
    public Actor Actor { get; private set; }
    public IState CurrentState { get; set; }
    public IState idleState { get; protected internal set; }
    public IState runState { get; protected internal set; }
    public IState sleepState { get; protected internal set; }
    public IState sneakyState { get; protected internal set; }

    private void Awake()
    {
        Actor = GetComponent<Actor>();
        Actor.CollisionController?.OnCollisionWithObstacle.AddListener(OnObstacleHit);
    }

    private void Update()
    {
        CurrentState?.Update(Actor);
    }

    private void OnObstacleHit(GameObject other)
    {
        CurrentState?.OnObstacleHit(Actor, other);
    }

    public void ChangeState(IState newState)
    {
        CurrentState?.Exit(Actor);
        CurrentState = newState;
        CurrentState?.Enter(Actor);
    }
}