using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCamera : MonoBehaviour
{
    public float _trackingSpeed = 5f;
    public GameObject _trackedObject;

    Camera _cammyBoi;
    Chonkfactory _chonkers;
    float _originalCamSize = 5f;

    private void Awake()
    {
        _cammyBoi = GetComponent<Camera>();
        _originalCamSize = _cammyBoi.orthographicSize;

        _chonkers = GameObject.FindGameObjectWithTag("Player").GetComponent<Chonkfactory>();
    }

    void Update()
    {
        if (_trackedObject != null)
        {
            Vector2 newPos = Vector3.Lerp(transform.position, _trackedObject.transform.position, Time.deltaTime * _trackingSpeed);
            transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        }

        float getRatiod = _chonkers.Chonk / (float)_chonkers.MaxChonkas;
        _cammyBoi.orthographicSize = _originalCamSize * Mathf.Clamp(getRatiod * 5.0f, 1f, float.MaxValue);
    }
}
