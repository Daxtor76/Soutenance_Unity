using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
    public GameObject characterPrefab;
    public Transform spawnPoint;
    private Character _character;


    private void Awake()
    {
        _character = new Character(
            null,
            Instantiate(characterPrefab, new Vector3(spawnPoint.position.x, 0.1f, spawnPoint.position.z), Quaternion.identity),
            2.0f);
        _character.StateChanged.AddListener(OnStateChanged);
        _character.CorridorChanged.AddListener(OnCorridorChanged);
        _character.Jumping.AddListener(OnJump);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W))
        {
            if (_character.state == Character.States.Idle)
                _character.SetState(Character.States.RunSlow);
            else if (_character.state == Character.States.RunSlow)
                _character.SetState(Character.States.RunFast);
            else
                _character.SetState(Character.States.Idle);
        }
        else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A))
        {
            if (_character.state != Character.States.Idle)
                _character.SetCorridor(-1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (_character.state != Character.States.Idle)
                _character.SetCorridor(1);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_character.state != Character.States.Idle)
            {
                _character.Jumping.Invoke();
            }
        }
        
        _character.mover?.Move(_character);
    }

    private void OnJump()
    {
        if (_character.mover.IsGrounded(_character))
        {
            _character.animator.SetTrigger("Jump");
            _character.mover?.Jump(_character);
        }
    }

    private void OnCorridorChanged(int direction)
    {
        _character.animator.SetTrigger("Strafe");
        _character.mover?.Strafe(_character, direction);
    }

    private void OnStateChanged()
    {
        _character.animator.SetInteger("CharacterState",(int)_character.state);
        
        if (_character.state == Character.States.RunSlow)
            _character.SetMover(new WalkMover());
        else if (_character.state == Character.States.RunFast)
            _character.SetMover(new RunMover());
        else
            _character.SetMover(null);
    }
}
