using NaughtyAttributes;
using System;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] bool _willDestroyOnDeath = false;
    [SerializeField, ShowIf("_willDestroyOnDeath")] GameObject _gameObjectToDestroy;
    [field: SerializeField, Min(0)] public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }
    public bool IsDead => CurrentHealth <= 0;

    public event Action OnHealthUpdate;
    public UnityEvent OnDeath;

    public Health(int maxHealth, int currentHealth = 0) // default as to be maxHealth
    {
        MaxHealth = maxHealth;
        CurrentHealth = currentHealth;

        if (currentHealth < 0)
        {
            throw new UnityEngine.Assertions.AssertionException("Can't create Health with negative CurrentHealth", "!!!");
        }

        if (currentHealth > maxHealth)
        {
            throw new UnityEngine.Assertions.AssertionException("Can't create Health with higher CurrentHealth than MaxHealth", "!!!");
        }

        if (maxHealth <= 0)
        {
            throw new UnityEngine.Assertions.AssertionException("MaxHealth has to be non zero positive", "!!!");
        }
    }

    [Button]
    public void DamageTest()
    {
        TakeDamage(10);
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void Heal(int value)
    {
        if (value > 0)
        {
            CurrentHealth += value;
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
            OnHealthUpdate.Invoke();
        }
        else if (value == 0)
        {
            return;
        }
        else
        {
            throw new UnityEngine.Assertions.AssertionException("Heal value cannot be negative", "!!!");
        }
    }

    public void TakeDamage(int value)
    {
        if (value > 0)
        {
            CurrentHealth -= value;
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                Death();
            }
            OnHealthUpdate?.Invoke();
        }
        else if (value == 0)
        {
            return;
        }
        else
        {
            throw new UnityEngine.Assertions.AssertionException("Damage value cannot be negative", "!!!");
        }
    }

    void Death()
    {
        OnDeath?.Invoke();
        if (_willDestroyOnDeath) Destroy(_gameObjectToDestroy);
    }
}