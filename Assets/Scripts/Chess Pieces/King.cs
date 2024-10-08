using System;
using UnityEngine;
using UnityEngine.UI;

public class King : ChessPiece
{
    private float _moveTimer;
    private float terrainLength;
    private GameObject backgroundObject;
    public Slider healthSlider;
    private LevelManager gameOverManager;

    public AudioClip deathSound;
    public AudioSource deathAudioSource;
    [SerializeField] private float forceToApply;

    public static event Action OnGamelost;
    private void Start()
    {
        healthSlider.value = 1;
        gameOverManager = FindObjectOfType<LevelManager>();
    }

    void Update()
    {

    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ennemy"))
        {
            ReceiveDamage();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                Vector3 impactForce = collision.relativeVelocity * playerRigidbody.mass;
                var impactForceClamped = Vector3.ClampMagnitude(impactForce, 10000f);
                Debug.Log("Player Force : " + -impactForceClamped.normalized * impactForceClamped.magnitude * 0.5f);
                forceToApply = impactForceClamped.magnitude * 0.5f;
                playerRigidbody.AddForce(-impactForceClamped.normalized * forceToApply, ForceMode.Impulse);
            }

            return;
        }

        base.OnCollisionEnter(collision);
    }

    void ReceiveDamage()
    {
        var kingDamageReceive = pieceDamageReceive;

        currentHealth -= kingDamageReceive;
        healthSlider.value -= 1 / kingDamageReceive;
        //Debug.Log(gameObject.name + " a reçu " + kingDamageReceive + " de dégâts. Vie restante : " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected override void Die()
    {
        Debug.Log("Le roi est mort ! ");
        gameOverManager.DisplayGameOverScreen();
        OnGamelost?.Invoke();

        deathAudioSource.clip = deathSound;
        deathAudioSource.Play();
        //SpawnParticles();
        //OnEnemyDied?.Invoke();
        //Destroy(gameObject);
    }

    public override void Move()
    {
    }
}