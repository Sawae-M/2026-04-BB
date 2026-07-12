using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    public float spawnInterval = 10f;
    public Transform[] spawnPoints;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        Vector3 spawnPos = spawnPoint.position;
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
