using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MovementController : MonoBehaviour
{
    private Actor actor;
    public IMover CurrentMover { get; set; }

    private void Awake()
    {
        actor = GetComponent<Actor>();
    }

    void Update()
    {
        CurrentMover?.Move(actor.CharacterController);
    }
}
