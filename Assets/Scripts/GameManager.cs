using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    public Player player;

    public AudioSource audioSource;
    public AudioClip collisionSound;

    private void Awake()
    {
    }

    private void OnEnable()
    {
        ChessPiece.OnEnemyDied += OnEnemyDied;
        Player.OnPlayerCollision += OnPlayerCollision;
        ChessPiece.OnCollisionWithPlayer += OnCollisionWithPlayer;
    }

    private void OnDisable()
    {
        ChessPiece.OnEnemyDied -= OnEnemyDied;
        Player.OnPlayerCollision -= OnPlayerCollision;
        ChessPiece.OnCollisionWithPlayer -= OnCollisionWithPlayer;
    }

    void OnEnemyDied()
    {
        player.IncreasedStats();
    }

    private void OnPlayerCollision(Collision collision)
    {
        if (tag == "Ennemy") return;
        Vector3 impactForce = collision.relativeVelocity * player.GetComponent<Rigidbody>().mass;
        float impactForceMagnitude = impactForce.magnitude;
        var impactForceMagnitudeClamp = Mathf.Clamp(impactForceMagnitude, 0f, 5000f);

        PlayCollisionSound(impactForceMagnitudeClamp);
        CameraManager.Instance.TriggerShake(impactForceMagnitudeClamp);
    }

    private void OnCollisionWithPlayer(float impactForceMagnitude)
    {
        if (tag == "Wall") return;
        var impactForceMagnitudeClamp = Mathf.Clamp(impactForceMagnitude, 0f, 5000f);
        PlayCollisionSound(impactForceMagnitudeClamp);
        CameraManager.Instance.TriggerShake(impactForceMagnitudeClamp);
    }

    private void PlayCollisionSound(float impactSpeed)
    {
        float normalizedImpactSpeed = Mathf.Clamp(impactSpeed, 0f, 5000f) / 5000f;

        audioSource.volume = 0.1f;
        audioSource.pitch = Mathf.Lerp(0.5f, 1.0f, normalizedImpactSpeed);
        audioSource.volume = Mathf.SmoothStep(0f, 0.2f, normalizedImpactSpeed);

        if (!audioSource.isPlaying)
        {
            audioSource.clip = collisionSound;
            audioSource.Play();
        }
    }

    void Start()
    {
    }
}
