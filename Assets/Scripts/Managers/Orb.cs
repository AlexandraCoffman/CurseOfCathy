using UnityEngine;
// Player collects the orbs
public class Orb : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            GameManager.Instance.AddOrb();
            Destroy(gameObject);
        }
    }
}