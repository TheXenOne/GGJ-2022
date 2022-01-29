using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chonkfactory : MonoBehaviour
{
    public int MaxChonkas;
    public int Chonk;
    public Vector2 ScaleAtMaxChonk;

    Vector2 startScale;
    // Start is called before the first frame update
    void Start()
    {
        startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(startScale, ScaleAtMaxChonk, Chonk / (float)MaxChonkas);
    }
}
