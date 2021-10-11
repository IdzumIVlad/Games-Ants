using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthSlider : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField]
    private Slider slider;
    [SerializeField]
    private float updateSpeedSeconds = 0.5f;
    [SerializeField]
    private float positionOffset;

    private EnemyHealth enemyHealth;
    Mover mover;
    Image image;
    public GameObject childrenImage;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mover = FindObjectOfType<Mover>();
        image = GetComponent<Image>();
    }

    public void SetHealth(EnemyHealth health)
    {
        this.enemyHealth = health;
        health.OnHealthSliderPctChanged += HandleHealthChanged;
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
            slider.enabled = false;
            image.enabled = false;
            childrenImage.SetActive(false);
        }
        else
        {
            slider.enabled = true;
            image.enabled = true;
            childrenImage.SetActive(true);
        }

    }

    private void LateUpdate()
    {
        transform.position = mainCamera.WorldToScreenPoint(enemyHealth.transform.position + Vector3.up * positionOffset); //it`s slow можно ли прокидывать вручную?
    }

    private void OnDestroy()
    {
        {
            enemyHealth.OnHealthSliderPctChanged -= HandleHealthChanged;
        }
    }
}
