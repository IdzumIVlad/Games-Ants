using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMotion : MonoBehaviour
{
    public NavMeshAgent agent;
    private Animator animator;
   // private GameObject player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
       // player = GameObject.FindGameObjectWithTag("Player");
    }

    

    void Update()
    {
        animator.SetBool("Move", agent.velocity.x != 0 || agent.velocity.z != 0);
        if (animator.GetBool("Move")) animator.SetBool("Attack", false);
    }
}
