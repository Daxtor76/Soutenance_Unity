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

    // FOV acceleration effect
    private bool _canInterpolateFOV = false;
    private float _kyubiFOVTransitionSpeed = 3f;
    private int _kyubiFOVTransitionSign = -1;
    private float _cameraMinFOV = 60.0f;
    private float _cameraMaxFOV = 70.0f;
    private float _FOVInterpolator = 0.0f;

    // Rotation Strafe effect
    float rotator = 0.0f;
    private float _minRotation = -2.5f;
    private float _maxRotation = 2.5f;
    private float _rotationSpeed = 5.0f;
    private float _backRotationSpeed = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.Find(Const.CHARACTER_NAME).transform.Find(Const.CHARACTER_CAMERA_DUMMY);
        _character = transform.parent.transform.parent.gameObject.GetComponent<Actor>();
        _camera = GetComponent<Camera>();
        _posFromTarget = transform.position - _target.position;
        _character.StateController.OnStateChange.AddListener(AdaptFromStateChange);
    }

    // Update is called once per frame
    void Update()
    {
        LookAtCharacter();

        // Increase FOV when in kyubi state
        if (_canInterpolateFOV)
        {
            _FOVInterpolator += _kyubiFOVTransitionSpeed * Time.deltaTime * _kyubiFOVTransitionSign;
            _FOVInterpolator = Mathf.Clamp(_FOVInterpolator, 0.0f, 1.0f);
            _camera.fieldOfView = Mathf.Lerp(_cameraMinFOV, _cameraMaxFOV, _FOVInterpolator);

            if (_camera.fieldOfView == _cameraMinFOV || _camera.fieldOfView == _cameraMaxFOV)
                _canInterpolateFOV = false;
        }

        // Rotate when strafe
        if (_character.StateController.CurrentState != Actor.States.idle && _character.StateController.CurrentState != Actor.States.dead)
        {
            rotator += _rotationSpeed * Time.deltaTime * -Input.GetAxisRaw(Const.STRAFE_AXIS_NAME);
            rotator = Mathf.Clamp(rotator, _minRotation, _maxRotation);
            if (Input.GetAxisRaw(Const.STRAFE_AXIS_NAME) == 0.0f)
            {
                if (rotator < 0.0f)
                    rotator += _backRotationSpeed * Time.deltaTime;
                else if (rotator > 0.0f)
                    rotator -= _backRotationSpeed * Time.deltaTime;
                else
                    rotator = 0.0f;
            }
            transform.parent.rotation *= Quaternion.Euler(0.0f, 0.0f, rotator);
        }
    }

    private void AdaptFromStateChange(Actor.States newState)
    {
        if (newState == Actor.States.kyubi)
        {
            _kyubiFOVTransitionSign = 1;
            _canInterpolateFOV = true;
        }
        else
        {
            _kyubiFOVTransitionSign = -1;
            _canInterpolateFOV = true;
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
