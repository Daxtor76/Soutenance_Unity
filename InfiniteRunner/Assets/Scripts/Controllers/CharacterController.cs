using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GameObject characterPrefab;
    Character character;

    private void Awake()
    {
        character = new Character(
            null,
            Instantiate(characterPrefab, Vector3.zero, Quaternion.identity),
            2.0f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (character.Mover?.GetType() == typeof(WalkMover))
                character.SetMover(new RunMover());
            else if (character.Mover?.GetType() == typeof(RunMover))
                character.SetMover(null);
            else
                character.SetMover(new WalkMover());
        }
        character.Mover?.Move(character);
    }
}
