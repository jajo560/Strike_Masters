using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSpawner : MonoBehaviour
{
    public GameObject trainPrefab;
    public Transform[] spawnPoints;

    public float timeBetweenTrains = 20f;
    private float timer;
    private bool isSpawning = false;

    void Start()
    {
        timer = timeBetweenTrains;
    }

    void Update()
    {
        if (!isSpawning) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnTrain();
            timer = timeBetweenTrains;
        }
    }

    void SpawnTrain()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];
        Instantiate(trainPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public void StartSpawning()
    {
        isSpawning = true;
        timer = 0f;
    }
}
