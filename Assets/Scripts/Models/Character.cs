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
    public int CorridorId { get; private set; }
    public UnityEvent<int> CorridorChanged = new UnityEvent<int>();

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
        CorridorId = 0;
    }

    private Animator GetAnimator()
    {
        Animator animator = Go.GetComponentInChildren<Animator>();
        return animator != null ? animator : null;
    }

    public void SetCorridor(int direction)
    {
        int corridor = 0;
        switch (direction)
        {
            case 1:
                corridor += 1;
                break;
            case -1:
                corridor -= 1;
                break;
        }

        if (CorridorId == corridor)
            return;

        CorridorId += corridor;
        CorridorChanged.Invoke(direction);
    }
}
