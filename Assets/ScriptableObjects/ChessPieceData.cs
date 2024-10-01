using UnityEngine;

[CreateAssetMenu(fileName = "NewChessPiece", menuName = "Chess Piece Data")]
public class ChessPieceData : ScriptableObject
{
    public string pieceName;
    public float moveSpeed;
    public float changeDirectionTime;
    public float maxHealth;
    public Vector3[] validMoves;

    // M�thode pour afficher des informations sur la pi�ce
    public string GetInfo()
    {
        return $"{pieceName}: Speed = {moveSpeed}, Change Direction Time = {changeDirectionTime}";
    }
}