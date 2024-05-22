using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomRotator : MonoBehaviour
{
    private void Start()
    {
        foreach (Transform child in transform)
        {
            float randomAngle = Random.Range(0.0f, 360.0f);
            child.Rotate(new Vector3(
                0.0f,
                randomAngle,
                0.0f));
        }
    }
}
