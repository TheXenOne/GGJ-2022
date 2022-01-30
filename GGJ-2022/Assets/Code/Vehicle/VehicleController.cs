using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public bool _startWithControl = false;
    public float _vehicleAcceleration = 18f;
    public float _vehicleTurnSpeed = 7f;
    public float _vehicleDrift = 0.5f;
    public float _vehicleMaxSpeed = 20f;
    public float _vehicleDrag = 4f;
    public float _vehicleDragLerpSpeed = 10f;
    public float _controlTransitionTime = 0.4f;
    public float _transitionRotationSpeed = 1f;

    [HideInInspector]
    public bool _hasControl = false;
    [HideInInspector]
    public bool _isTransitioning = false;
    [HideInInspector]
    public GameObject _particleTrail;

    LandController _landController;

    float _accelerationInput = 0f;
    float _steeringInput = 0f;
    float _rotationAngle = 0f;
    float _velocityVsUp = 0f;
    float _transitionTimer = 0f;

    Rigidbody2D _rigidbody;
    HealthComponent _playerHealth;

    public void TakeControl()
    {
        _hasControl = true;
        _landController._hasControl = false;
        _landController._isTransitioning = false;
        _isTransitioning = true;
        _rigidbody.constraints = RigidbodyConstraints2D.None;
        _rigidbody.drag = 0f;
        _particleTrail.SetActive(true);
        _rotationAngle = (Mathf.Atan2(-_landController._movement.x, _landController._movement.y)) * Mathf.Rad2Deg;
    }

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _landController = GetComponent<LandController>();
        _playerHealth = GetComponent<HealthComponent>();
        _particleTrail = transform.GetChild(0).gameObject;
        _particleTrail.SetActive(false);

        if (_startWithControl)
        {
            TakeControl();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_hasControl)
        {
            if (_isTransitioning)
            {
                _transitionTimer += Time.deltaTime;
                if (_transitionTimer > _controlTransitionTime)
                {
                    _isTransitioning = false;
                    _playerHealth._immune = false;
                }
            }
            else
            {
                _steeringInput = Input.GetAxis("Horizontal");
                _accelerationInput = Input.GetAxis("Vertical");
            }
        }
    }

    void FixedUpdate()
    {
        if (_hasControl)
        {
            if (_isTransitioning)
            {
                if (_rigidbody.velocity.sqrMagnitude > 0f)
                {
                    //float velocitySteerToggle = _rigidbody.velocity.magnitude / 8;
                    //velocitySteerToggle = Mathf.Clamp01(velocitySteerToggle);

                    //if (_accelerationInput < 0f)
                    //    _steeringInput = -_steeringInput;

                    //_rotationAngle -= _steeringInput * _vehicleTurnSpeed * velocitySteerToggle;

                    //_rigidbody.MoveRotation(Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(_rotationAngle, Vector3.forward), _transitionRotationSpeed * Time.fixedDeltaTime));
                }
            }
            else
            {
                ApplyEngineForce();

                ApplyDrift();

                ApplySteering();
            }
        }
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
