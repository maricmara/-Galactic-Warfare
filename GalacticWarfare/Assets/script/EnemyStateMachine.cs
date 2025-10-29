using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    private Animator animator;
    private Transform player;
    private NavMeshAgent agent;

    public float chaseDistance = 8f;
    public float attackDistance = 3f;

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);
        animator.SetFloat("distance", dist);

        if (dist < attackDistance)
            animator.SetTrigger("Attack");
        else if (dist < chaseDistance)
        {
            agent.SetDestination(player.position);
            animator.SetBool("isChasing", true);
        }
        else
            animator.SetBool("isChasing", false);
    }
}

