using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LandController : MonoBehaviour
{
    public bool _startWithControl = true;
    public float _moveSpeed = 5f;

    [HideInInspector]
    public bool _hasControl = false;

    VehicleController _vehicleController;
    Rigidbody2D _rigidbody;
    Vector2 _movement;

    public void TakeControl()
    {
        _hasControl = true;
        _vehicleController._hasControl = false;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        _rigidbody.MoveRotation(0f);
    }

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _vehicleController = GetComponent<VehicleController>();

        if (_startWithControl)
        {
            TakeControl();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_hasControl)
        {
            _movement.x = Input.GetAxis("Horizontal");
            _movement.y = Input.GetAxis("Vertical");
        }
    }

    void FixedUpdate()
    {
        if (_hasControl)
        {
            _rigidbody.MovePosition(_rigidbody.position + _movement * _moveSpeed * Time.fixedDeltaTime);
        }
    }
}
