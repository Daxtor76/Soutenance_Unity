using System;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public CharacterController CharacterController { get; private set; }
    public AnimationController AnimationController { get; private set; }
    public CollisionController CollisionController { get; private set; }
    public MovementController MovementController { get; private set; }
    public IMover groundMover { get; private set; }
    public StateController StateController { get; private set; }
    public IState idleState { get; private set; }
    public IState smoothRunState { get; private set; }
    public ScoreController ScoreController { get; private set; }

    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        
        CollisionController = this.AddComponent<CollisionController>();
        
        StateController = this.AddComponent<StateController>();
        idleState = new CharacterIdleState();
        smoothRunState = new CharacterSmoothRunState();
        
        AnimationController = this.AddComponent<AnimationController>();
        ScoreController = this.AddComponent<ScoreController>();
        
        MovementController = this.AddComponent<MovementController>();
        groundMover = new SmoothGroundMover(Const.CHARACTER_FORWARD_SPEED, Const.CHARACTER_SIDE_SPEED);
        
        StateController.ChangeState(idleState);
    }
}
