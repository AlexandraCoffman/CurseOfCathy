using UnityEngine;
using UnityEngine.UI;
using System.Collections;
// Player health, ui, and invulnerability settings
public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHearts = 5; 
    private int currentHearts;

    [Header("UI Settings")]
    public Image[] heartImages;

    [Header("Invulnerability Settings")]
    public float invulnerabilityTime = 5f;
    private bool isInvulnerable = false;

    private SpriteRenderer spriteRenderer;
    private Collider2D playerCollider;

    void Start()
    {
        currentHearts = maxHearts;
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
        
        UpdateHealthUI();
    }

    public void TakeDamage(Collider2D enemyCollider)
    {
        if (isInvulnerable) {
            return;
        }

        currentHearts--;
        UpdateHealthUI();

        if (currentHearts <= 0) {
            if (GameManager.Instance != null) {
                GameManager.Instance.GameOver();
            }
        } else {
            StartCoroutine(DamageRoutine(enemyCollider));
        }
    }

    private void UpdateHealthUI()
    {
        for (int i = 0; i < heartImages.Length; i++) {
            if (i < currentHearts) {
                heartImages[i].enabled = true;
            } else {
                heartImages[i].enabled = false;
            }
        }
    }

    IEnumerator DamageRoutine(Collider2D enemyCollider)
    {
        isInvulnerable = true;
        Physics2D.IgnoreCollision(playerCollider, enemyCollider, true);

        float elapsedTime = 0f;
        float blinkInterval = 0.2f;

        while (elapsedTime < invulnerabilityTime) {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0f); 
            yield return new WaitForSeconds(blinkInterval);
            
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f); 
            yield return new WaitForSeconds(blinkInterval);

            elapsedTime += (blinkInterval * 2);
        }

        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

        if (enemyCollider != null) {
            Physics2D.IgnoreCollision(playerCollider, enemyCollider, false);
        }

        isInvulnerable = false;
    }
}