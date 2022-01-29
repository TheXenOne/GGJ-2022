using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drifter : MonoBehaviour
{
    public float _driftSpeed = 0.05f;
    public float _additionalRandomDrift = 0.025f;
    public Vector2 _driftDirection = new Vector2(1f, 0f);
    public Vector2 _additionalRandomDriftRange = new Vector2(0.1f, 0.1f);

    // Update is called once per frame
    void Update()
    {
        float driftSpeed = _driftSpeed + Random.Range(-_additionalRandomDrift, _additionalRandomDrift);
        Vector2 driftDir = _driftDirection +
            new Vector2(
                Random.Range(-_additionalRandomDriftRange.x, _additionalRandomDriftRange.x),
                Random.Range(-_additionalRandomDriftRange.y, _additionalRandomDriftRange.y)
                );
        Vector2 addPos = driftDir * driftSpeed * Time.deltaTime;

        transform.position += new Vector3(addPos.x, addPos.y, 0f);
    }
}
