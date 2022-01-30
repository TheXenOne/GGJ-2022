using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Chonkfactory : MonoBehaviour
{
    public int MaxChonkas;
    public int Chonk;
    public Vector2 ScaleAtMaxChonk;
    public bool _isPlayer = false;

    [HideInInspector]
    public int InitialChonk = 0;

    [HideInInspector]
    public Action _winDelegate;

    Vector2 startScale;
    static int _playerChonk = 5;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        startScale = transform.localScale;
        InitialChonk = Chonk;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPlayer)
        {
            _playerChonk = Chonk;
        }
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
                {
                    if (GetComponent<PenguinMovementController>() != null) // Peng eating peng
                    {
                        if (Chonk - bigberthaScale.Chonk < _playerChonk * 0.5f)
                        {
                            Destroy(incomiiiiiiinnnnggg);
                        }
                    }
                    else // Player eating peng
                    {
                        Destroy(incomiiiiiiinnnnggg);
                    }
                }
            }
        }
    }
}
