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
            Instantiate(characterPrefab, new Vector3(spawnPoint.position.x, 0.0f, spawnPoint.position.z), Quaternion.identity),
            2.0f);
        _character.StateChanged.AddListener(OnStateChanged);
        _character.CorridorChanged.AddListener(OnCorridorChanged);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_character.state == Character.States.Idle)
                _character.SetState(Character.States.RunSlow);
            else if (_character.state == Character.States.RunSlow)
                _character.SetState(Character.States.RunFast);
            else
                _character.SetState(Character.States.Idle);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_character.state != Character.States.Idle)
                _character.SetCorridor("left");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (_character.state != Character.States.Idle)
                _character.SetCorridor("right");
        }
        _character.mover?.Move(_character);
    }

    private void OnCorridorChanged(string direction)
    {
        _character.mover?.Strafe(_character, direction);
        // TO DO: Add strafe animation
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
