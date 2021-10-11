using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    public int attackPoint = 3;
    public int distanceForAttack = 3;
    EnemyHealth enemyHealth;
    public Animator animator;
    HealthPlayer healthPlayer;
    HealthAI healthAI;
    NavMeshAgent navMeshAgent;



    private void Start()
    {
        enemyHealth = FindObjectOfType<EnemyHealth>();
        animator = GetComponent<Animator>();
        if (gameObject.tag == "Player")
            healthPlayer = GetComponent<HealthPlayer>();
        if (gameObject.tag == "WarriorAI")
        {
            healthAI = GetComponent<HealthAI>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
           

    }

    private void Update()
    {
        if (enemyHealth.currentHealth < 0) return;
        if (gameObject.tag == "Player")
        {
            if (!healthPlayer.isLive) return;
            float distanceToPlayer = Vector3.Distance(enemyHealth.transform.position, transform.position);

            if (distanceToPlayer <= distanceForAttack && healthPlayer.isLive && enemyHealth.currentHealth > 0 && !enemyHealth.onReborn)
            {
                FaceTarget();
                AnimationAttack();
            }
        }

        if (gameObject.tag == "WarriorAI")
        {
            if (!healthAI.isLive) return;
            float distanceToPlayer = Vector3.Distance(enemyHealth.transform.position, transform.position);
            navMeshAgent.SetDestination(enemyHealth.transform.position);

            if (distanceToPlayer <= distanceForAttack && healthAI.isLive && enemyHealth.currentHealth > 0 && !enemyHealth.onReborn)
            {
                FaceTarget();
                AnimationAttack();
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (enemyHealth.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5 * Time.deltaTime);
    }
    void AnimationAttack()
    {
        animator.SetBool("Attack", true);
    }

    void AttackTarget()
    {
        enemyHealth.ModifyHealth(-attackPoint); //(-UnityEngine.Random.Range(0.5f + attackPoint, attackPoint - 0.5f));
        animator.SetBool("Attack", false);
    }
}
