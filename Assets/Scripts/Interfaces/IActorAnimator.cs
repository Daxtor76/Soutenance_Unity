using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActorAnimator
{
    void Exit(Actor actor);
    void UpdateActorAnimator(Actor actor);
    void Enter(Actor actor);
    void AdaptOnStateChange(Actor actor, Actor.States state);
}
