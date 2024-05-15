using System;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Events;

public class Actor : MonoBehaviour
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
    public InputHandler InputHandler { get; private set; }

    private void Awake()
    {
        if (TryGetComponent<CharacterController>(out CharacterController characterController))
            CharacterController = characterController;
        
        if (TryGetComponent<CollisionController>(out CollisionController collisionController))
            CollisionController = collisionController;
        
        if (TryGetComponent<InputHandler>(out InputHandler inputHandler))
            InputHandler = inputHandler;

        if (TryGetComponent<MovementController>(out MovementController movementController))
        {
            MovementController = movementController;
            if (InputHandler != null)
                groundMover = new SmoothGroundMover(Const.CHARACTER_FORWARD_SPEED, Const.CHARACTER_SIDE_SPEED);
            else
                groundMover = new SmoothGroundMover(Const.ENEMY_FORWARD_SPEED, Const.ENEMY_SIDE_SPEED);
        }

        if (TryGetComponent<StateController>(out StateController stateController))
        {
            StateController = stateController;
            idleState = new IdleState();
            smoothRunState = new SmoothRunState();
        }
        
        if (TryGetComponent<ScoreController>(out ScoreController scoreController))
            ScoreController = scoreController;
        
        if (TryGetComponent<AnimationController>(out AnimationController animationController))
            AnimationController = animationController;
    }
}
