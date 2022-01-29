using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{
    public float _maxHealth = 100f;
    public float _currentHealth = 100f;

    public GameObject _damageParticlePrefab;
    public Slider _healthUISlider;

    public void AddHealth(float healthToAdd)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + healthToAdd, 0f, _maxHealth);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0f, _maxHealth);

        if (_damageParticlePrefab != null)
        {
            Instantiate(_damageParticlePrefab, transform.position, Quaternion.identity);
        }
    }

    public void AddMaxHealth(float healthToAdd)
    {
        _maxHealth = Mathf.Clamp(_maxHealth + healthToAdd, 0f, float.MaxValue);
    }

    public void ReduceMaxHealth(float healthToReduce)
    {
        _maxHealth = Mathf.Clamp(_maxHealth - healthToReduce, 1f, float.MaxValue);
    }

    private void Update()
    {
        if (_healthUISlider != null)
        {
            _healthUISlider.value = _currentHealth / _maxHealth;
        }
    }
}
