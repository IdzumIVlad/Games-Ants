using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayerSlider : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField]
    private Slider slider;
    [SerializeField]
    private float updateSpeedSeconds = 0.5f;
    [SerializeField]
    private float positionOffset;

    private HealthPlayer healthPlayer;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public void SetHealth(HealthPlayer health)
    {
        this.healthPlayer = health;
        health.OnHealthSliderPlayerPctChanged += HandleHealthChanged;
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

    private void LateUpdate()
    {
            transform.position = mainCamera.WorldToScreenPoint(healthPlayer.transform.position + Vector3.up * positionOffset); //it`s slow можно ли прокидывать вручную?
    }

    private void OnDestroy()
    {
        {
            healthPlayer.OnHealthSliderPlayerPctChanged -= HandleHealthChanged;
        }
    }
}
