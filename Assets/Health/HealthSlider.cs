using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField]
    private Slider slider;
    [SerializeField]
    private float updateSpeedSeconds = 0.5f;
    [SerializeField]
    private float positionOffset;

    private HealthResources HealthResources;
    Mover mover;
    Image image;
    public GameObject children;

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mover = FindObjectOfType<Mover>();
        image = GetComponent<Image>();
    }

    public void SetHealth(HealthResources health)
    {
        this.HealthResources = health;
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
            children.SetActive(false);
        }
        else
        {
            slider.enabled = true;
            image.enabled = true;
            children.SetActive(true);
        }
            
    }

    private void LateUpdate()
    {
        transform.position = mainCamera.WorldToScreenPoint(HealthResources.transform.position + Vector3.up * positionOffset); //it`s slow можно ли прокидывать вручную?
    }

    private void OnDestroy()
    {
        {
            HealthResources.OnHealthSliderPctChanged -= HandleHealthChanged;
        }
    }
}
