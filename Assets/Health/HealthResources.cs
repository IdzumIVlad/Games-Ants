using System;
using UnityEngine;

public class HealthResources : MonoBehaviour
{
    public static event Action<HealthResources> OnHealthSliderAdded = delegate { };
    public static event Action<HealthResources> OnHealthSliderRemoved = delegate { };

    public float fullHealth = 10;
    public float CurrentHealth { get; private set; }

    public event Action<float> OnHealthSliderPctChanged = delegate { };

    private void Start()
    {
        CurrentHealth = fullHealth;
        OnHealthSliderAdded(this);
    }

    private void OnEnable()
    {
        CurrentHealth = fullHealth;
        OnHealthSliderAdded(this);
    }

    public void ModifyHealth(int amount)
    {
        CurrentHealth += amount;
        float currentHealthPct = CurrentHealth / fullHealth;
        OnHealthSliderPctChanged(currentHealthPct);
    }

    private void OnDisable()
    {
        OnHealthSliderRemoved(this);
    }
}
