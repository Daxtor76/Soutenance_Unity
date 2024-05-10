using UnityEngine;

public abstract class State : IState
{
    public virtual void Enter(Character character)
    {
    }

    public virtual void Update(Character character)
    {
    }

    public virtual void Exit(Character character)
    {
    }

    public virtual void OnCollisionHappened(Character character, GameObject other)
    {
        if (other.CompareTag(Const.OBSTACLE_TAG_NAME))
        {
            character.StateController.ChangeState(character.idleState);
        }
    }
}

public class CharacterIdleState : State
{
    public override void Enter(Character character)
    {
        character.MovementController.CurrentMover = null;
        character.AnimationController.Animator.SetInteger("CharacterState",0);
    }

    public override void Update(Character character)
    {
        base.Update(character);
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W))
        {
            character.StateController.ChangeState(character.smoothRunState);
        }
    }
}

public class CharacterSmoothRunState : State
{
    public override void Enter(Character character)
    {
        character.MovementController.CurrentMover = new SmoothGroundMover(Const.CHARACTER_FORWARD_SPEED, Const.CHARACTER_SIDE_SPEED);
        character.AnimationController.Animator.SetInteger("CharacterState",1);
    }

    public override void Update(Character character)
    {
        base.Update(character);
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A))
        {
            character.MovementController.CurrentMover.Strafe(-1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            character.MovementController.CurrentMover.Strafe(1);
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            character.AnimationController.Animator.SetTrigger("Jump");
            character.MovementController.Jump(Const.CHARACTER_JUMP_HEIGHT);
        }
        else if (Input.GetKeyDown(KeyCode.S))
            character.StateController.ChangeState(character.idleState);
    }
}