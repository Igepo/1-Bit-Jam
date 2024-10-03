using UnityEngine;

public class Pawn : ChessPiece
{
    private float _moveTimer;
    private float terrainLength;
    private GameObject backgroundObject;

    private void Start()
    {
        backgroundObject = GameObject.Find("Background");

        if (backgroundObject != null)
            terrainLength = backgroundObject.transform.localScale.x;

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
        if (backgroundObject != null)
        {
            float moveDistance = terrainLength / 8f;

            Vector3 moveDirection = -transform.up * moveDistance;

            _rigidbody.MovePosition(transform.position + moveDirection);
        }
    }
}
