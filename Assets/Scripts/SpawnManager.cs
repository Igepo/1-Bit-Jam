using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject unitPrefab;
    public BoxCollider[] spawnAreas;
    public int numberOfUnitsToSpawn = 5;
    public float spawnHeight = 0f;
    public Transform parent;

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

        Vector3 randomPosition = new Vector3(
            Random.Range(selectedArea.bounds.min.x, selectedArea.bounds.max.x),
            spawnHeight,
            Random.Range(selectedArea.bounds.min.z, selectedArea.bounds.max.z)
        );

        var toto = Instantiate(unitPrefab, randomPosition, Quaternion.identity, parent);
    }
}
