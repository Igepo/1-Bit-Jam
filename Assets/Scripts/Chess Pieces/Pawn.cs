using UnityEngine;

public class Pawn : ChessPiece
{
    private float _moveTimer;
    private float terrainLength;

    private void Start()
    {
    }

    void Update()
    {
    }
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.gameObject.CompareTag("Ally"))
        {
            Debug.Log("Collision avec un Ally d�tect�e !");

            // Par exemple, vous pouvez stopper l'agent ou changer son comportement
            // Ex : Prendre une action sp�cifique ou annuler des d�g�ts
        }
    }

    public override void Move()
    {
    }
}
