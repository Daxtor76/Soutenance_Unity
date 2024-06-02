using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class CharacterAnimator : ActorAnimator
{
    private float _movementBlender = 0.0f;
    private float _movementBlendSpeed = 5.0f;

    public override void Enter(Actor actor)
    {
        base.Enter(actor);

        _actor = actor;

        actor.AnimationController?.Animator?.SetFloat("MovementBlend", _movementBlender);
        actor.CollisionController?.OnCollisionWithEnemy?.AddListener(Attack);
    }

    public override void UpdateActorAnimator(Actor actor)
    {
        base.UpdateActorAnimator(actor);
        _movementBlender += _movementBlendSpeed * Input.GetAxisRaw(Const.STRAFE_AXIS_NAME) * Time.deltaTime;
        _movementBlender = Mathf.Clamp(_movementBlender, -1.0f, 1.0f);

        if (Input.GetAxisRaw(Const.STRAFE_AXIS_NAME) == 0.0f)
        {
            if (_movementBlender < 0.0f)
                _movementBlender += _movementBlendSpeed * Time.deltaTime;
            else if (_movementBlender > 0.0f)
                _movementBlender -= _movementBlendSpeed * Time.deltaTime;
            else
                _movementBlender = 0.0f;
        }

        actor.AnimationController?.Animator.SetFloat("MovementBlend", _movementBlender);
    }

    private void Attack(Actor other)
    {
        if (_actor.StateController?.CurrentState == Actor.States.kyubi)
            _actor.AnimationController?.Animator?.SetTrigger("Attack");
    }

    public override void AdaptOnStateChange(Actor actor, Actor.States state)
    {
        if (state == Actor.States.kyubi)
            actor.AnimationController?.Animator.SetBool("IsKyubi", true);
        else if (state == Actor.States.sneak)
            actor.AnimationController?.Animator.SetBool("IsSneaky", true);
        else if (state == Actor.States.run)
            actor.AnimationController?.Animator.SetTrigger("LaunchGame");
        else if (state == Actor.States.dead)
            actor.AnimationController?.Animator.SetInteger("Death", Random.Range(1, 3));

        if (state != Actor.States.kyubi && state != Actor.States.sneak)
        {
            actor.AnimationController?.Animator.SetBool("IsKyubi", false);
            actor.AnimationController?.Animator.SetBool("IsSneaky", false);
        }
    }

    public override void Exit(Actor actor)
    {
        base.Exit(actor);
    }
}
