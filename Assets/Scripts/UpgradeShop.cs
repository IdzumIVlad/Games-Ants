using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UpgradeShop : MonoBehaviour
{
    public GameObject shopPanel;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            ToggleWindowWithFade();
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        ToggleWindowWithFade();
    //    }
    //}

    public void ToggleWindowWithFade()
    {
        var canvasGroup = shopPanel.GetComponent<CanvasGroup>();
        canvasGroup.DOFade(canvasGroup.alpha < 1 ? 1 : 0, 0.5f).OnComplete(() =>
        {
            canvasGroup.interactable = canvasGroup.alpha == 1;
            canvasGroup.blocksRaycasts = canvasGroup.alpha == 1;
        });
    }
}
