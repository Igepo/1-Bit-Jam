using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject pawnPrefab;
    public GameObject tankPawnPrefab;

    [Header("Spawn Settings")]
    public BoxCollider[] spawnAreas;
    public float spawnHeight = 0f;
    public Transform parent;
    public Collider exclusionZone;

    [Header("Wave Settings")]
    public float spawnInterval = 2f;
    public float waveInterval = 5f;
    public int maxUnits = 20;
    public int maxPawnsPerWave = 6;
    public int minTankPawnsPerWave = 1;
    public int maxTankPawnsPerWave = 2;
    public float spawnAreaRadius = 5f;

    private int currentUnitCount = 0;
    private Vector3 waveSpawnCenter;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            if (currentUnitCount < maxUnits)
            {
                // Determine the number of enemies to spawn for this wave
                int enemiesToSpawn = Random.Range(5, maxPawnsPerWave + 1);
                int tanksToSpawn = Random.Range(minTankPawnsPerWave, maxTankPawnsPerWave + 1);
                int pawnsToSpawn = enemiesToSpawn - tanksToSpawn;

                // Generate a central position for the wave
                waveSpawnCenter = GenerateWaveCenter();

                // Spawn tanks
                for (int i = 0; i < tanksToSpawn; i++)
                {
                    SpawnUnit(tankPawnPrefab);
                    yield return new WaitForSeconds(spawnInterval);
                }

                // Spawn pawns
                for (int i = 0; i < pawnsToSpawn; i++)
                {
                    SpawnUnit(pawnPrefab);
                    yield return new WaitForSeconds(spawnInterval);
                }
            }

            // Wait until the next wave
            yield return new WaitForSeconds(waveInterval);
        }
    }

    private void SpawnUnit(GameObject prefab)
    {
        // Generate a random position within the spawn area radius, around the waveSpawnCenter
        Vector3 randomPosition = GenerateValidPositionWithinRadius(waveSpawnCenter);

        if (randomPosition != Vector3.zero)
        {
            Instantiate(prefab, randomPosition, Quaternion.identity, parent);
            currentUnitCount++;
        }
    }

    private Vector3 GenerateWaveCenter()
    {
        BoxCollider selectedArea = spawnAreas[Random.Range(0, spawnAreas.Length)];
        for (int attempt = 0; attempt < 100; attempt++)
        {
            Vector3 randomCenter = new Vector3(
                Random.Range(selectedArea.bounds.min.x, selectedArea.bounds.max.x),
                spawnHeight,
                Random.Range(selectedArea.bounds.min.z, selectedArea.bounds.max.z)
            );

            if (!IsInsideExclusionZone(randomCenter))
            {
                return randomCenter;
            }
        }

        return Vector3.zero;
    }

    private Vector3 GenerateValidPositionWithinRadius(Vector3 center)
    {
        for (int attempt = 0; attempt < 100; attempt++)
        {
            Vector3 randomPosition = center + new Vector3(
                Random.Range(-spawnAreaRadius, spawnAreaRadius),
                0,
                Random.Range(-spawnAreaRadius, spawnAreaRadius)
            );

            if (!IsInsideExclusionZone(randomPosition))
            {
                return randomPosition;
            }
        }

        return Vector3.zero;
    }

    private bool IsInsideExclusionZone(Vector3 position)
    {
        return exclusionZone != null && exclusionZone.bounds.Contains(position);
    }

    public void OnUnitDestroyed()
    {
        currentUnitCount--;
    }

    private void OnEnable()
    {
        Pawn.OnPawnDie += OnUnitDestroyed;
    }
    private void OnDisable()
    {
        Pawn.OnPawnDie -= OnUnitDestroyed;

        StopAllCoroutines();
    }
}
