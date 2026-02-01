using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuSpawner : MonoBehaviour
{
     [Header("Prefabs")]
    public List<GameObject> prefabs = new List<GameObject>();

    [Header("Spawn Settings")]
    public float spawnInterval = 1.0f;
    public float destroyAfter = 5.0f;

    [Header("Spawn Area")]
    public Vector3 spawnArea = new Vector3(1, 0, 1);

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnRandomPrefab();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnRandomPrefab()
    {
        if (prefabs.Count == 0)
            return;

        // Pick random prefab
        GameObject prefab =
            prefabs[Random.Range(0, prefabs.Count)];

        // Random position
        Vector3 position = transform.position +
            new Vector3(
                Random.Range(-spawnArea.x / 2f, spawnArea.x / 2f),
                Random.Range(-spawnArea.y / 2f, spawnArea.y / 2f),
                Random.Range(-spawnArea.z / 2f, spawnArea.z / 2f)
            );

        // Spawn
        GameObject obj = Instantiate(
            prefab,
            position,
            Quaternion.identity
        );

        // Auto destroy
        Destroy(obj, destroyAfter);
    }
}
