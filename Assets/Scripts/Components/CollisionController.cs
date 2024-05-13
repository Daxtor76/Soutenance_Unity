using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class CollisionController : MonoBehaviour
{
    public UnityEvent<GameObject> CollisionWithTriggerHappened = new UnityEvent<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{gameObject.name} collided with trigger {other.gameObject.name}");
        CollisionWithTriggerHappened?.Invoke(other.gameObject);
    }
}