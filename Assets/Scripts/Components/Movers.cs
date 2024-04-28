using UnityEngine;

public abstract class Mover : IMover
{
    public abstract void Move(Character character);

    public void Strafe(Character character, int direction)
    {
        float offset = 0.0f;
        
        switch (direction)
        {
            case 1:
                offset += Const.SPACINGX;
                break;
            case -1:
                offset -= Const.SPACINGX;
                break;
        }

        character.targetX = offset;
    }

    public void Jump(Character character)
    {
        character.targetY = Const.JUMPHEIGHT;
    }

    public bool IsGrounded(Character character)
    {
        return Physics.Raycast(character.go.transform.position, Vector2.down, 0.1f);
    }
}

public class WalkMover : Mover
{
    public override void Move(Character character)
    {
        // Apply Gravity
        character.targetY += Const.GRAVITY * Time.deltaTime;
        
        if (character.targetX > 0.0f)
            character.targetX += Const.GRAVITY * Time.deltaTime;
        else
            character.targetX -= Const.GRAVITY * Time.deltaTime;
        
        if (IsGrounded(character) && character.targetY < 0.0f)
            character.targetY = 0.0f;
        
        character.go.transform.Translate(new Vector3(character.targetX, character.targetY, character.speed) * Time.deltaTime);
    }
}

public class RunMover : Mover
{
    public override void Move(Character character)
    {
        // Apply Gravity
        character.targetY += Const.GRAVITY * Time.deltaTime;
        
        if (character.targetX > 0.0f)
            character.targetX += Const.GRAVITY * Time.deltaTime;
        else
            character.targetX -= Const.GRAVITY * Time.deltaTime;
        
        if (IsGrounded(character) && character.targetY < 0.0f)
            character.targetY = 0.0f;
        
        character.go.transform.Translate(new Vector3(character.targetX, character.targetY, character.speed * 2) * Time.deltaTime);
    }
}
