using System;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;

public class Character
{
    public GameObject Go { get; private set; }
    public Animator Animator { get; private set; }
    public MovementController MovementController { get; private set; }
    public IMover groundMover { get; private set; }
    
    public StateController StateController { get; private set; }
    public IState idleState { get; private set; }
    public IState runState { get; private set; }

    public Character(GameObject go)
    {
        Go = go;
        
        Animator = GetAnimator();
        StateController = Go.AddComponent<StateController>();
        MovementController = Go.AddComponent<MovementController>();
        
        idleState = new CharacterIdleState(this);
        runState = new CharacterRunState(this);
        StateController.ChangeState(idleState);
        
        groundMover = new GroundMover(Const.CHARACTER_FORWARD_SPEED);
    }

    private Animator GetAnimator()
    {
        Animator animator = Go.GetComponentInChildren<Animator>();
        return animator != null ? animator : null;
    }
}
