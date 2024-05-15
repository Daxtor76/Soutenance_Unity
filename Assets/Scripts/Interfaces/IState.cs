using UnityEngine;

public interface IState
{
    void Enter(Actor actor);
    void Update(Actor actor);
    void Exit(Actor actor);
    void OnCollisionWithTriggerHappened(Actor actor, GameObject other);
}