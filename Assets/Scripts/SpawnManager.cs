using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject unitPrefab;
    public BoxCollider[] spawnAreas;
    public int initialSpawnCount = 5; // Nombre d'unit�s � spawn au d�but
    public float spawnHeight = 0f;
    public Transform parent;
    public Collider exclusionZone; // Zone � �viter lors du spawn
    public float spawnInterval = 2f; // Intervalle entre les spawns
    public int maxUnits = 20; // Limite maximale d'unit�s

    private float spawnTimer = 0f; // Compteur pour g�rer le timing des spawns
    private int currentUnitCount = 0; // Compteur d'unit�s cr��es

    void Start()
    {
        for (int i = 0; i < initialSpawnCount; i++)
        {
            SpawnUnit();
        }
    }

    void Update()
    {
        spawnTimer += Time.deltaTime; // Incr�menter le timer � chaque frame

        if (spawnTimer >= spawnInterval)
        {
            if (currentUnitCount < maxUnits)
            {
                SpawnUnit();
                spawnTimer = 0f; // R�initialiser le timer
            }
        }
    }

    private void SpawnUnit()
    {
        BoxCollider selectedArea = spawnAreas[Random.Range(0, spawnAreas.Length)];
        Vector3 randomPosition = GenerateValidPosition(selectedArea);

        if (randomPosition != Vector3.zero) // Assurez-vous que nous avons trouv� une position valide
        {
            Instantiate(unitPrefab, randomPosition, Quaternion.identity, parent);
            currentUnitCount++; // Incr�menter le compteur d'unit�s
        }
    }

    private Vector3 GenerateValidPosition(BoxCollider selectedArea)
    {
        for (int attempt = 0; attempt < 100; attempt++) // Limiter les tentatives pour �viter une boucle infinie
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(selectedArea.bounds.min.x, selectedArea.bounds.max.x),
                spawnHeight,
                Random.Range(selectedArea.bounds.min.z, selectedArea.bounds.max.z)
            );

            if (!IsInsideExclusionZone(randomPosition))
            {
                return randomPosition; // Retourner une position valide
            }
        }

        return Vector3.zero; // Retourner un vecteur nul si aucune position valide n'est trouv�e
    }

    private bool IsInsideExclusionZone(Vector3 position)
    {
        return exclusionZone != null && exclusionZone.bounds.Contains(position);
    }

    private void OnDisable()
    {
        // Optionnel : vous pouvez ajouter une logique ici pour nettoyer ou arr�ter le spawn si n�cessaire
    }
}
