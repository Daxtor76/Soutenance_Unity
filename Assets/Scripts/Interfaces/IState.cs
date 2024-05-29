using UnityEngine;

public interface IState
{
    void Enter(Actor actor);
    void UpdateState(Actor actor);
    void Exit(Actor actor);
    void OnObstacleHit(Actor actor, GameObject other);
}