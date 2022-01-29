using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCamera : MonoBehaviour
{
    public float _trackingSpeed = 5f;
    public GameObject _trackedObject;

    // Update is called once per frame
    void Update()
    {
        if (_trackedObject != null)
        {
            Vector2 newPos = Vector3.Lerp(transform.position, _trackedObject.transform.position, Time.deltaTime * _trackingSpeed);
            transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        }
    }
}
