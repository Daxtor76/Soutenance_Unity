using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    public CharacterController CharacterController { get; private set; }
    public AnimationController AnimationController { get; private set; }
    public CollisionController CollisionController { get; private set; }
    public MovementController MovementController { get; private set; }
    public StateController StateController { get; private set; }
    public ScoreController ScoreController { get; private set; }
    public InputHandler InputHandler { get; private set; }

    private void Awake()
    {
        if (TryGetComponent(out InputHandler inputHandler))
            InputHandler = inputHandler;

        if (TryGetComponent(out StateController stateController))
            StateController = stateController;
        
        if (TryGetComponent(out CharacterController characterController))
            CharacterController = characterController;

        if (TryGetComponent(out MovementController movementController))
            MovementController = movementController;
        
        if (TryGetComponent(out CollisionController collisionController))
            CollisionController = collisionController;
        
        if (TryGetComponent(out ScoreController scoreController))
            ScoreController = scoreController;
        
        if (TryGetComponent(out AnimationController animationController))
            AnimationController = animationController;
    }
}