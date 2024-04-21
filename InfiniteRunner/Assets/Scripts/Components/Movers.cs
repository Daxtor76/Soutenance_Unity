using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkMover : IMover
{
    public void Move(Character character)
    {
        Debug.Log("je marche");
        character.Go.transform.position += new Vector3(
            0.0f,
            0.0f,
            character.Speed * Time.deltaTime);
    }
}

public class RunMover : IMover
{
    public void Move(Character character)
    {
        Debug.Log("je cours");
        character.Go.transform.position += new Vector3(
            0.0f,
            0.0f,
            character.Speed * Time.deltaTime * 3f);
    }
}
