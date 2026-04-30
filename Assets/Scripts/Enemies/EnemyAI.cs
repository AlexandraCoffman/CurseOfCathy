using UnityEngine;
using UnityEngine.AI;
// Enemy AI using GitHub NavMeshPro
public class EnemyAI : MonoBehaviour
{
    [Header("Target Settings")]
    [Tooltip("Player")]
    public Transform target; 

    private NavMeshAgent agent;
    private Collider2D myCollider;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        myCollider = GetComponent<Collider2D>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        Vector3 pos = transform.position;
        pos.z = 0f;
        transform.position = pos;
    }

    void Update()
    {
        if (target == null) {
            return;
        }

        if (agent.isOnNavMesh) {
            agent.SetDestination(target.position);
        } else {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(transform.position, out hit, 2.0f, NavMesh.AllAreas)) {
                transform.position = hit.position;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Enemy collided with: " + collision.gameObject.name + " Tag: " + collision.gameObject.tag);
        
        if (collision.gameObject.CompareTag("Player")) {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            
            if (playerHealth != null) {
                playerHealth.TakeDamage(myCollider);
            }
        }
    }
}