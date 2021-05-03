using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float _horozontalInput;
    private float _verticalInput;
    private float _steerAngle;
    private float _currentBreakForce;

    private bool _isBreaking;

    public ParticleSystem smoke;

    [SerializeField] private float motorForce;
    [SerializeField] private float _breakForce;
    [SerializeField] private float maxSteeringAngle;

    [SerializeField] private WheelCollider frontLeftWheelColider;
    [SerializeField] private WheelCollider frontRightWheelColider;
    [SerializeField] private WheelCollider backLeftWheelColider;
    [SerializeField] private WheelCollider backRightWheelColider;

    [SerializeField] private Transform frontLeftWheel;
    [SerializeField] private Transform frontRightWheel;
    [SerializeField] private Transform backLeftWheel;
    [SerializeField] private Transform backRightWheel;

    private void Update()
    {
        if (Input.GetButtonDown(VERTICAL))
        {
            smoke.Play();
        }
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        //RotateWheels();
    }

    private void HandleMotor()
    {
        frontLeftWheelColider.motorTorque = _verticalInput * motorForce;
        frontRightWheelColider.motorTorque = _verticalInput * motorForce;
        if (_isBreaking)
            _currentBreakForce = _breakForce;
        if(_isBreaking)
        {
            ApplyBreaking();
            smoke.Stop();
        }else
        {
            RemoveBreaking();
        }
    }

    private void RemoveBreaking()
    {
        frontLeftWheelColider.brakeTorque = 0;
        frontRightWheelColider.brakeTorque = 0;
        backLeftWheelColider.brakeTorque = 0;
        backRightWheelColider.brakeTorque = 0;
    }

    private void ApplyBreaking()
    {
        frontLeftWheelColider.brakeTorque = _currentBreakForce;
        frontRightWheelColider.brakeTorque = _currentBreakForce;
        backLeftWheelColider.brakeTorque = _currentBreakForce;
        backRightWheelColider.brakeTorque = _currentBreakForce;
    }

    private void HandleSteering()
    {
        _steerAngle = maxSteeringAngle * _horozontalInput;
        frontLeftWheelColider.steerAngle = _steerAngle;
        frontRightWheelColider.steerAngle = _steerAngle;
    }

    private void GetInput()
    {
        _horozontalInput = Input.GetAxis(HORIZONTAL);
        _verticalInput = Input.GetAxis(VERTICAL);
        _isBreaking = Input.GetKey(KeyCode.Space);
    }
    /*
    private void RotateWheels()
    {
        UpdateSingleWheel(frontLeftWheelColider, frontLeftWheel);
        UpdateSingleWheel(frontRightWheelColider, frontRightWheel);
        UpdateSingleWheel(backLeftWheelColider, backRightWheel);
        UpdateSingleWheel(backRightWheelColider, backLeftWheel);
    }

    private void UpdateSingleWheel(WheelCollider wheelColider, Transform wheelTransform)
    {
        Vector3 pos;
        wheelColider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
    }*/
}
