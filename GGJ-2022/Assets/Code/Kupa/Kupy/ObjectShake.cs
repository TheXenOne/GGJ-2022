using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShake : MonoBehaviour
{

	private Vector3 _originPosition;
	private Quaternion _originRotation;
	public float _shakeDecay = 0.002f;
	public float _shakeIntensity = .3f;
	public bool _shakeOnStart = true;

	private float _currentShakeIntensity = 0;

    void Start()
    {
        if (_shakeOnStart)
        {
			Shake();
        }
    }

    void Update()
	{
		if (_currentShakeIntensity > 0)
		{
			transform.position = _originPosition + Random.insideUnitSphere * _currentShakeIntensity;
			transform.rotation = new Quaternion(
				_originRotation.x + Random.Range(-_currentShakeIntensity, _currentShakeIntensity) * .2f,
				_originRotation.y + Random.Range(-_currentShakeIntensity, _currentShakeIntensity) * .2f,
				_originRotation.z + Random.Range(-_currentShakeIntensity, _currentShakeIntensity) * .2f,
				_originRotation.w + Random.Range(-_currentShakeIntensity, _currentShakeIntensity) * .2f);
			_currentShakeIntensity -= _shakeDecay * Time.deltaTime;
		}
	}

	void Shake()
	{
		_originPosition = transform.position;
		_originRotation = transform.rotation;
		_currentShakeIntensity = _shakeIntensity;
	}
}