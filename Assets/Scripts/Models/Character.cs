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
    }
}