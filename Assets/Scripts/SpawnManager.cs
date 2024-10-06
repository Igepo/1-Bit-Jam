using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject unitPrefab;
    public BoxCollider[] spawnAreas;
    public int numberOfUnitsToSpawn = 5;
    public float spawnHeight = 0f;
    public Transform parent;
    public Collider exclusionZone; // Zone à éviter lors du spawn

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

        // Générer une position valide
        do
        {
            randomPosition = new Vector3(
                Random.Range(selectedArea.bounds.min.x, selectedArea.bounds.max.x),
                spawnHeight,
                Random.Range(selectedArea.bounds.min.z, selectedArea.bounds.max.z)
            );
        }
        while (IsInsideExclusionZone(randomPosition)); // Vérifier si la position est à l'intérieur de la zone d'exclusion

        Instantiate(unitPrefab, randomPosition, Quaternion.identity, parent);
    }

    bool IsInsideExclusionZone(Vector3 position)
    {
        return exclusionZone != null && exclusionZone.bounds.Contains(position);
    }
}
