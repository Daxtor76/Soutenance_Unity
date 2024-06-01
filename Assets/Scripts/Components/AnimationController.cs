using System;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Actor _actor;
    public Animator Animator { get; private set; }
    private float _movementBlender = 0.0f;
    private float _movementBlendSpeed = 5.0f;

    private void Awake()
    {
        _actor = GetComponent<Actor>();
        Animator = GetAnimator();

        Animator?.SetFloat("MovementBlend", _movementBlender);

        _actor?.StateController?.OnStateChange?.AddListener(AdaptOnStateChange);
    }

    private void Update()
    {
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

        Animator?.SetFloat("MovementBlend", _movementBlender);
    }

    private void AdaptOnStateChange(Actor.States state)
    {
        if (state == Actor.States.kyubi)
            Animator.SetBool("IsKyubi", true);
        else if (state == Actor.States.sneak)
            Animator.SetBool("IsSneaky", true);
        else if (state == Actor.States.run)
            Animator.SetTrigger("LaunchGame");

        if (state != Actor.States.kyubi && state != Actor.States.sneak)
        {
            Animator.SetBool("IsKyubi", false);
            Animator.SetBool("IsSneaky", false);
        }
    }

    private Animator GetAnimator()
    {
        Animator animator = gameObject.GetComponentInChildren<Animator>();
        return animator != null ? animator : null;
    }
}