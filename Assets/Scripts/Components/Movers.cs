using System;
using UnityEngine;

public abstract class Mover : IMover
{
    public abstract void Move(Character character);

    public virtual void Strafe(Character character, int direction)
    {
        float offset = 0.0f;
        Vector3 characterCurrentPosition = character.go.transform.position;
        
        switch (direction)
        {
            case 1:
                offset += 2;
                break;
            case -1:
                offset -= 2;
                break;
        }

        character.targetX = Mathf.Clamp(character.targetX + offset, character.initialPos.x - 2, character.initialPos.x + 2);
    }
}

public class WalkMover : Mover
{
    public override void Move(Character character)
    {
        Vector3 from = character.go.transform.position;
        Vector3 to = new Vector3(
            character.targetX, 
            0.0f,
            character.go.transform.position.z + character.speed * Time.deltaTime);
        
        character.go.transform.position = new Vector3(
            Mathf.Lerp(from.x, to.x, 3 * Time.deltaTime),
            0.0f,
            Mathf.Lerp(from.z, to.z, 250 * Time.deltaTime));
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
