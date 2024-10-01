using UnityEngine;
using UnityEngine.UIElements;

public abstract class ChessPiece : MonoBehaviour
{
    protected Rigidbody _rigidbody;
    public ChessPieceData chessPieceData;
    //public Slider healthSlider;

    private float currentHealth;
    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        currentHealth = chessPieceData.maxHealth;
    }

    public abstract void Move();

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                float impactVelocity = collision.relativeVelocity.magnitude;
                float damage = CalculateDamage(impactVelocity);
                TakeDamage(damage);

                Vector3 impactForce = collision.relativeVelocity * playerRigidbody.mass;
                impactForce = Vector3.ClampMagnitude(impactForce, 1000f);

                playerRigidbody.AddForce(-impactForce.normalized * impactForce.magnitude * 0.5f, ForceMode.Impulse);

                playerRigidbody.velocity = Vector3.zero;
                Player playerScript = collision.gameObject.GetComponent<Player>();
                if (playerScript != null)
                {
                    playerScript.StopPlayer();
                }
            }
        }
    }
    protected float CalculateDamage(float impactVelocity)
    {
        var velocityThreshold = 40f;
        if (impactVelocity > velocityThreshold)
            return impactVelocity / 1.5f; // Valeur à redefinir
        else
            return 0; // Pas assez de vitesse à l'imapct
    }

    public virtual void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log(gameObject.name + " a reçu " + damageAmount + " de dégâts. Vie restante : " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        Debug.Log(gameObject.name + " est mort !");
        Destroy(gameObject);
    }
}
