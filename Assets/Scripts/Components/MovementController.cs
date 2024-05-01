using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MovementController : MonoBehaviour
{
    public IMover CurrentMover { get; set; }
    
    public int CorridorId { get; private set; }

    private void Awake()
    {
        CorridorId = 0;
    }

    void Update()
    {
        CurrentMover?.Move(transform.gameObject);
    }
    
    public void Jump(float jumpHeight)
    {
        if (CurrentMover != null)
        {
            if (CurrentMover.IsGrounded(transform.gameObject))
            {
                CurrentMover?.Jump(jumpHeight);
            }
        }
    }

    public void SetCorridor(int direction)
    {
        int corridor = 0;
        switch (direction)
        {
            case 1:
                corridor += 1;
                break;
            case -1:
                corridor -= 1;
                break;
        }

        if (CorridorId == corridor)
            return;

        CorridorId += corridor;
        CurrentMover?.Strafe(direction);
    }
}
