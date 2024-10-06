using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject unitPrefab;
    public BoxCollider[] spawnAreas;
    public int numberOfUnitsToSpawn = 5;
    public float spawnHeight = 0f;
    public Transform parent;
    public Collider exclusionZone; // Zone � �viter lors du spawn

    void Start()
    {
        for (int i = 0; i < numberOfUnitsToSpawn; i++)
        {
            SpawnUnit();
        }
    }

    void SpawnUnit()
    {
        BoxCollider selectedArea = spawnAreas[Random.Range(0, spawnAreas.Length)];

        Vector3 randomPosition;

        // G�n�rer une position valide
        do
        {
            randomPosition = new Vector3(
                Random.Range(selectedArea.bounds.min.x, selectedArea.bounds.max.x),
                spawnHeight,
                Random.Range(selectedArea.bounds.min.z, selectedArea.bounds.max.z)
            );
        }
        while (IsInsideExclusionZone(randomPosition)); // V�rifier si la position est � l'int�rieur de la zone d'exclusion

        Instantiate(unitPrefab, randomPosition, Quaternion.identity, parent);
    }

    bool IsInsideExclusionZone(Vector3 position)
    {
        return exclusionZone != null && exclusionZone.bounds.Contains(position);
    }
}
