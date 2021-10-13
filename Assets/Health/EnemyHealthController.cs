using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField]
    private EnemyHealthSlider healthSliderPrefab;
    private Dictionary<EnemyHealth, EnemyHealthSlider> healthSliders = new Dictionary<EnemyHealth, EnemyHealthSlider>();
    public GameObject children;


    private void Awake()
    {
        EnemyHealth.OnHealthSliderEnemyAdded += AddHealthBar;
        //HealthResources.OnHealthSliderRemoved += RemoveHealthBar;

    }

    private void AddHealthBar(EnemyHealth health)
    {
        if (healthSliders.ContainsKey(health) == false)
        {
            EnemyHealthSlider healthBar = Instantiate(healthSliderPrefab, children.transform);
            healthSliders.Add(health, healthBar);
            healthBar.SetHealth(health);
        }
    }
}
