using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    public GameObject cameraHolder;

    public GameObject terrain;

    public float padding = 50f;
    void LateUpdate()
    {
        if (player != null && terrain != null)
        {
            Vector3 desiredPosition = player.position;

            Bounds terrainBounds = terrain.GetComponent<Collider>().bounds;
            

            if (desiredPosition.x < terrainBounds.min.x + padding)
            {
                desiredPosition.x = terrainBounds.min.x + padding;
            }
            else if (desiredPosition.x > terrainBounds.max.x - padding)
            {
                desiredPosition.x = terrainBounds.max.x - padding;
            }

            if (desiredPosition.y < terrainBounds.min.y + padding)
            {
                desiredPosition.y = terrainBounds.min.y + padding;
            }
            else if (desiredPosition.y > terrainBounds.max.y - padding)
            {
                desiredPosition.y = terrainBounds.max.y - padding;
            }

            Vector3 smoothedPosition = Vector3.Lerp(cameraHolder.transform.position, desiredPosition, smoothSpeed);
            cameraHolder.transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, cameraHolder.transform.position.z);
        }
    }
}
