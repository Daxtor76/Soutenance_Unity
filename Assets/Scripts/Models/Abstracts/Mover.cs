﻿using UnityEngine;

public abstract class Mover : IMover
{
    protected float ForwardSpeed { get; set; }
    protected float SideSpeed { get; set; }
    protected Vector3 velocity = Vector3.zero;

    public virtual void Run(CharacterController characterController)
    {
        if (IsGrounded(characterController) && velocity.y < 0f)
            velocity.y = 0f;
        
        characterController.Move(velocity * Time.deltaTime);
        
        velocity.y += Const.GRAVITY * Time.deltaTime;
    }

    public virtual void Strafe(float pDir)
    {
        switch (pDir)
        {
            case > 0:
                velocity.x = SideSpeed;
                break;
            case < 0:
                velocity.x = -SideSpeed;
                break;
            default:
                velocity.x = 0.0f;
                break;
        }
    }

    public void Jump(float jumpHeight)
    {
        velocity.y = jumpHeight;
    }

    public virtual void Enter(Actor actor)
    {
    }

    public virtual void Update(Actor actor)
    {
    }

    public virtual void Exit(Actor actor)
    {
    }

    public bool IsGrounded(CharacterController characterController)
    {
        if (characterController)
        {
            return Physics.Raycast(characterController.transform.position, Vector3.down, 0.05f);
        }
        return true;
    }

    public void SetForwardSpeed(float newSpeed)
    {
        ForwardSpeed = newSpeed;
    }

    public void SetSideSpeed(float newSpeed)
    {
        SideSpeed = newSpeed;
    }
}