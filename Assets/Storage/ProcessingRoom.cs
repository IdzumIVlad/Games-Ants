using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ProcessingRoom : MonoBehaviour
{
    public int roomLvl = 0;
    public int cost = 10;
    public int roomCapacity = 1;
    public int currentAmount = 0;
    [Header("How long is it produced")]
    public float processingTime = 10;

    public Image loadingIMG;
    public Canvas Canvas;

    public GameObject processingRoom;
    public GameObject resourcePrefab;
    public ResTypes resType = ResTypes.Seed;
    public List<GameObject> resInRoom;
    

    public GameObject mushroomPrefab;

    private void Start()
    {
        DOTween.Init();
        if (roomLvl == 0)
            processingRoom.SetActive(false);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Workers" || other.transform.tag == "Player")
        {
            if (other.gameObject.GetComponent<Baggage>().GetCurrentRes(resType) > 0)
            {
                if (currentAmount < roomCapacity)
                    IncreaseResToStorage(other);
                else
                    Debug.Log("Storage overloaded" + this.name); // вывести message про переполненный склад
            }
            else if (other.gameObject.GetComponent<Baggage>() != null && currentAmount > 0)
                if (other.gameObject.GetComponent<Baggage>().currentResAmount == 0)
                {
                    DecreaseRes(other);

                }
                    
                else
                    return;
        }
    }

    void IncreaseResToStorage(Collider other)
    {
        other.gameObject.GetComponent<Baggage>().DecreaseRes(resType);
        GameObject prefab = Instantiate(resourcePrefab, new Vector3(transform.position.x + Random.Range(-0.8f, 0.8f), -70.91f,
             transform.position.z + Random.Range(-1.6f, 0f)), Quaternion.identity);
        prefab.transform.SetParent(transform);
        currentAmount++;
        resInRoom.Add(prefab);
        DOVirtual.DelayedCall(processingTime / 3, () => ProducedMushroom(), false);

    }

    void DecreaseRes(Collider other)
    {
        if (resInRoom.Count > 0)
        {
            currentAmount--;
            other.gameObject.GetComponent<Baggage>().IncreaseRes(ResTypes.Mushroom, 1);
            GameObject firstRes = resInRoom[0];
            resInRoom.Remove(firstRes);
            Destroy(firstRes);
            currentAmount--;
        }
    }

    void BornMushroom()
    {
        GameObject prefab = Instantiate(mushroomPrefab, new Vector3(transform.position.x + Random.Range(-0.8f, 0.8f), -70.91f,
             transform.position.z + Random.Range(-1.6f, 0f)), Quaternion.identity);
        resInRoom.Add(prefab);
        currentAmount++;
        loadingIMG.fillAmount = 0;
        Canvas.gameObject.SetActive(false);
    } 

    void ProducedMushroom()
    {
        Canvas.gameObject.SetActive(true);
        loadingIMG.DOFillAmount(1, processingTime);
        DOVirtual.DelayedCall(processingTime, () => BornMushroom(), false);
        GameObject firstRes = resInRoom[0];
        resInRoom.Remove(firstRes);
        Destroy(firstRes);

        //Invoke("BornMushroom", processingTime);
    }
}
