using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class HealthPlayer : MonoBehaviour
{
    public float fullHealth = 10f;
    public float increaseHealth = 1f;
    public bool isLive;
    public float currentHealth;

    public Transform homePoint;
    public CanvasGroup dieTableUI;
    public GameObject popUpPrefab;
    public GameObject canvas;
    public float positionOffsetY = -0.5f;
    public float positionOffsetZ = -0.5f;
    Attack attack;


    CharacterController characterController;

    public static event Action<HealthPlayer> OnHealthSliderPlayerAdded = delegate { };
    public static event Action<HealthPlayer> OnHealthSliderPlayerRemoved = delegate { };

    public event Action<float> OnHealthSliderPlayerPctChanged = delegate { };

    private void Start()
    {
        currentHealth = fullHealth;
        OnHealthSliderPlayerAdded(this);
        characterController = FindObjectOfType<CharacterController>();
        attack = GetComponent<Attack>();
    }

    private void Update()
    {
        if (currentHealth < 0)
            isLive = false;
        if(currentHealth < fullHealth && isLive)
            ModifyHealth(increaseHealth * Time.deltaTime);
    }

    private void OnEnable()
    {
        isLive = true;
        currentHealth = fullHealth;
        OnHealthSliderPlayerAdded(this);
    }

    public void ModifyHealth(float amount)
    {
        currentHealth += amount;
        if (currentHealth > fullHealth) currentHealth = fullHealth;
        ShowModifyHealthText(amount);
        float currentHealthPct = currentHealth / fullHealth;
        OnHealthSliderPlayerPctChanged(currentHealthPct);
        if (currentHealth <= 0)
            DeathPlayer();
    }

    

    private void ShowModifyHealthText(float amount)
    {
        if (Mathf.Abs(amount) < 0.5f) return;
            var popUpText = Instantiate(popUpPrefab,
                new Vector3(canvas.transform.position.x, canvas.transform.position.y + positionOffsetY, canvas.transform.position.z + positionOffsetZ),
                Quaternion.identity,
                canvas.transform);
            popUpText.GetComponent<TMP_Text>().text = amount.ToString("0.0");
    }

    public void Reborn()
    {
        characterController.gameObject.transform.position = homePoint.transform.position;
        characterController.enabled = true;
        ModifyHealth(fullHealth);
        float currentHealthPct = currentHealth / fullHealth;
        OnHealthSliderPlayerPctChanged(currentHealthPct);
        ToggleWindowWithFade();
        isLive = true;
        attack.animator.SetBool("Death", false);
    }

    void DeathPlayer()
    {
        attack.animator.SetBool("Death", true);
        attack.animator.SetBool("Attack", false);
        characterController.enabled = false;
        isLive = false;
        ToggleWindowWithFade();
    }

    public void ToggleWindowWithFade()
    {
        var canvasGroup = dieTableUI;
        canvasGroup.DOFade(canvasGroup.alpha < 1 ? 1 : 0, 0.5f).OnComplete(() =>
        {
            canvasGroup.interactable = canvasGroup.alpha == 1;
            canvasGroup.blocksRaycasts = canvasGroup.alpha == 1;
        });
    }

    private void OnDisable()
    {
        OnHealthSliderPlayerRemoved(this);
    }
}
