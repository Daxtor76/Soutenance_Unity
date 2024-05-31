using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbController : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(transform.parent.up, 200.0f * Time.deltaTime);
    }
}
