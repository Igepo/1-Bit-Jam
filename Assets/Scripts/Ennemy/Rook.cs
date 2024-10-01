using UnityEngine;

public class Rook : ChessPiece
{
    private Vector3 _moveDirection;

    private void Start()
    {
        InvokeRepeating(nameof(ChangeDirection), 0, chessPieceData.changeDirectionTime);
    }

    private void ChangeDirection()
    {
        _moveDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        if (_moveDirection.x != 0) _moveDirection.z = 0;
    }

    public override void Move()
    {
        _rigidbody.MovePosition(transform.position + _moveDirection * chessPieceData.moveSpeed * Time.deltaTime);
    }
}
