using UnityEngine;

public class King : ChessPiece
{
    private Vector3[] _moves = {
        new Vector3(1, 0, 0),
        new Vector3(-1, 0, 0),
        new Vector3(0, 0, 1),
        new Vector3(0, 0, -1),
        new Vector3(1, 0, 1),
        new Vector3(1, 0, -1),
        new Vector3(-1, 0, 1),
        new Vector3(-1, 0, -1)
    };

    private void Start()
    {
        InvokeRepeating(nameof(Move), 0, 2f);
    }

    public override void Move()
    {
        int randomIndex = Random.Range(0, _moves.Length);
        Vector3 move = _moves[randomIndex] * chessPieceData.moveSpeed;
        _rigidbody.MovePosition(transform.position + move * Time.deltaTime);
    }
}
