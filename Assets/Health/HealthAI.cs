using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class HealthAI : MonoBehaviour
{
    public static event Action<HealthAI> OnHealthAdded = delegate { };
    public static event Action<HealthAI> OnHealthRemoved = delegate { };


    public float fullHealth = 10;
    [SerializeField] public float currentHealth;

    public event Action<float> OnHealthPctChanged = delegate { };
    public float increaseHealth = 0.02f;
    public bool isLive;
    Animator animator;

    private void OnEnable()
    {
        isLive = true;
        currentHealth = fullHealth;
        OnHealthAdded(this);
        animator = GetComponent<Animator>();
    }

    public void ModifyHealth(int amount, Enemy enemy)
    {
        currentHealth += amount;
        float currentHealthPct = currentHealth / fullHealth;
        OnHealthPctChanged(currentHealthPct);
        if (currentHealth <= 0f)
        {
            isLive = false;
            animator.SetBool("Death", true);
            Death();
        }
            
    }

    void Death()
    {
        animator.SetBool("Death", false);
        Destroy(gameObject); //можно через пул объектов, котиорый будет изменяться от вместимости муравейника
    }

    private void OnDisable()
    {
        animator.SetBool("Death", false);
        OnHealthRemoved(this);
    }
}
