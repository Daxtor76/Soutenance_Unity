using System;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Actor _actor;
    public Animator Animator { get; private set; }
    public ActorAnimator CurrentActorAnimator { get; private set; }

    private void Awake()
    {
        _actor = GetComponent<Actor>();
        Animator = GetAnimator();

        _actor.StateController?.OnStateChange?.AddListener(AdaptOnStateChange);
    }

    private void Update()
    {
        CurrentActorAnimator?.UpdateActorAnimator(_actor);
    }

    public void SetActorAnimator(ActorAnimator actorAnimator)
    {
        CurrentActorAnimator?.Exit(_actor);
        CurrentActorAnimator = actorAnimator;
        CurrentActorAnimator?.Enter(_actor);
    }
    private void AdaptOnStateChange(Actor.States state)
    {
        CurrentActorAnimator?.AdaptOnStateChange(_actor, state);
    }

    private Animator GetAnimator()
    {
        Animator animator = gameObject.GetComponentInChildren<Animator>();
        return animator != null ? animator : null;
    }
}