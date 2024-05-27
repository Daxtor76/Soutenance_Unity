using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _target;
    private Vector3 _posFromTarget;

    private Vector3 _currentVelocity;
    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.Find(Const.CHARACTER_NAME).transform.Find(Const.CHARACTER_CAMERA_DUMMY);
        _posFromTarget = transform.position - _target.position;
    }

    // Update is called once per frame
    void Update()
    {
        LookAtCharacter();
        //FollowTarget(_target);
    }

    private void FollowTarget(Transform target)
    {
        Vector3 newPosition = Vector3.SmoothDamp(transform.position, target.position + _posFromTarget, ref _currentVelocity, 0.15f);
        transform.position = newPosition;
    }

    private void LookAtCharacter()
    {
        transform.LookAt(_target);
    }
}
