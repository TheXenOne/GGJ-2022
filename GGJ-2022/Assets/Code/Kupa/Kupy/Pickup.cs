using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Pickup : MonoBehaviour
{
    public Vector2 _randomChonkRange = new Vector2(1, 5);

    private void Awake()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<LandController>() != null)
        {
            var chonk = collision.gameObject.GetComponent<Chonkfactory>();

            chonk.Chonk += Mathf.RoundToInt(Random.Range(_randomChonkRange.x, _randomChonkRange.y));

            Destroy(transform.GetChild(0).gameObject);
            Destroy(gameObject);
        }
    }
}
