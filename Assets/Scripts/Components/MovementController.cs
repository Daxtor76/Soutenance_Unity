using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MovementController : MonoBehaviour
{
    private CharacterController characterController;

    public IMover CurrentMover { get; set; }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        CurrentMover?.Move(characterController);
    }
}
