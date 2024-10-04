using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class ChessPiece : MonoBehaviour
{
    protected Rigidbody _rigidbody;
    public ChessPieceData chessPieceData;
    public GameObject floatingTextPrefab;
    public GameObject particlePrefab;
    public bool isInvincible = false; // Pour le debug
    //public Slider healthSlider;

    private float currentHealth;
    private GameObject parent;

    private string[] noDamageMessages = {
        "That tickles!",
        "No pain!",
        "Barely a scratch.",
        "Just a nudge...",
        "Minor impact.",
        "Not strong enough!",
        "Try harder!",
        "I've seen worse.",
        "Light as a feather.",
        "Barely felt that!"
    };

    public static event Action OnEnemyDied;
    public static event Action<float> OnCollisionWithPlayer;
    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        currentHealth = chessPieceData.maxHealth;
        parent = GameObject.Find("FloatingTextParent");
    }

    public abstract void Move();

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                //float impactVelocity = collision.relativeVelocity.magnitude;
                Vector3 impactForce = collision.relativeVelocity * playerRigidbody.mass;
                
                float impactForceMagnitude = impactForce.magnitude;
                OnCollisionWithPlayer?.Invoke(impactForceMagnitude);

                float damage = CalculateDamage(impactForceMagnitude);
                TakeDamage(damage);
                var colisionPosition = collision.GetContact(0).point;
                ShowFloatingText(damage, colisionPosition);

                var impactForceClamped = Vector3.ClampMagnitude(impactForce, 10000f);
                playerRigidbody.AddForce(-impactForceClamped.normalized * impactForceClamped.magnitude * 0.5f, ForceMode.Impulse);

                //playerRigidbody.velocity = Vector3.zero;
                Player playerScript = collision.gameObject.GetComponent<Player>();
                if (playerScript != null)
                {
                    //playerScript.StopPlayer();
                }
            }
        }
    }
    protected float CalculateDamage(float impactVelocity)
    {
        impactVelocity /= 10f;
        var velocityThreshold = 100f; // Valeur minimum de vitesse à l'impact pour infliger des dégats
        if (impactVelocity > velocityThreshold)
            return impactVelocity;
        else
            return 0; // Pas assez de vitesse à l'imapct
    }

    public virtual void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        //  Debug.Log(gameObject.name + " a reçu " + damageAmount + " de dégâts. Vie restante : " + currentHealth);

        if (currentHealth <= 0 && !isInvincible)
        {
            Die();
        }
    }

    void ShowFloatingText(float damageAmount, Vector3 position)
    {
        if (floatingTextPrefab != null)
        {
            var go = Instantiate(floatingTextPrefab, position, floatingTextPrefab.transform.rotation, parent.transform);
            var damageRounded = Mathf.RoundToInt(damageAmount);

            if (damageRounded == 0)
            {
                string randomMessage = noDamageMessages[UnityEngine.Random.Range(0, noDamageMessages.Length)];
                go.GetComponent<TextMeshPro>().text = randomMessage;
            }
            else
            {
                go.GetComponent<TextMeshPro>().text = damageRounded.ToString();
            }
        }
    }
    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " est mort !");
        SpawnParticles();
        OnEnemyDied?.Invoke();
        Destroy(gameObject);
    }

    void SpawnParticles()
    {
        GameObject particleSystemInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        ParticleSystem ps = particleSystemInstance.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Play();
        }

        Destroy(particleSystemInstance, ps.main.duration);
    }
}
