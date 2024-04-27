using System;
using UnityEngine;
using UnityEngine.Events;

public class Character
{
    public enum States
    {
        Idle,
        RunSlow,
        RunFast
    }

    public GameObject go { get; private set; }
    public Vector3 initialPos { get; private set; }
    public Animator animator { get; private set; }
    public IMover mover { get; private set; }
    public float speed { get; private set; }
    public States state { get; private set; }
    public int corridorId { get; private set; }
    
    public UnityEvent StateChanged = new UnityEvent();
    public UnityEvent<string> CorridorChanged = new UnityEvent<string>();

    public Character(IMover mover, GameObject go, float speed)
    {
        this.mover = mover;
        this.go = go;
        this.speed = speed;
        this.state = States.Idle;
        this.animator = GetAnimator();
        corridorId = 0;
        initialPos = go.transform.position;
    }

    private Animator GetAnimator()
    {
        Animator animator = go.GetComponentInChildren<Animator>();
        return animator != null ? animator : null;
    }

    public void SetCorridor(string direction)
    {
        int corridor = 0;
        switch (direction)
        {
            case "right":
                corridor += 1;
                break;
            case "left":
                corridor -= 1;
                break;
        }
        corridorId = Math.Clamp(corridor, -1, 1);
        CorridorChanged?.Invoke(direction);
    }

    public void SetState(States state)
    {
        this.state = state;
        StateChanged?.Invoke();
    }

    public void SetMover(IMover mover)
    {
        this.mover = mover;
    }
}
