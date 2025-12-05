using System;
using UnityEngine;

public class HealthModel
{
    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }
    
    public event Action OnDeath;
    public event Action<int> OnHealthChanged;
    
    public HealthModel(int maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }
    
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        OnHealthChanged?.Invoke(CurrentHealth);
        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
            OnDeath?.Invoke();
        }
           
    }
    
    
}
