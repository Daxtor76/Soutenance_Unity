using UnityEngine;

public abstract class Mover : IMover
{
    protected float ForwardSpeed { get; set; }
    protected float SideSpeed { get; set; }
    protected Vector3 velocity = Vector3.zero;
    public abstract void Move(CharacterController characterController);
    public virtual void Strafe(int pDir)
    {
        switch (pDir)
        {
            case 1:
                velocity.x += SideSpeed;
                break;
            case -1:
                velocity.x -= SideSpeed;
                break;
            default:
                velocity.x = 0.0f;
                break;
        }
    }

    public void Jump(float jumpHeight)
    {
        velocity.y = jumpHeight;
    }

    public bool isGrounded(CharacterController characterController)
    {
        RaycastHit hitInfo;
        return Physics.Raycast(characterController.transform.position, Vector3.down, out hitInfo, 0.05f);
    }
}

public class SmoothGroundMover : Mover
{
    public SmoothGroundMover(float pForwardSpeed, float pSideSpeed)
    {
        ForwardSpeed = pForwardSpeed;
        SideSpeed = pSideSpeed;
        
        velocity.z = ForwardSpeed;
    }
    public override void Move(CharacterController characterController)
    {
        if (isGrounded(characterController) && velocity.y < 0f)
            velocity.y = 0f;
        
        characterController.Move(velocity * Time.deltaTime);
        
        velocity.x = 0.0f;
        velocity.y += Const.GRAVITY * Time.deltaTime;
    }
}
