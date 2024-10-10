using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    public CameraShake cameraShake;
    public GameObject cameraHolder;
    public Player player; // R�f�rence au joueur
    public float baseCameraHeight = 10f; // Hauteur initiale de la cam�ra
    public float maxCameraHeight = 50f; // Hauteur maximale de la cam�ra
    public float speedForMaxDezoom = 1000f; // Vitesse pour atteindre le d�zoom max
    public float cameraLerpSpeed = 2f;

    private float targetCameraHeight;

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

    void Update()
    {
        if (player != null)
        {
            AdjustCameraZoom();
        }
    }

    // Conserve ta m�thode de tremblement de cam�ra
    public void TriggerShake(float impactForceMagnitude)
    {
        var shakeMagnitude = impactForceMagnitude / 10000f;
        StartCoroutine(cameraShake.Shake(0.5f, shakeMagnitude));
    }

    private void AdjustCameraZoom()
    {
        float playerSpeed = player.CurrentSpeed;

        // Calcul du ratio de vitesse
        float speedRatio = Mathf.Clamp01(playerSpeed / speedForMaxDezoom);

        // Calcul de la hauteur cible de la cam�ra
        targetCameraHeight = Mathf.Lerp(baseCameraHeight, maxCameraHeight, speedRatio);

        // Interpolation Lerp pour une transition douce de la hauteur de la cam�ra
        Vector3 currentPosition = cameraHolder.transform.position;
        float newCameraHeight = Mathf.Lerp(currentPosition.y, targetCameraHeight, Time.deltaTime * cameraLerpSpeed);

        // Mise � jour de la position Y de la cam�ra
        cameraHolder.transform.position = new Vector3(currentPosition.x, newCameraHeight, currentPosition.z);
    }

    // Conserve ta m�thode de d�zoom pour d'autres �v�nements si n�cessaire
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
