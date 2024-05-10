using UnityEngine;

public interface IState
{
    void Enter(Character character);
    void Update(Character character);
    void Exit(Character character);
    void OnCollisionHappened(Character character, GameObject other);
}