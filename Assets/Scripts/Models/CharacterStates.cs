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
        character.AnimationController.Animator.SetInteger("CharacterState",0);
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
    public override void Enter()
    {
        character.MovementController.CurrentMover = new GroundMover(Const.CHARACTER_FORWARD_SPEED);
        character.AnimationController.Animator.SetInteger("CharacterState",1);
        character.Go.GetComponent<CollisionController>().CollisionHappened.AddListener(OnCollisionHappened);
    }

    private void OnCollisionHappened(GameObject other)
    {
        if (other.CompareTag(Const.OBSTACLE_TAG_NAME))
        {
            character.StateController.ChangeState(character.idleState);
        }
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A))
        {
            if (character.MovementController.CorridorId > -1)
            {
                character.MovementController.SetCorridor(-1);
                character.AnimationController.Animator.SetTrigger("Strafe");
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (character.MovementController.CorridorId < 1)
            {
                character.MovementController.SetCorridor(1);
                character.AnimationController.Animator.SetTrigger("Strafe");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            character.AnimationController.Animator.SetTrigger("Jump");
            character.MovementController.Jump(Const.CHARACTER_JUMP_HEIGHT);
        }
        else if (Input.GetKeyDown(KeyCode.S))
            character.StateController.ChangeState(character.idleState);
    }

    public override void Exit()
    {
        character.Go.GetComponent<CollisionController>().CollisionHappened.RemoveListener(OnCollisionHappened);
    }
}