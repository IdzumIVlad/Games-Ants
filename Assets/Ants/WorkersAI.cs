using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkersAI : MonoBehaviour
{
    AntMother antMother;
    Baggage baggage;
    public NavMeshAgent agent;
    public PickUpResourses currentResContainer;
    public List<PickUpResourses> resList;
    public List<Teleport> teleportList;
    public List<ResourceStorage> storageList;
    public float SelectionProcessTime = 5;
    public ResTypes assignedResType;

    

    #region huetaVadosa
    /*void Start()
    {
        baggage = GetComponentInParent<Baggage>();
        aiMotion = FindObjectsOfType<AIMotion>();
    }

    IEnumerator Harvester()
    {
        while (doHarvest)
            yield return null;
    }

    private void Update()
    {
        if (baggage.currentResAmount > 0)
        {
            ResTypes res = ResTypes.Apple;
            for (int i = 0; i < baggage.resSlots.Length; i++)
            {
                if (baggage.resSlots[i].resAmount > 0)
                    res = baggage.resSlots[i].resTypes;
            }
            switch (res)
            {
                case ResTypes.Apple:
                    NewDestination(appleStorage.transform.position);
                    break;

                case ResTypes.Seed:
                    Debug.Log("Seed");
                    break;

                case ResTypes.Mushroom:
                    Debug.Log("Mushroom");
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    public void NewDestination(Vector3 position)
    {
        transform.gameObject.GetComponent<NavMeshAgent>().SetDestination(position);
    }

    public void UpdateMassiveAnts()
    {
        aiMotion = FindObjectsOfType<AIMotion>();
    }

    public void SetDestination()
    {
        foreach (AIMotion ai in aiMotion)
        {
            ai.agent.SetDestination(transform.position);
        }

    }

    */
    #endregion

    private void Start()
    {
        resList = new List<PickUpResourses>(FindObjectsOfType<PickUpResourses>().ToList());
        teleportList = new List<Teleport>(FindObjectsOfType<Teleport>().ToList());
        storageList = new List<ResourceStorage>(FindObjectsOfType<ResourceStorage>().ToList());
        baggage = gameObject.GetComponent<Baggage>();
        
    }

    private void OnEnable()
    {
        antMother = FindObjectOfType<AntMother>();
        if (antMother != null)
            antMother.currentAmount++;
        else
            return;
    }

    private void OnDisable()
    {
        antMother.currentAmount--;
    }

    private void Update()
    {
        if (baggage.currentResAmount == 0)
        {
            if (currentResContainer == null)
            {
                currentResContainer = FindResContainer();
            }

            if (currentResContainer == null) return;

            //check for path available 
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(currentResContainer.movePoint.position, path);

            if (path.status != NavMeshPathStatus.PathComplete)
                MoveToTeleport();
            else
                MoveToContainer();
        }
        else
        {
            NavMeshPath path = new NavMeshPath();
            foreach (var res in storageList)
            {
                if (res.resType == baggage.CurrentResInBaggage())
                {
                    if (res.currentAmount < res.roomCapacity)
                        agent.CalculatePath(res.movePoint.position, path);
                    else
                    {
                        Debug.Log("assigned storage is full, increase its level");
                        return;
                    }
                }
            }

            if (path.status != NavMeshPathStatus.PathComplete)
                MoveToTeleport();
            else
                MoveToStorage();
        }
    }

    public void MoveToTeleport()
    {
        Teleport currentTeleport = null;
        
        foreach (var teleport in teleportList)
        {
            //check for path available 
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(teleport.movePoint.position, path);
            if (path.status != NavMeshPathStatus.PathComplete) continue;
            currentTeleport = teleport;
            break;
        }
        
        if (currentTeleport == null)
            return;
        agent.SetDestination(currentTeleport.movePoint.position);
        agent.stoppingDistance = 0.2f;
    }
    
    public void MoveToContainer()
    {
        agent.stoppingDistance = 2f;
        var currentDistance = Vector3.Distance(transform.position, currentResContainer.movePoint.position);
        agent.SetDestination(currentResContainer.movePoint.position);
    }

    public void MoveToStorage()
    {
        agent.stoppingDistance = 0.2f;
        ResTypes resInBaggage = baggage.CurrentResInBaggage();
        ResourceStorage currentStorage = null;

        foreach (var storage in storageList)
        {
            if (storage.resType == resInBaggage)
            {
                //check for path available 
                NavMeshPath path = new NavMeshPath();
                agent.CalculatePath(storage.movePoint.position, path);
                if (path.status != NavMeshPathStatus.PathComplete) continue;

                currentStorage = storage;
                break;
            }
        }

        if (currentStorage == null)
        {
            return;
        }

        agent.SetDestination(currentStorage.transform.position);
    }


    private PickUpResourses FindResContainer()
    {
        PickUpResourses result = null;

        foreach (var res in resList)
        {
            if (res.resTypes != assignedResType) continue;
            result = res;
            break;
        }

        return result;
    }

    public void SwitchAssidnedResType()
    {

    }
}



