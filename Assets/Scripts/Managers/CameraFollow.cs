using UnityEngine;
// Camera follows the player, keeps in mind the boundries of the camera 
public class CameraFollow : MonoBehaviour
{
    [Header("Target Setup")]
    public Transform target;
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    [Header("Smoothness")]
    [Range(1f, 10f)]
    public float smoothFactor = 5f;

    [Header("Map Bounds")]
    public bool useBounds = true;
    public Vector2 minCameraPos;
    public Vector2 maxCameraPos;

    void LateUpdate()
    {
        if (target != null) {
            Vector3 desiredPosition = target.position + offset;
            
            if (useBounds) {
                desiredPosition.x = Mathf.Clamp(desiredPosition.x, minCameraPos.x, maxCameraPos.x);
                desiredPosition.y = Mathf.Clamp(desiredPosition.y, minCameraPos.y, maxCameraPos.y);
            }
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothFactor * Time.deltaTime);
        }
    }
}