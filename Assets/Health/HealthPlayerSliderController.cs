using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayerSliderController : MonoBehaviour
{
    [SerializeField]
    private HealthPlayerSlider healthSliderPrefab;
    private Dictionary<HealthPlayer, HealthPlayerSlider> healthSliders = new Dictionary<HealthPlayer, HealthPlayerSlider>();
    public GameObject children;

    private void Awake()
    {
        HealthPlayer.OnHealthSliderPlayerAdded += AddHealthBar;
        //HealthResources.OnHealthSliderRemoved += RemoveHealthBar;

    }

    private void AddHealthBar(HealthPlayer health)
    {
        if (healthSliders.ContainsKey(health) == false)
        {
            HealthPlayerSlider healthBar = Instantiate(healthSliderPrefab, children.transform);
            healthSliders.Add(health, healthBar);
            healthBar.SetHealth(health);
        }
    }
}
