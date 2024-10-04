using UnityEngine;

public class King : ChessPiece
{
    private float _moveTimer;
    private float terrainLength;
    private GameObject backgroundObject;

    private void Start()
    {

    }

    void Update()
    {

    }

    protected override void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision avec un Ennemy détectée !");

        if (collision.gameObject.CompareTag("Ennemy"))
        {
            Debug.Log("Collision avec un Ennemy détectée !");

            // Par exemple, vous pouvez stopper l'agent ou changer son comportement
            // Ex : Prendre une action spécifique ou annuler des dégâts
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                Vector3 impactForce = collision.relativeVelocity * playerRigidbody.mass;
                var impactForceClamped = Vector3.ClampMagnitude(impactForce, 10000f);
                playerRigidbody.AddForce(-impactForceClamped.normalized * impactForceClamped.magnitude * 0.5f, ForceMode.Impulse);
            }

            return;
        }

        base.OnCollisionEnter(collision);
    }
    public override void Move()
    {
    }
}
