using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class AntMother : MonoBehaviour
{
    public int roomCapacity = 1;
    public int roomLevel = 1;
    public float processingTime = 2;
    public GameObject egg;
    public GameObject ant;
    public ResTypes resType = ResTypes.Mushroom;
    public float speedAntAI = 2;

    public List<WorkersAI> workersAIs = new List<WorkersAI>();

    public int currentAmount = 0;

    int amountForStart = 0;

    private void Start()
    {
        workersAIs = FindObjectsOfType<WorkersAI>().ToList();
        for (int i = 0; i < amountForStart; i++)
        {
            BornNewAnt();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Workers" || other.gameObject.tag == "Player")
        {
           if ((other.GetComponent<Baggage>().GetCurrentRes(resType) > 0 && roomCapacity > currentAmount))
            {
                BornNewEgg();
                other.GetComponent<Baggage>().DecreaseRes(resType);
            }
        }
        
    }


    private void BornNewEgg()
    {
        egg.SetActive(true);  
        if(currentAmount < roomCapacity)
        {
            Invoke("BornNewAnt", processingTime);
            Invoke("OffEgg", processingTime);
            
        }
    }

    private void BornNewAnt()
    {
        var newAnt = Instantiate(ant, new Vector3(transform.position.x + Random.Range(-1f, 1f),
            transform.position.y + Random.Range(-1f, 1f),
            transform.position.z + Random.Range(-1f, 1f)), Quaternion.identity);
        newAnt.GetComponent<NavMeshAgent>().speed = speedAntAI;
        workersAIs.Add(newAnt.GetComponent<WorkersAI>());
    }

    private void OffEgg()
    {
        egg.SetActive(false);
    }

}
