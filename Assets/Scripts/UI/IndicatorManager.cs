using UnityEngine;
using UnityEngine.UI;
// Manager for the arrows indicating where the door and ememy are located relative to the player location
public class IndicatorManager : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform player;
    public float orbitRadius = 150f;

    [Header("Enemy Indicator")]
    public Transform enemyTarget; 
    public RectTransform enemyArrow; 

    [Header("Door Indicator")]
    public Transform doorTarget; 
    public RectTransform doorArrow; 

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        UpdateSingleIndicator(enemyTarget, enemyArrow);
        UpdateSingleIndicator(doorTarget, doorArrow);
    }

    private void UpdateSingleIndicator(Transform target, RectTransform arrow)
    {
        if (GameManager.Instance != null && GameManager.Instance.isLevelComplete) {
            if (arrow != null) arrow.gameObject.SetActive(false);
            return;
        }

        if (target == null || player == null || arrow == null) {
            if (arrow != null) arrow.gameObject.SetActive(false);
            return;
        }

        if (!arrow.gameObject.activeSelf) {
            arrow.gameObject.SetActive(true);
        }

        Vector3 direction = (target.position - player.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        arrow.localEulerAngles = new Vector3(0, 0, angle - 90f); 

        Vector3 playerScreenPos = cam.WorldToScreenPoint(player.position);
        playerScreenPos.z = 0f; 
        
        arrow.position = playerScreenPos; 

        Vector3 offset = new Vector3(direction.x, direction.y, 0) * orbitRadius;
        arrow.localPosition += offset; 
    }
}