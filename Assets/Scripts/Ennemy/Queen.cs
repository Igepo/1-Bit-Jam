using UnityEngine;

public class Queen : ChessPiece
{
    private Vector3 _moveDirection;

    private void Start()
    {
        InvokeRepeating(nameof(ChangeDirection), 0, chessPieceData.changeDirectionTime);
    }

    private void ChangeDirection()
    {
        _moveDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }

    public override void Move()
    {
        _rigidbody.MovePosition(transform.position + _moveDirection * chessPieceData.moveSpeed * Time.deltaTime);
    }
}
