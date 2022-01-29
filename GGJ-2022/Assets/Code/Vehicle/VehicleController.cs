using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public float _vehicleAcceleration = 18f;
    public float _vehicleTurnSpeed = 7f;
    public float _vehicleDrift = 0.5f;
    public float _vehicleMaxSpeed = 20f;
    public float _vehicleDrag = 4f;
    public float _vehicleDragLerpSpeed = 10f;

    float _accelerationInput = 0f;
    float _steeringInput = 0f;
    float _rotationAngle = 0f;
    float _velocityVsUp = 0f;

    Rigidbody2D _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _steeringInput = Input.GetAxis("Horizontal");
        _accelerationInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        ApplyEngineForce();

        ApplyDrift();

        ApplySteering();
    }

    void ApplyEngineForce()
    {
        _velocityVsUp = Vector2.Dot(transform.up, _rigidbody.velocity);

        if (_velocityVsUp > _vehicleMaxSpeed && _accelerationInput > 0f)
            return;

        if (_velocityVsUp < -_vehicleMaxSpeed * 0.5f && _accelerationInput < 0)
            return;

        if (_rigidbody.velocity.sqrMagnitude > _vehicleMaxSpeed * _vehicleMaxSpeed && _accelerationInput > 0)
            return;

        _rigidbody.drag = Mathf.Approximately(_accelerationInput, 0f) ? Mathf.Lerp(_rigidbody.drag, _vehicleDrag, Time.fixedDeltaTime * _vehicleDragLerpSpeed) : 0f;

        Vector2 engineForce = transform.up * _accelerationInput * _vehicleAcceleration;

        _rigidbody.AddForce(engineForce, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        float velocitySteerToggle = _rigidbody.velocity.magnitude / 8;
        velocitySteerToggle = Mathf.Clamp01(velocitySteerToggle);

        if (_accelerationInput < 0f)
            _steeringInput = -_steeringInput;

        _rotationAngle -= _steeringInput * _vehicleTurnSpeed * velocitySteerToggle;

        _rigidbody.MoveRotation(_rotationAngle);
    }

    void ApplyDrift()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(_rigidbody.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(_rigidbody.velocity, transform.right);

        _rigidbody.velocity = forwardVelocity + rightVelocity * _vehicleDrift;
    }
}
