using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MovementController : MonoBehaviour
{
    public GameController gameController;
    public IMover CurrentMover { get; set; }

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    private void Start()
    {
        gameController.Character.CorridorChanged.AddListener(OnStrafe);
    }

    void Update()
    {
        CurrentMover?.Move(gameController.Character);
    }
    
    public void Jump(float jumpHeight)
    {
        if (CurrentMover != null)
        {
            if (CurrentMover.IsGrounded(gameController.Character))
            {
                CurrentMover?.Jump(jumpHeight);
            }
        }
    }

    private void OnStrafe(int direction)
    {
        CurrentMover?.Strafe(gameController.Character, direction);
    }
}
