using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LandController : MonoBehaviour
{
    public bool _startWithControl = true;
    public float _moveSpeed = 2f;
    public float _controlTransitionTime = 0.7f;
    public float _transitionDrag = 5f;
    public float _rotationSpeed = 5f;

    [HideInInspector]
    public bool _hasControl = false;
    [HideInInspector]
    public bool _isTransitioning = false;
    [HideInInspector]
    public Vector2 _movement;

    VehicleController _vehicleController;
    Rigidbody2D _rigidbody;
    float _transitionTimer = 0f;
    HealthComponent _playerHealth;

    public void TakeControl()
    {
        _hasControl = true;
        _vehicleController._hasControl = false;
        _vehicleController._isTransitioning = false;
        _vehicleController._particleTrail.SetActive(false);
        _isTransitioning = true;
        _transitionTimer = 0f;
        _rigidbody.drag = _transitionDrag;
        _playerHealth._immune = true;
    }

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _vehicleController = GetComponent<VehicleController>();
        _playerHealth = GetComponent<HealthComponent>();

        if (_startWithControl)
        {
            TakeControl();
        }
    }

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
                }
            }
            else
            {
                _movement.x = Input.GetAxis("Horizontal");
                _movement.y = Input.GetAxis("Vertical");
                _movement.Normalize();
            }
        }
    }

    void FixedUpdate()
    {
        if (_hasControl)
        {
            if (!_isTransitioning)
            {
                _rigidbody.MovePosition(_rigidbody.position + _movement * _moveSpeed * Time.fixedDeltaTime);

                if (_movement.sqrMagnitude > 0f)
                {
                    float targetAngle = (Mathf.Atan2(-_movement.x, _movement.y)) * Mathf.Rad2Deg;

                    _rigidbody.MoveRotation(Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(targetAngle, Vector3.forward), _rotationSpeed * Time.fixedDeltaTime));
                }
            }
        }
    }
}
