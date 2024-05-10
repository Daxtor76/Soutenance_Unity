using UnityEngine;
using UnityEngine.Events;

public class CollisionController : MonoBehaviour
{
    public UnityEvent<GameObject> CollisionHappened = new UnityEvent<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{gameObject.name} collided {other.gameObject.name}");
        CollisionHappened?.Invoke(other.gameObject);
    }
}