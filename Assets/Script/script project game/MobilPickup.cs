using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentbreakForce;
    private bool isBreaking;

    //setting
    [SerializeField] private float motorForce, breakForce, maxSteerAngle;

    //wheelcollider
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    //wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    [Header("Android UI Controls")]
    [SerializeField] private TombolInput tombolGas;
    [SerializeField] private TombolInput tombolRem;
    [SerializeField] private TombolInput tombolKiri;
    [SerializeField] private TombolInput tombolKanan;
    [SerializeField] private TombolInput tombolHandbrake;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        float keyboardHorizontal = Input.GetAxis("Horizontal");
        float keyboardVertical = Input.GetAxis("Vertical");
        bool keyboardBreaking = Input.GetKey(KeyCode.Space);

        float mobileVertical = 0f;
        float mobileHorizontal = 0f;
        bool mobileBreaking = false;

        if (tombolGas != null && tombolGas.sedangDitekan) mobileVertical = 1f;
        else if (tombolRem != null && tombolRem.sedangDitekan) mobileVertical = -1f;

        if (tombolKanan != null && tombolKanan.sedangDitekan) mobileHorizontal = 1f;
        else if (tombolKiri != null && tombolKiri.sedangDitekan) mobileHorizontal = -1f;

        if (tombolHandbrake != null && tombolHandbrake.sedangDitekan) mobileBreaking = true;

        horizontalInput = (keyboardHorizontal != 0) ? keyboardHorizontal : mobileHorizontal;
        verticalInput = (keyboardVertical != 0) ? keyboardVertical : mobileVertical;
        isBreaking = keyboardBreaking || mobileBreaking;
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateWheelPos(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPos(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPos(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheelPos(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateWheelPos(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}