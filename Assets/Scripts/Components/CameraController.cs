using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _target;
    private Actor _character;
    private Camera _camera;
    private Vector3 _posFromTarget;

    private Vector3 _currentVelocity;

    private bool _canInterpolateFOV = false;
    private float _kyubiFOVTransitionSpeed = 3f;
    private int _kyubiFOVTransitionSign = 1;
    private float _cameraInitialFOV = 60.0f;
    private float _cameraMaxFOV = 70.0f;
    private float _FOVInterpolator = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.Find(Const.CHARACTER_NAME).transform.Find(Const.CHARACTER_CAMERA_DUMMY);
        _character = transform.parent.gameObject.GetComponent<Actor>();
        _camera = GetComponent<Camera>();
        _posFromTarget = transform.position - _target.position;
        _character.StateController.OnStateChange.AddListener(AdaptFromStateChange);
    }

    // Update is called once per frame
    void Update()
    {
        LookAtCharacter();
        //FollowTarget(_target);

        // Increase FOV when in kyubi state
        _camera.fieldOfView = Mathf.Lerp(_cameraInitialFOV, _cameraMaxFOV, _FOVInterpolator);
        if (_canInterpolateFOV)
        {
            _FOVInterpolator += _kyubiFOVTransitionSpeed * Time.deltaTime * _kyubiFOVTransitionSign;
            _FOVInterpolator = Mathf.Clamp(_FOVInterpolator, 0.0f, 1.0f);
        }
    }

    private void AdaptFromStateChange(Actor.States newState)
    {
        if (newState == Actor.States.kyubi)
        {
            _canInterpolateFOV = true;
            _kyubiFOVTransitionSign = 1;
        }
        else
        {
            _kyubiFOVTransitionSign = -1;
        }
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
