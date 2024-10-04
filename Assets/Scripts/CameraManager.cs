using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    public CameraShake cameraShake;
    public GameObject cameraHolder;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TriggerShake(float impactForceMagnitude)
    {
        var shakeMagnitude = impactForceMagnitude / 10000f;
        StartCoroutine(cameraShake.Shake(0.5f, shakeMagnitude));
    }

    public void StartDezoom(float multiplier, float duration)
    {
        StartCoroutine(Dezoom(multiplier, duration));
    }

    private IEnumerator Dezoom(float multiplier, float duration)
    {
        Vector3 currentPosition = cameraHolder.transform.position;

        float targetY = currentPosition.y * multiplier;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float newY = Mathf.Lerp(currentPosition.y, targetY, elapsedTime / duration);

            cameraHolder.transform.position = new Vector3(currentPosition.x, newY, currentPosition.z);

            yield return null;
        }

        cameraHolder.transform.position = new Vector3(currentPosition.x, targetY, currentPosition.z);
    }
}
