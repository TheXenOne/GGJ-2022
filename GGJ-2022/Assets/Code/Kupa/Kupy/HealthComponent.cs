using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthComponent : MonoBehaviour
{
    public float baseHealth = 100f;
    public float _maxHealth = 10000f;

    [HideInInspector]
    public float _currentHealth = 0.0f;

    public GameObject _damageParticlePrefab;
    public GameObject _deathParticlePrefab;
    public Slider _healthUISlider;

    [HideInInspector]
    public bool _isDead = false;
    [HideInInspector]
    public Action _deathDelegate;
    [HideInInspector]
    public bool _immune = false;

    SealAudi _sealAudio = null;

    public void Start()
    {
        _sealAudio = GetComponent<SealAudi>();
        _currentHealth = baseHealth;
    }

    public void AddHealth(float healthToAdd)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + healthToAdd, 0f, _maxHealth);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0f, _maxHealth);

        _sealAudio?.Damaged();

        if (_currentHealth <= 0f)
        {
            _isDead = true;
            OnDeath();
        }

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

    public void OnDeath()
    {
        if (_deathParticlePrefab != null)
        {
            Instantiate(_deathParticlePrefab, transform.position, Quaternion.identity);
        }

        if (_deathDelegate != null)
        {
            _deathDelegate();
        }
    }

    private void Update()
    {
        if (_healthUISlider != null && gameObject.CompareTag("Player"))
        {
            _healthUISlider.value = _currentHealth / _maxHealth;
        }
    }
}
