using UnityEngine;

public class CharacterIdleState : State
{
    public override void Enter(Actor actor)
    {
        actor.MovementController.ChangeMover(null);
    }

    public override void Update(Actor actor)
    {
        if (Input.GetButtonDown(Const.RUN_AXIS_NAME))
        {
            actor.StateController.ChangeState(actor.StateController.runState);
        }
    }
}

public class CharacterRunState : State
{
    public override void Enter(Actor actor)
    {
        actor.MovementController.ChangeMover(new CharacterMover(
            Const.CHARACTER_FORWARD_SPEED,
            Const.CHARACTER_SIDE_SPEED,
            Const.CHARACTER_ROTATION_SPEED
        ));
        actor.AnimationController?.Animator?.SetInteger("CharacterState",1);
    }
}

public class CharacterSneakyState : State
{
    public override void Enter(Actor actor)
    {
        actor.MovementController.ChangeMover(new CharacterSneakyMover(
            Const.CHARACTER_SNEAKY_FORWARD_SPEED,
            Const.CHARACTER_SNEAKY_SIDE_SPEED,
            Const.CHARACTER_ROTATION_SPEED
        ));
    }
}