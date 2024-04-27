using UnityEngine;

public class Character
{
    public enum States
    {
        Idle,
        RunSlow,
        RunFast
    }

    public GameObject Go { get; private set; }
    public Animator Animator { get; private set; }
    public IMover Mover { get; private set; }
    public float Speed { get; private set; }
    public States State { get; private set; }

    public Character(IMover mover, GameObject go, float speed)
    {
        this.Mover = mover;
        this.Go = go;
        this.Speed = speed;
        this.State = States.Idle;
        this.Animator = GetAnimator();
    }

    private Animator GetAnimator()
    {
        Animator animator = Go.GetComponentInChildren<Animator>();
        return animator != null ? animator : null;
    }

    public void SetState(States state)
    {
        State = state;
        Animator.SetInteger("CharacterState",(int)State);
    }

    public void SetMover(IMover mover)
    {
        this.Mover = mover;
    }
}
