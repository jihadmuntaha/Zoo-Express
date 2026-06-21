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

    // --- TAMBAHAN VARIABEL UNTUK INPUT ANDROID ---
    [Header("Android UI Controls")]
    [SerializeField] private TombolInput tombolGas;
    [SerializeField] private TombolInput tombolRem;
    [SerializeField] private TombolInput tombolKiri;
    [SerializeField] private TombolInput tombolKanan;
    [SerializeField] private TombolInput tombolHandbrake; // Opsional untuk tombol rem tangan di HP

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        // 1. Ambil input dari Keyboard bawaan PC terlebih dahulu
        float keyboardHorizontal = Input.GetAxis("Horizontal");
        float keyboardVertical = Input.GetAxis("Vertical");
        bool keyboardBreaking = Input.GetKey(KeyCode.Space);

        // 2. Siapkan penampung untuk input dari Tombol HP Android
        float mobileVertical = 0f;
        float mobileHorizontal = 0f;
        bool mobileBreaking = false;

        // Cek tekanan tombol gas/rem di HP
        if (tombolGas != null && tombolGas.sedangDitekan) mobileVertical = 1f;
        else if (tombolRem != null && tombolRem.sedangDitekan) mobileVertical = -1f;

        // Cek tekanan tombol belok kanan/kiri di HP
        if (tombolKanan != null && tombolKanan.sedangDitekan) mobileHorizontal = 1f;
        else if (tombolKiri != null && tombolKiri.sedangDitekan) mobileHorizontal = -1f;

        // Cek rem tangan di HP (jika dipasang tombolnya)
        if (tombolHandbrake != null && tombolHandbrake.sedangDitekan) mobileBreaking = true;

        // 3. Gabungkan: Jika keyboard tidak ditekan (0), gunakan input dari tombol HP
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