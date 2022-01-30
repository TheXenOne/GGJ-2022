using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Chonkfactory : MonoBehaviour
{
    public int MaxChonkas;
    public int Chonk;
    public Vector2 ScaleAtMaxChonk;

    [HideInInspector]
    public int InitialChonk = 0;

    [HideInInspector]
    public Action _winDelegate;

    Vector2 startScale;
    // Start is called before the first frame update
    void Start()
    {
        startScale = transform.localScale;
        InitialChonk = Chonk;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(startScale, ScaleAtMaxChonk, Chonk / (float)MaxChonkas);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject incomiiiiiiinnnnggg = col.gameObject;
        if(incomiiiiiiinnnnggg)
        {
            Chonkfactory bigberthaScale = incomiiiiiiinnnnggg.GetComponent<Chonkfactory>();
            if(bigberthaScale != null && bigberthaScale.Chonk < Chonk)
            {
                Chonk++; //big yummies
                if (incomiiiiiiinnnnggg.CompareTag("Player"))
                {
                    HealthComponent lifeJuice = incomiiiiiiinnnnggg.GetComponent<HealthComponent>();
                    if (lifeJuice != null) lifeJuice.TakeDamage(10);
                }
                else if (incomiiiiiiinnnnggg.CompareTag("BigChungus"))
                {
                    Destroy(incomiiiiiiinnnnggg);
                    if (_winDelegate != null)
                        _winDelegate();
                }
                else
                    Destroy(incomiiiiiiinnnnggg);
            }
        }
    }
}
