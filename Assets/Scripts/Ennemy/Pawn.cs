using UnityEngine;

public class Pawn : ChessPiece
{
    private float _moveTimer;

    private void Start()
    {
        _moveTimer = chessPieceData.changeDirectionTime;
    }

    void Update()
    {
        _moveTimer -= Time.deltaTime;
        if (_moveTimer <= 0)
        {
            Move();
            _moveTimer = chessPieceData.changeDirectionTime;
        }
    }

    public override void Move()
    {
        _rigidbody.MovePosition(transform.position + transform.forward * chessPieceData.moveSpeed * Time.deltaTime);
    }
}
