using System;
using UnityEngine;

public class PatouneController : MonoBehaviour
{
    public float rotationSpeed = 45.0f;
    public int scoreValue = 5;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
