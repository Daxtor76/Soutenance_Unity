using UnityEngine;
using UnityEngine.Events;

public abstract class Actor : MonoBehaviour
{
    public enum States
    {
        idle,
        run,
        sneak,
        kyubi,
        sleep
    }
    public AnimationController AnimationController { get; private set; }
    public StateController StateController { get; private set; }
    public CollisionController CollisionController { get; private set; }
    public MovementController MovementController { get; private set; }
    public ScoreController ScoreController { get; private set; }

    public virtual void Awake()
    {
        if (TryGetComponent(out StateController stateController))
            StateController = stateController;

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
