using UnityEngine;

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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_character.State == Character.States.Idle)
            {
                _character.SetState(Character.States.RunSlow);
                _character.SetMover(new WalkMover());
            }
            else if (_character.State == Character.States.RunSlow)
            {
                _character.SetState(Character.States.RunFast);
                _character.SetMover(new RunMover());
            }
            else
            {
                _character.SetState(Character.States.Idle);
                _character.SetMover(null);
            }
        }
        _character.Mover?.Move(_character);
    }
}
