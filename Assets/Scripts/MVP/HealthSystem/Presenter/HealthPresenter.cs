using UnityEngine;

public class HealthPresenter : MonoBehaviour, IDamageable
{
    [SerializeField] private HealthView _view;
    [SerializeField] private int _initialHealth = 100;
    private HealthModel _model;

    private void Start()
    {
        _model = new HealthModel(_initialHealth);
        _model.OnHealthChanged += UpdateHealthBar;
        _model.OnDeath += HandleDeath;

        UpdateHealthBar(_model.CurrentHealth);
    }

    private void UpdateHealthBar(int currentHealth)
    {
        var healthPercentage = (float)currentHealth / _model.MaxHealth;
        _view.SetHealth(healthPercentage);
    }

    private void HandleDeath()
    {
        Debug.Log("Player has died.");
    }

    // Example method to simulate taking damage
    public void TakeDamage(int damage)
    {
        _model.TakeDamage(damage);
    }
}

public interface IDamageable
{
    void TakeDamage(int damage);
}