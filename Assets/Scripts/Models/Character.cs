using System;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;

public class Character
{
    public GameObject Go { get; private set; }
    public AnimationController AnimationController { get; private set; }
    public CollisionController CollisionController { get; private set; }
    public MovementController MovementController { get; private set; }
    public IMover groundMover { get; private set; }
    public StateController StateController { get; private set; }
    public IState idleState { get; private set; }
    public IState runState { get; private set; }
    private Rigidbody _rigidbody;
    public ScoreController ScoreController { get; private set; }

    public Character(GameObject go)
    {
        Go = go;
        
        StateController = Go.AddComponent<StateController>();
        AnimationController = Go.AddComponent<AnimationController>();
        CollisionController = Go.AddComponent<CollisionController>();
        MovementController = Go.AddComponent<MovementController>();
        ScoreController = Go.AddComponent<ScoreController>();

        _rigidbody = Go.AddComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
        
        idleState = new CharacterIdleState(this);
        runState = new CharacterRunState(this);
        StateController.ChangeState(idleState);
        
        groundMover = new GroundMover(Const.CHARACTER_FORWARD_SPEED);
    }
}
