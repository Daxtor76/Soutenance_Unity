using UnityEngine;

public class PatouneController : MonoBehaviour
{
    public float speed = 45.0f;
    public int value = 5;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}
