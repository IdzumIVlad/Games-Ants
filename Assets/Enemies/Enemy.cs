using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Slider healthBar;
    
    public int attackPoint = 1;
    public float chaseRange = 5f;

    public List<HealthAI> targets;
    float distanceToTarget = Mathf.Infinity;

    public Transform homePoint;
    
    
    NavMeshAgent navMeshAgent;
    public Animator animator;
    HealthPlayer healthPlayer;
    GameObject currentTarget;
    Mover mover;
    EnemyHealth enemyHealth;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }


    private void Start()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
        healthPlayer = FindObjectOfType<HealthPlayer>();
        enemyHealth = GetComponent<EnemyHealth>();
        mover = FindObjectOfType<Mover>();
    }

    private void Update()
    {
        navMeshAgent.speed = mover.speed - 0.5f;

        if (navMeshAgent.velocity.sqrMagnitude > 0)
            animator.SetBool("Move", true);
        else
            animator.SetBool("Move", false);

        float distanceToPlayer = Vector3.Distance(healthPlayer.transform.position, transform.position);

        if(distanceToPlayer <= chaseRange)
        {
            currentTarget = healthPlayer.gameObject;
            if (navMeshAgent.pathStatus != NavMeshPathStatus.PathInvalid)
                navMeshAgent.SetDestination(healthPlayer.transform.position);

            if(distanceToPlayer <= navMeshAgent.stoppingDistance && healthPlayer.isLive && enemyHealth.currentHealth > 0 && !enemyHealth.onReborn)
            {
                FaceTarget();
                AnimationAttack();

            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (healthPlayer.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5 * Time.deltaTime);
    }
    void AnimationAttack()
    {
        animator.SetBool("Attack", true);
    }

    void AttackTarget()
    {
        animator.SetBool("Attack", false);
        if (currentTarget.gameObject.tag == "Player")
            healthPlayer.ModifyHealth(-UnityEngine.Random.Range(0.5f + attackPoint, attackPoint - 0.5f));
        //if (currentTarget.gameObject.tag == "Workers")
        //    currentTarget.GetComponent<HealthAI>().ModifyHealth(-attackPoint, this); // дорого
        
    }

    

}
