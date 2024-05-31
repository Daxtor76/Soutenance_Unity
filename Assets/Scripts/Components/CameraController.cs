using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    private Transform _target;
    private Actor _character;
    private Camera _camera;

    private Vector3 _currentVelocity;

    // FOV acceleration effect
    private bool _canInterpolateFOV = false;
    private float _kyubiFOVTransitionSpeed = 3.0f;
    private int _kyubiFOVTransitionSign = -1;
    private float _cameraMinFOV = 60.0f;
    private float _cameraMaxFOV = 70.0f;
    private float _FOVInterpolator = 0.0f;

    // Position acceleration effect
    private Transform _normalDummy;
    private Transform _kyubiDummy;
    private float _kyubiPosTransitionSpeed = 0.3f;

    // Rotation Strafe effect
    float rotator = 0.0f;
    private float _minRotation = -2.5f;
    private float _maxRotation = 2.5f;
    private float _rotationSpeed = 5.0f;
    private float _backRotationSpeed = 15.0f;
    private Transform _destination;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        _character = transform.parent.transform.parent.gameObject.GetComponent<Actor>();
        _target = _character.transform.Find(Const.CAMERA_TARGET_DUMMY);
        _normalDummy = _character.transform.Find(Const.CAMERA_NORMAL_DUMMY);
        _kyubiDummy = _character.transform.Find(Const.CAMERA_KYUBI_DUMMY);

        _destination = _normalDummy;

        _character.StateController.OnStateChange.AddListener(AdaptFromStateChange);
    }

    // Update is called once per frame
    void Update()
    {
        LookAtTargetDummy();

        // Increase FOV when in kyubi state
        if (_canInterpolateFOV)
        {
            _FOVInterpolator += _kyubiFOVTransitionSpeed * Time.deltaTime * _kyubiFOVTransitionSign;
            _FOVInterpolator = Mathf.Clamp(_FOVInterpolator, 0.0f, 1.0f);
            _camera.fieldOfView = Mathf.Lerp(_cameraMinFOV, _cameraMaxFOV, _FOVInterpolator);

            if (_camera.fieldOfView == _cameraMinFOV || _camera.fieldOfView == _cameraMaxFOV)
                _canInterpolateFOV = false;
        }
        // Change position when in kyubi state
        Vector3 newPosition = Vector3.SmoothDamp(transform.parent.position, _destination.position, ref _currentVelocity, _kyubiPosTransitionSpeed);
        transform.parent.position = newPosition;

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
            _destination = _kyubiDummy;
        }
        else
        {
            _kyubiFOVTransitionSign = -1;
            _canInterpolateFOV = true;
            _destination = _normalDummy;
        }
    }

    private void LookAtTargetDummy()
    {
        transform.LookAt(_target);
    }
}
