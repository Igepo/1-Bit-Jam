using System;
using UnityEngine;

public class Pawn : ChessPiece
{
    public static event Action OnPawnDie;
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

    }

    public override void Move()
    {
    }

    protected override void Die()
    {
        base.Die();
        OnPawnDie?.Invoke();
    }
}
