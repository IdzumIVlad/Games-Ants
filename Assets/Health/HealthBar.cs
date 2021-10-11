using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    private Camera mainCamera;

    [SerializeField]
    private GameObject foregroundImage;
    [SerializeField]
    private float updateSpeedSeconds = 0.5f;
    [SerializeField]
    private float positionOffset;
    public GameObject children;
    private HealthAI healthAI;
    Mover mover;
    Slider slider;
    Image image;
    

    private void Start()
    {
        mainCamera = Camera.main;
        mover = FindObjectOfType<Mover>();
        image = foregroundImage.GetComponent<Image>();
        slider = foregroundImage.GetComponent<Slider>();
    }

    public void SetHealth(HealthAI health)
    {
        this.healthAI = health;
        health.OnHealthPctChanged += HandleHealthChanged;
    }

    private void HandleHealthChanged(float pct)
    {
        StartCoroutine(ChangeToPct(pct));
    }

    private IEnumerator ChangeToPct(float pct)
    {
        float preChangePct = slider.value;
        float elapsed = 0f;

        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            slider.value = Mathf.Lerp(preChangePct, pct, elapsed / updateSpeedSeconds);
            yield return null;
        }

        slider.value = pct;
    }

    private void Update()
    {
        if (mover.transform.position.y < 0)
        {
            children.SetActive(false);
            image.enabled = false;
        }
        else
        {
            children.SetActive(true);
            foregroundImage.GetComponent<Image>().enabled = true;
        }

    }

    private void LateUpdate()
    {
        transform.position = mainCamera.WorldToScreenPoint(healthAI.transform.position + Vector3.up * positionOffset); //it`s slow можно ли прокидывать вручную?
    }

    private void OnDestroy()
    {
        {
            healthAI.OnHealthPctChanged -= HandleHealthChanged;
        }
    }
}
