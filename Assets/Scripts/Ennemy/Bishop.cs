using UnityEngine;

public class Bishop : ChessPiece
{
    private Vector3 _moveDirection;

    private void Start()
    {
        InvokeRepeating(nameof(ChangeDirection), 0, chessPieceData.changeDirectionTime);
    }

    private void ChangeDirection()
    {
        _moveDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        if (Mathf.Abs(_moveDirection.x) < Mathf.Abs(_moveDirection.z))
        {
            _moveDirection.x = _moveDirection.z > 0 ? 1 : -1;
        }
        else
        {
            _moveDirection.z = _moveDirection.x > 0 ? 1 : -1;
        }
    }

    public override void Move()
    {
        _rigidbody.MovePosition(transform.position + _moveDirection * chessPieceData.moveSpeed * Time.deltaTime);
    }
}
