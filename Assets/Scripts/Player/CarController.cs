using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

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

    public Image blackImage;
    public Camera Main;
    public Camera FirstAnimation; 
    public Camera Hastaganimation;

    public GameObject HUD;

    public Settings settings;

    private float max_speed = 150;

    public string CarSpeed;
    private float Speed;
    private Vector3 startingPosition, speedvec;

    private void Start()
    {
        Main.gameObject.SetActive(true);
        FirstAnimation.gameObject.SetActive(false);
        Hastaganimation.gameObject.SetActive(false);
        HUD.SetActive(true);

        StartCoroutine(SwingRandomly());
        blackImage.gameObject.SetActive(false);
        startingPosition = transform.position;
        promille = StoredVariables.Promille;
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
    
    IEnumerator SetRandomSteeringAngle()
    {
        yield return new WaitForSeconds(.8f);
        _steerAngle = UnityEngine.Random.Range(10, 90);
        StartCoroutine(SetRandomSteeringAngle());
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Crashed());
    }

    IEnumerator SwingRandomly()
    {
        var L = -1;
        var R = 1;
        var turnLeftForce = L * promille;
        var turnRightForce = R * promille;

        yield return new WaitForSeconds(UnityEngine.Random.Range(0.8f, 2.8f));
        var rot = transform.rotation;
        var newrot = rot * Quaternion.Euler(0, UnityEngine.Random.Range(turnLeftForce, turnRightForce), 0);
        transform.rotation = newrot;

        StartCoroutine(SwingRandomly());
    }

    IEnumerator Crashed()
    {
        blackImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        blackImage.gameObject.SetActive(false);
        Main.gameObject.SetActive(false);
        HUD.SetActive(false);
        if (!settings.HasCrashed)
        {
            FirstAnimation.gameObject.SetActive(true);
            yield return new WaitForSeconds(10);
            FirstAnimation.gameObject.SetActive(false);
            Hastaganimation.gameObject.SetActive(true);
            yield return new WaitForSeconds(3);
            settings.HasCrashed = true;
            settings.SaveSettings();
            Debug.Log("settings.HasCrashed = " + settings.HasCrashed);
            SceneManager.LoadScene(1);
        }
            
        if (settings.HasCrashed)
        {
            Hastaganimation.gameObject.SetActive(true);
            yield return new WaitForSeconds(3);
            settings.HasCrashed = true;
            settings.SaveSettings();
            SceneManager.LoadScene(1);
        }
            
    }
}
