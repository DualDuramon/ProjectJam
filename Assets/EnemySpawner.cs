using NUnit.Framework;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int nowMobCount;
    [SerializeField] private int maxMobCount;
    [SerializeField] private float spawnTime;
    [SerializeField] private int startMobSpawnAmount;

    private float nowSpawnTime;
    private Coroutine spawnCoroutine;

    public void StartSpawn()
    {
        nowMobCount = 0;
        nowSpawnTime = 0.0f;

        for(int i = 0; i < startMobSpawnAmount; i++)
        {
            SpawnEnemyAtRandomLocation();
        }

        spawnCoroutine = StartCoroutine(SpawnStartCoroutine());
    }

    IEnumerator SpawnStartCoroutine()
    {
        Debug.Log("Spawn Start");
        while(nowMobCount < maxMobCount)
        {
            if(nowSpawnTime < spawnTime)
            {
                nowSpawnTime += Time.deltaTime;
            }
            else
            {
                SpawnEnemyAtRandomLocation();
            }

            yield return null;
        }
    }

    private void SpawnEnemyAtRandomLocation()
    {
        int randomIdx = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefab, spawnPoints[randomIdx].position, spawnPoints[randomIdx].rotation);
        nowSpawnTime = 0.0f;
        nowMobCount++;
    }

    public void StopSpawn()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }
}
