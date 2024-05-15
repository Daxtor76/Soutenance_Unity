using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MovementController : MonoBehaviour
{
    private Actor actor;
    public IMover groundMover { get; private set; }
    public IMover CurrentMover { get; set; }

    private void Awake()
    {
        actor = GetComponent<Actor>();
        if (actor.InputHandler != null)
            groundMover = new SmoothGroundMover(Const.CHARACTER_FORWARD_SPEED, Const.CHARACTER_SIDE_SPEED);
        else
            groundMover = new SmoothGroundMover(Const.ENEMY_FORWARD_SPEED, Const.ENEMY_SIDE_SPEED);
    }

    void Update()
    {
        CurrentMover?.Move(actor.CharacterController);
    }
}
