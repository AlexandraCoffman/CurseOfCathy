using UnityEngine;
// Player completes the level
public class LevelExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            GameManager.Instance.LevelComplete();
        }
    }
}