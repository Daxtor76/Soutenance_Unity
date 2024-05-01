using UnityEngine;
using UnityEngine.Events;

public abstract class Mover : IMover
{
    public float Speed { get; set; }
    public Vector3 direction = Vector3.zero;
    public abstract void Move(GameObject go);
    public void Strafe(int pDir)
    {
        float offset = 0.0f;
        
        switch (pDir)
        {
            case 1:
                offset += Const.CHARACTER_STRAFE_DISTANCE;
                break;
            case -1:
                offset -= Const.CHARACTER_STRAFE_DISTANCE;
                break;
        }
        direction.x = offset;
    }

    public void Jump(float jumpHeight)
    {
        direction.y = jumpHeight;
    }

    public bool IsGrounded(GameObject go)
    {
        return Physics.Raycast(go.transform.position, Vector2.down, 0.1f);
    }
}

public class GroundMover : Mover
{
    public GroundMover(float pSpeed)
    {
        Speed = pSpeed;
    }
    public override void Move(GameObject go)
    {
        // Apply Gravity
        direction.y += Const.GRAVITY * Time.deltaTime;
        
        if (direction.x > 0.0f)
            direction.x += Const.GRAVITY * Time.deltaTime;
        else
            direction.x -= Const.GRAVITY * Time.deltaTime;
        
        if (IsGrounded(go) && direction.y < 0.0f)
            direction.y = 0.0f;
        
        go.transform.Translate(new Vector3(direction.x, direction.y, Speed) * Time.deltaTime);
    }
}
