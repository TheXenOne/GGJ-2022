using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageArea : MonoBehaviour
{
    public float _damage = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthComponent HealthComp = collision.gameObject.GetComponent<HealthComponent>();
        if (HealthComp != null)
        {
            HealthComp.TakeDamage(_damage);
        }
    }
}
