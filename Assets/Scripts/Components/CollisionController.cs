using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class CollisionController : MonoBehaviour
{
    public UnityEvent<GameObject> OnCollisionWithObstacle = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> OnCollisionWithPatoune = new UnityEvent<GameObject>();
    public UnityEvent OnCollisionWithTileSpawner = new UnityEvent();
    public UnityEvent<int> OnCollisionWithRotator = new UnityEvent<int>();

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"{gameObject.name} collided with {other.gameObject.name} of tag {other.gameObject.tag}");
        if (other.gameObject.CompareTag(Const.OBSTACLE_TAG_NAME))
        {
            OnCollisionWithObstacle?.Invoke(other.gameObject);
        }
        else if (other.gameObject.CompareTag(Const.PATOUNE_TAG_NAME))
        {
            OnCollisionWithPatoune?.Invoke(other.gameObject);
        }
        else if (other.gameObject.CompareTag(Const.TILESPAWNER_TAG_NAME))
        {
            OnCollisionWithTileSpawner?.Invoke();
        }
        else if (other.gameObject.CompareTag(Const.RIGHT_ROTATOR_TAG_NAME))
        {
            OnCollisionWithRotator?.Invoke(1);
        }
        else if (other.gameObject.CompareTag(Const.LEFT_ROTATOR_TAG_NAME))
        {
            OnCollisionWithRotator?.Invoke(-1);
        }
    }
}