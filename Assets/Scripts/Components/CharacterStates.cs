using UnityEngine;

public abstract class State : IState
{
    public Character character { get; protected set; }

    public virtual void Enter()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void Exit()
    {
    }
}

public class CharacterIdleState : State
{
    public CharacterIdleState(Character chara)
    {
        character = chara;
    }

    public override void Enter()
    {
        character.MovementController.CurrentMover = null;
        character.Animator.SetInteger("CharacterState",0);
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W))
        {
            character.StateController.ChangeState(character.runState);
        }
    }
}

public class CharacterRunState : State
{
    public CharacterRunState(Character chara)
    {
        character = chara;
    }
    public override  void Enter()
    {
        character.MovementController.CurrentMover = new GroundMover(Const.CHARACTER_FORWARD_SPEED);
        character.Animator.SetInteger("CharacterState",1);
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A))
        {
            if (character.CorridorId > -1)
            {
                character.SetCorridor(-1);
                character.Animator.SetTrigger("Strafe");
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (character.CorridorId < 1)
            {
                character.SetCorridor(1);
                character.Animator.SetTrigger("Strafe");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            character.Animator.SetTrigger("Jump");
            character.MovementController.Jump(Const.CHARACTER_JUMP_HEIGHT);
        }
        else if (Input.GetKeyDown(KeyCode.S))
            character.StateController.ChangeState(character.idleState);
    }
}