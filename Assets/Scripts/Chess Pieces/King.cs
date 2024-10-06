using UnityEngine;
using UnityEngine.UI;

public class King : ChessPiece
{
    private float _moveTimer;
    private float terrainLength;
    private GameObject backgroundObject;
    public Slider healthSlider;

    private void Start()
    {
        healthSlider.value = 1;
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
                playerRigidbody.AddForce(-impactForceClamped.normalized * impactForceClamped.magnitude * 0.5f, ForceMode.Impulse);
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
        Debug.Log(gameObject.name + " a reçu " + kingDamageReceive + " de dégâts. Vie restante : " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected override void Die()
    {
        Debug.Log("Le roi est mort ! ");
        healthSlider.gameObject.SetActive(false);
        //SpawnParticles();
        //OnEnemyDied?.Invoke();
        //Destroy(gameObject);
    }

    public override void Move()
    {
    }
}
