using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Teleport : MonoBehaviour
{
    [SerializeField] GameObject homePos;
    [SerializeField] GameObject naturePos;
    public Transform movePoint;

    private CharacterController characterController;
    private NavMeshAgent nav;

    private void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Player")
        {
            characterController = other.gameObject.GetComponent<CharacterController>();
            characterController.enabled = false;
            if (transform.name == "TeleportToNature")
                other.gameObject.transform.position = naturePos.transform.position;
            if (transform.name == "TeleportToHome")
                other.gameObject.transform.position = homePos.transform.position;
            characterController.enabled = true;
        }

       // if(other.gameObject.)

        if (other.tag == "Workers") // other.tag == "Ant(AI)" ||
        {
            nav = other.gameObject.GetComponent<NavMeshAgent>();
            Vector3 dest = nav.destination;
            nav.ResetPath();

            if (transform.name == "TeleportToNature")
                nav.Warp(naturePos.transform.position);
                
            if (transform.name == "TeleportToHome")
                nav.Warp(homePos.transform.position);

            nav.SetDestination(dest);
            
        }
    }
}
