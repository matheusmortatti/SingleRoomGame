using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float WalkSpeed = 5.0f;
    [SerializeField]
    private float LookSensitivity = 5.0f;

    private PlayerMotor motor;

    private float _xMov, _zMov, _xRot, _yRot;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * WalkSpeed;

        Vector3 _rotation = new Vector3(-_xRot, _yRot, 0) * LookSensitivity;

        motor.ApplyVelocity(_velocity);
        motor.ApplyRotation(_rotation);
    }

    public void ApplyVerticalMovement(float val)
    {
        _zMov = val;
    }

    public void ApplyHorizontalMovement(float val)
    {
        _xMov = val;
    }

    public void ApplyLookX(float val)
    {
        _yRot = val;
    }

    public void ApplyLookY(float val)
    {
        _xRot = val;
    }

    public void Interact(bool buttonValue)
    {
        // Interact with objects
        if (buttonValue)
            Debug.Log(buttonValue);
    }
}
