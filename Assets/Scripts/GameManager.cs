using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pawnPrefab;
    [SerializeField] private GameObject rookPrefab;
    [SerializeField] private GameObject knightPrefab;
    [SerializeField] private GameObject bishopPrefab; // Ajout pour le Fou
    [SerializeField] private GameObject queenPrefab;  // Ajout pour la Reine
    [SerializeField] private GameObject kingPrefab;   // Ajout pour le Roi

    void Start()
    {
        // Instancier des pions
        for (int i = 0; i < 8; i++)
        {
            Instantiate(pawnPrefab, new Vector3(i, 0, 1), Quaternion.identity);
        }

        //// Instancier des tours
        //Instantiate(rookPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        //Instantiate(rookPrefab, new Vector3(7, 0, 0), Quaternion.identity);

        //// Instancier des cavaliers
        //Instantiate(knightPrefab, new Vector3(1, 0, 0), Quaternion.identity);
        //Instantiate(knightPrefab, new Vector3(6, 0, 0), Quaternion.identity);

        //// Instancier des fous
        //Instantiate(bishopPrefab, new Vector3(2, 0, 0), Quaternion.identity);
        //Instantiate(bishopPrefab, new Vector3(5, 0, 0), Quaternion.identity);

        //// Instancier la reine
        //Instantiate(queenPrefab, new Vector3(3, 0, 0), Quaternion.identity);

        //// Instancier le roi
        //Instantiate(kingPrefab, new Vector3(4, 0, 0), Quaternion.identity);
    }
}
