using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;
    private UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (animator == null || agent == null) return;

        if (agent.velocity.magnitude > 0.1f)
            animator.Play("black_run");
        else
            animator.Play("black_idle");
    }
}