using System;
using UnityEngine;
public class Character : Actor
{
    private void Start()
    {
        StateController.idleState = new CharacterIdleState();
        StateController.runState = new CharacterRunState();
        
        StateController?.ChangeState(StateController.idleState);
    }
}