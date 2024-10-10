using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    public GameObject cameraHolder;

    public GameObject terrain;

    public float padding = 30f;
    public bool hasPadding = false;

    void LateUpdate()
    {
        if (player != null && terrain != null)
        {
            Vector3 desiredPosition = player.position;

            Bounds terrainBounds = terrain.GetComponent<Collider>().bounds;
            
            if (hasPadding)
            {
                if (desiredPosition.x < terrainBounds.min.x + padding)
                {
                    desiredPosition.x = terrainBounds.min.x + padding;
                }
                else if (desiredPosition.x > terrainBounds.max.x - padding)
                {
                    desiredPosition.x = terrainBounds.max.x - padding;
                }

                if (desiredPosition.z < terrainBounds.min.z + padding)
                {
                    desiredPosition.z = terrainBounds.min.z + padding;
                }
                else if (desiredPosition.z > terrainBounds.max.z - padding)
                {
                    desiredPosition.z = terrainBounds.max.z - padding;
                }
            }


            Vector3 smoothedPosition = Vector3.Lerp(cameraHolder.transform.position, desiredPosition, smoothSpeed);
            cameraHolder.transform.position = new Vector3(smoothedPosition.x, cameraHolder.transform.position.y, smoothedPosition.z);
        }
    }
}
