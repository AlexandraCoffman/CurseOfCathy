using UnityEngine;
// Sprint speed boost for player getting coffee relics
public class CoffeeRelics : MonoBehaviour
{
    [Header("Boost Settings")]
    public float boostDuration = 3f;
    public float speedMultiplier = 1.6f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            
            if (playerMovement != null) {
                playerMovement.StartSpeedBoost(boostDuration, speedMultiplier);
                Destroy(gameObject);
            }
        }
    }
}