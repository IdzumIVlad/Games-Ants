using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBarPrefab;

    private Dictionary<HealthAI, HealthBar> healthBars = new Dictionary<HealthAI, HealthBar>();
    public GameObject children;

    private void Awake()
    {
        HealthAI.OnHealthAdded += AddHealthBar;
        HealthAI.OnHealthRemoved += RemoveHealthBar;
    }

    private void AddHealthBar(HealthAI health)
    {
        if(healthBars.ContainsKey(health) == false)
        {
            var healthBar = Instantiate(healthBarPrefab, children.transform);
            healthBars.Add(health, healthBar);
            healthBar.SetHealth(health);
        }
    }

    private void RemoveHealthBar(HealthAI health)
    {
        if (healthBars.ContainsKey(health))
        {
            Destroy(healthBars[health].gameObject);
            healthBars.Remove(health);
        }
    }

}
