using UnityEngine;
using UnityEngine.UI;
// UI Camera Shadow
public class CameraShadow : MonoBehaviour
{
    [Header("Shadow UI")]
    public Image shadowOverlay;

    private Color originalColor;

    void Start()
    {
        if (shadowOverlay != null) {
            originalColor = shadowOverlay.color;
        }
    }

    void Update()
    {
        
    }
}