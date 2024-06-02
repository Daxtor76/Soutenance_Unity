using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActorAnimator : IActorAnimator
{
    protected Actor _actor;

    public virtual void AdaptOnStateChange(Actor actor, Actor.States state)
    {
    }

    public virtual void Enter(Actor actor)
    {
    }

    public virtual void Exit(Actor actor)
    {
    }

    public virtual void UpdateActorAnimator(Actor actor)
    {
    }
}
