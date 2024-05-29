using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDispersor : MonoBehaviour
{
    private void Start()
    {
        foreach (Transform child in transform)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(0.0f, 0.2f),
                0.0f,
                Random.Range(0.0f, 0.2f));

            child.transform.position += randomOffset;
        }
    }
}
