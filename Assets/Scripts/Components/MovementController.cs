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
        if (TryGetComponent<CharacterController>(out CharacterController cc))
            characterController = cc;
    }

    void Update()
    {
        CurrentMover?.Move(characterController);
    }
}
