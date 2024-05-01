using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class CollisionController : MonoBehaviour
{
    public Collider Collider { get; private set; }
    public UnityEvent<GameObject> CollisionHappened = new UnityEvent<GameObject>();
    private void Awake()
    {
        Collider = GetCollider();
    }

    private void Start()
    {
        gameObject.GetComponent<StateController>().StateChanged.AddListener(OnStateChanged);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{gameObject.name} collided {other.gameObject.name}");
        CollisionHappened.Invoke(other.gameObject);
    }

    private void OnStateChanged(IState newState)
    {
    }

    private BoxCollider GetCollider()
    {
        BoxCollider collider = gameObject.GetComponentInChildren<BoxCollider>();
        return collider != null ? collider : null;
    }
}