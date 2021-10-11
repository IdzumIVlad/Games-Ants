using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;

public class ResourceStorage : MonoBehaviour
{
    public int roomLevel = 1;
    public int roomCapacity;
    public int currentAmount;
    public GameObject resourcePrefab;
    public ResTypes resType;
    public Transform movePoint;
    public Expirience expirience;

    [Header("How long is it produced")]
    public float processingTime = 10;

    public Image loadingIMG;
    public Canvas Canvas;

    public List<GameObject> resInRoom;

    public void LevelUp()
    {
        roomLevel++;
        roomCapacity += roomLevel * roomCapacity;
        expirience.exp += roomLevel;
    }

    private void Start()
    {
        List<GameObject> resInRoom = new List<GameObject>();
        for(int i = 0; i < currentAmount; i++)
        {
            IncreaseResToStorage();
        }
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
            else if(other.gameObject.GetComponent<Baggage>() != null && currentAmount > 0)
                if (other.gameObject.GetComponent<Baggage>().currentResAmount == 0)
                    DecreaseRes(other);
            else
                return;
        }
    }


    public void IncreaseResToStorage()
    {
        GameObject prefab;
        prefab = Instantiate(resourcePrefab, new Vector3(transform.position.x + Random.Range(-1.1f, 1.1f), -70.91f,
             transform.position.z + Random.Range(-0.93f, 1.3f)), Quaternion.identity);
        resInRoom.Add(prefab);
        prefab.transform.SetParent(transform);
    }

    void IncreaseResToStorage(Collider other)
    {
        GameObject prefab;
        other.gameObject.GetComponent<Baggage>().DecreaseRes(resType);
        prefab = Instantiate(resourcePrefab, new Vector3(transform.position.x + Random.Range(-1.1f, 1.1f), -70.91f,
             transform.position.z + Random.Range(-0.93f, 1.3f)), Quaternion.identity);
        resInRoom.Add(prefab);
        prefab.transform.SetParent(transform);
        currentAmount++;
    }

    public void DecreaseRes(int cost) // для покупок за ресурсы
    {
        GameObject firstRes = resInRoom[0];
        resInRoom.Remove(firstRes);
        Destroy(firstRes);
        currentAmount -= cost;
    }

    void DecreaseRes(Collider other)
    {
        other.gameObject.GetComponent<Baggage>().IncreaseRes(resType, 1);
        GameObject firstRes = resInRoom[0];
        resInRoom.Remove(firstRes);
        Destroy(firstRes);
        currentAmount--;
    }
}
