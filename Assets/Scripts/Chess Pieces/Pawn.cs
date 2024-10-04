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
            Debug.Log("Collision avec un Ally détectée !");

            // Par exemple, vous pouvez stopper l'agent ou changer son comportement
            // Ex : Prendre une action spécifique ou annuler des dégâts
        }
    }

    public override void Move()
    {
    }
}
