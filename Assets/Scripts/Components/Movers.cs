using System;
using UnityEngine;

public abstract class Mover : IMover
{
    public abstract void Move(Character character);

    public virtual void Strafe(Character character, string direction)
    {
        Vector3 strafeVector = new Vector3();
        Vector3 characterCurrentPosition = character.go.transform.position;
        
        switch (direction)
        {
            case "right":
                strafeVector.x += 2;
                break;
            case "left":
                strafeVector.x -= 2;
                break;
        }

        Vector3 finalVector = new Vector3(
            Mathf.Clamp(characterCurrentPosition.x + strafeVector.x, character.initialPos.x - 2, character.initialPos.x + 2),
            characterCurrentPosition.y,
            characterCurrentPosition.z);
        character.go.transform.position = finalVector;
    }
}

public class WalkMover : Mover
{
    public override void Move(Character character)
    {
        character.go.transform.position += new Vector3(
            0.0f,
            0.0f,
            character.speed * Time.deltaTime);
    }
}

public class RunMover : Mover
{
    public override void Move(Character character)
    {
        character.go.transform.position += new Vector3(
            0.0f,
            0.0f,
            character.speed * Time.deltaTime * 3f);
    }
}
