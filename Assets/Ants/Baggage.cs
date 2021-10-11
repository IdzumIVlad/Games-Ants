using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Baggage : MonoBehaviour
{
    public ResSlot[] resSlots;
    public int antCapacity = 1;
    public int currentResAmount;
    public float processingTime = 10;

    public MeshRenderer appleMesh;
    public MeshRenderer seedMesh;
    public MeshRenderer mushroomMesh;

    
    [System.Serializable]
    public class ResSlot
    {
        public ResTypes resTypes;
        public int resAmount;
    }

    private void Start()
    {
        
    }


    public int GetCurrentRes(ResTypes resTypes)
    {
        return GetResSlot(resTypes).resAmount;
    }

    private ResSlot GetResSlot(ResTypes resTypes)
    {
        foreach (ResSlot slot in resSlots)
        {
            if (slot.resTypes == resTypes)
            {
                return slot;
            }
        }
        return null;
    }

    public ResTypes CurrentResInBaggage()
    {
        ResTypes result = ResTypes.Apple;
        foreach (ResSlot slot in resSlots)
        {
            if (slot.resAmount != 0)
            {
                return slot.resTypes;
            }
        }
        return result;
    }

    public void PickUpRes(PickUpResourses resContainer)
    {
        if (currentResAmount >= antCapacity) return; // MESSAGE: baggage overloaded
        IncreaseRes(resContainer.resTypes, antCapacity);
        resContainer.currentCapacity -= antCapacity;

        if (resContainer.resTypes == ResTypes.Apple) appleMesh.enabled = true;
        if (resContainer.resTypes == ResTypes.Seed) seedMesh.enabled = true;
        if (resContainer.resTypes == ResTypes.Mushroom) mushroomMesh.enabled = true;

    }

    

    public void DecreaseRes(ResTypes resTypes) 
    {
        GetResSlot(resTypes).resAmount = 0;
        currentResAmount = 0;

        if (resTypes == ResTypes.Apple) appleMesh.enabled = false;
        if (resTypes == ResTypes.Seed) seedMesh.enabled = false;
        if (resTypes == ResTypes.Mushroom) mushroomMesh.enabled = false;
    }

    public void IncreaseRes(ResTypes resTypes, int amount)
    {
        GetResSlot(resTypes).resAmount += amount;
        currentResAmount += amount;
        if (resTypes == ResTypes.Apple) appleMesh.enabled = true;
        if (resTypes == ResTypes.Seed) seedMesh.enabled = true;
        if (resTypes == ResTypes.Mushroom) mushroomMesh.enabled = true;
    }

    

}
