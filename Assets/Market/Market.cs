using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class Market : MonoBehaviour
{
    public GameObject marketPanel;

    [Header ("Count resources")]
    public TMP_Text appleCount;
    public TMP_Text seedCount;
    public TMP_Text mushroomCount;

    [Header("Prices")]
    public TMP_Text appleCostText;
    public TMP_Text seedCostText;
    public TMP_Text mushroomCostText;

    [Header("ResourcesCosts")]
    public int appleCost = 3;
    public int seedCost = 1;
    public int mushroomCost = 7;

    CanvasGroup canvasGroup;
    Bank bank;

    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
        canvasGroup = marketPanel.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canvasGroup.alpha > 0)
        {
            appleCostText.text = appleCost.ToString();
            seedCostText.text = seedCost.ToString();
            mushroomCostText.text = mushroomCost.ToString();

            appleCount.text = bank.appleStorage.currentAmount.ToString();
            seedCount.text = bank.seedStorage.currentAmount.ToString();
            mushroomCount.text = bank.mushroomStorage.currentAmount.ToString();
        }
    }

    public void SellApple()
    {
        if(bank.appleStorage.currentAmount > 0)
        {
            bank.appleStorage.DecreaseRes(1);
            bank.money += appleCost;
        }
    }

    public void SellSeed()
    {
        if (bank.seedStorage.currentAmount > 0)
        {
            bank.seedStorage.DecreaseRes(1);
            bank.money += seedCost;
        }
    }

    public void SellMushroom()
    {
        if (bank.mushroomStorage.currentAmount > 0)
        {
            bank.mushroomStorage.DecreaseRes(1);
            bank.money += mushroomCost;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
            ToggleWindow();
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //        ToggleWindow();
    //}

    public void ToggleWindow()
    {
        canvasGroup.DOFade(canvasGroup.alpha < 1 ? 1 : 0, 0.5f).OnComplete(()=>
        {
            canvasGroup.interactable = canvasGroup.alpha == 1;
            canvasGroup.blocksRaycasts = canvasGroup.alpha == 1;
        });
    }
}
