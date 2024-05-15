using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MovementController : MonoBehaviour
{
    private Actor actor;
    public IMover groundMover { get; private set; }
    public IMover CurrentMover { get; set; }

    private void Awake()
    {
        actor = GetComponent<Actor>();
        if (actor.InputHandler)
            groundMover = new SmoothGroundMover(Const.CHARACTER_FORWARD_SPEED, Const.CHARACTER_SIDE_SPEED);
        else
            groundMover = new SmoothGroundMover(Const.ENEMY_FORWARD_SPEED, Const.ENEMY_SIDE_SPEED);
    }

    void Update()
    {
        CurrentMover?.Run(actor.CharacterController);
        if (actor.InputHandler)
        {
            CurrentMover?.Strafe(Input.GetAxisRaw(Const.STRAFE_AXIS_NAME));
            
            if (CurrentMover.IsGrounded(actor.CharacterController))
            {
                if (Input.GetButtonDown(Const.JUMP_AXIS_NAME))
                    CurrentMover?.Jump(Const.CHARACTER_JUMP_HEIGHT);
                else if (Input.GetKeyDown(KeyCode.S)) // TO DO: remove it when DISCRETION feature is done
                    actor.StateController.ChangeState(actor.StateController.idleState);
            }
        }
    }
}
