using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    
    public void SetHealth(float healthPercentage)
    {
        _healthBar.value = healthPercentage;
    }
   
}
