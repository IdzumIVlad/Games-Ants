using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
using DG.Tweening;

public class EnemyHealth : MonoBehaviour
{
    public static event Action<EnemyHealth> OnHealthSliderEnemyAdded = delegate { };
    public static event Action<EnemyHealth> OnHealthSliderEnemyRemoved = delegate { };
    
    public float fullHealth = 10;
    public float currentHealth;
    public event Action<float> OnHealthSliderPctChanged = delegate { };

    NavMeshAgent navMeshAgent;
    Expirience expirience;
    public int expAward = 5;
    public GameObject popUpPrefab;
    public GameObject canvas;
    public Camera mainCamera;
    Enemy enemy;
    public bool onReborn;

    private void Start()
    {
        currentHealth = fullHealth;
        OnHealthSliderEnemyAdded(this);
        expirience = FindObjectOfType<Expirience>();
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
        onReborn = false;
    }

    private void OnEnable()
    {
        currentHealth = fullHealth;
        OnHealthSliderEnemyAdded(this);
    }

    private void Update()
    {
        //float currentHealthPct = currentHealth / fullHealth;
        //OnHealthSliderPctChanged(currentHealthPct);

    }

    public void ModifyHealth(float amount)
    {
        currentHealth += amount;
        if (currentHealth > fullHealth) currentHealth = fullHealth;
        ShowModifyHealthText(amount);
        float currentHealthPct = currentHealth / fullHealth;
        OnHealthSliderPctChanged(currentHealthPct);
        if (currentHealth <= 0)
            DeathEnemy();
    }

    private void ShowModifyHealthText(float amount)
    {
        var popUpText = Instantiate(popUpPrefab, transform.position, Quaternion.identity, canvas.transform);
        popUpText.GetComponent<TMP_Text>().text = amount.ToString("0.0");
    }

    private void OnDisable()
    {
        OnHealthSliderEnemyRemoved(this);
    }

    void DeathEnemy()
    {

        if (onReborn) return;
        enemy.animator.SetBool("Attack", false);
        enemy.animator.SetBool("Death", true);
        expirience.exp += expAward;
        expAward += 2;
        navMeshAgent.ResetPath();
        onReborn = true;
        DOVirtual.DelayedCall(2, BornNewEnemy);
        

    }

    void BornNewEnemy()
    {
        navMeshAgent.Warp(new Vector3(gameObject.transform.position.x + UnityEngine.Random.Range(-30f, 30f),
            gameObject.transform.position.y,
            gameObject.transform.position.z + UnityEngine.Random.Range(-30f, 30f)));
        enemy.animator.SetBool("Death", false);
        fullHealth += 2.5f;
        ModifyHealth(fullHealth*2);
        float currentHealthPct = currentHealth / fullHealth;
        OnHealthSliderPctChanged(currentHealthPct);
        onReborn = false;
    }
}
