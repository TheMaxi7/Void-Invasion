using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public float cdWaveTimer = 5f;
    private float cd = 2f;
    public Transform spawnPoint;
    private int waveNumber = 1;
    private void Update()
    {
        if (cd <= 0)
        {
            StartCoroutine(SpawnWave());
            cd = cdWaveTimer;
        }

        cd -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
        waveNumber++;
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}

