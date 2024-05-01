public interface IMover
{
    void Move(Character character);
    void Strafe(Character character, int pDir);
    void Jump(float jumpHeight);
    bool IsGrounded(Character character);
}
