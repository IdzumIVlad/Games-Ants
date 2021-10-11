using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PickUpResourses : MonoBehaviour
{
    public float timePickUp = 3;

    public int currentCapacity = 10;
    public int maxCapacity = 10;
    
    public ResTypes resTypes;

    public Image loadCircle;
    public Transform movePoint;

    HealthResources healthResources;
    SpawningArea spawningArea;

    private void Start()
    {
        spawningArea = FindObjectOfType<SpawningArea>();
        healthResources = GetComponent<HealthResources>();
        gameObject.SetActive(true);
        MoveToNewPosition();
    }

    private void Update()
    {
        if (currentCapacity == 0)
        {
            MoveToNewPosition();
        }


    }

    private void OnTriggerStay(Collider other) 
    {
        
        if (other.transform.tag == "Workers" || other.transform.tag == "Player")
        {
            
            Baggage baggage = other.GetComponent<Baggage>();
            if (baggage != null) {
                if (baggage.currentResAmount == 0)
                {
                    loadCircle.fillAmount = 1 / timePickUp;
                    timePickUp -= 1 * Time.deltaTime;

                    if (timePickUp <= 0)
                    {
                        baggage.PickUpRes(this);
                        ClearCircle();
                        healthResources.ModifyHealth(-1);
                    }
                       
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Workers" || other.transform.tag == "Player")
        {
            ClearCircle();
        }
    }

    public void ClearCircle()
    {
        loadCircle.fillAmount = 0;
        timePickUp = 3;
    }

    void MoveToNewPosition()
    {
        gameObject.SetActive(false);
        gameObject.transform.position = spawningArea.SpawnPosition(resTypes); // raycast
        currentCapacity = maxCapacity;
        gameObject.SetActive(true);
    }
}
