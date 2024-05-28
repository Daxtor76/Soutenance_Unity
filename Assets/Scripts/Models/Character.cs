using System;
using UnityEngine;
public class Character : Actor
{
    private void Start()
    {
        StateController.idleState = new CharacterIdleState();
        StateController.runState = new CharacterRunState();
        StateController.sneakyState = new CharacterSneakyState();
        
        StateController?.ChangeState(StateController.idleState);
        
        MovementController.runMover = new CharacterMover(
            Const.CHARACTER_FORWARD_SPEED,
            Const.CHARACTER_STRAFE_SPEED,
            Const.CHARACTER_ROTATION_SPEED
        );
    }
}