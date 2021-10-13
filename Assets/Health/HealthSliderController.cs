using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSliderController : MonoBehaviour
{
    [SerializeField]
    private HealthSlider healthSliderPrefab;
    private Dictionary<HealthResources, HealthSlider> healthSliders = new Dictionary<HealthResources, HealthSlider>();
    public GameObject children;

    private void Awake()
    {
        HealthResources.OnHealthSliderAdded += AddHealthBar;
        //HealthResources.OnHealthSliderRemoved += RemoveHealthBar;
        
    }

    private void AddHealthBar(HealthResources health)
    {
        if (healthSliders.ContainsKey(health) == false)
        {
            HealthSlider HealthSlider = Instantiate(healthSliderPrefab,  children.transform);
            healthSliders.Add(health, HealthSlider);
            HealthSlider.SetHealth(health);
        }
    }

    //private void RemoveHealthBar(HealthResources health)
    //{
    //    if (healthSliders.ContainsKey(health))
    //    {
    //        Destroy(healthSliders[health].gameObject);
    //        healthSliders.Remove(health);
    //    }
    //}
}
