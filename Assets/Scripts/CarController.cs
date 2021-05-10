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
    public float promille;

    private bool _isBreaking;
    private bool _reverse;
    
    public Rigidbody Rb;
    public StoredVariables storedVariables;

    [SerializeField] public float motorForce;
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

    private float max_speed = 150;

    public string CarSpeed;
    private float Speed;
    private Vector3 startingPosition, speedvec;

    private float TimeBeetween;

    private void Start()
    {
        startingPosition = transform.position;
        promille = StoredVariables.Promille;
        drunk();
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        RotateWheels();


        speedvec = ((transform.position - startingPosition) / Time.deltaTime);
        Speed = (float)((int)speedvec.magnitude * 3.6);

        startingPosition = transform.position;
        if (_reverse)
            CarSpeed = "R " + Speed.ToString("f0") + "Km/h";
        else { CarSpeed = Speed.ToString("f0") + "Km/h"; }
        
    }

    private void HandleMotor()
    {

        if (Speed < max_speed)
        {
            frontLeftWheelColider.motorTorque = _verticalInput * motorForce;
            frontRightWheelColider.motorTorque = _verticalInput * motorForce;
        }
        else
        {
            backLeftWheelColider.motorTorque = 0;
            backRightWheelColider.motorTorque = 0;
        }

        if (_isBreaking)
            _currentBreakForce = _breakForce;
        if(_isBreaking)
        {
            ApplyBreaking();
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
        Quaternion rot;
        wheelColider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
    }

    public void SetDrunkTimer()
    {
        if (promille > .3)
        {
            TimeBeetween = 3f;
        }else
        {
            TimeBeetween = 4;
        }
            
        if (promille > .8)
            TimeBeetween = 1.8f;
        if (promille > 1.2)
            TimeBeetween = 1.3f;
        if (promille > 1.8)
            TimeBeetween = .8f;
        if (promille > 2.3)
            TimeBeetween = .3f;
    }

    IEnumerable drunk()
    {
        yield return new WaitForSeconds(TimeBeetween);

        drunk();
    }
}
