using System.Collections;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;
    public float spawnInterval = 1.5f;
    public Vector3 spawnAreaMin;
    public Vector3 spawnAreaMax;
    public int maxTargets = 5;

    private int currentTargets = 0;
    private bool isSpawning = false;
    private Coroutine spawnCoroutine;

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            spawnCoroutine = StartCoroutine(SpawnTargets());
        }
    }

    IEnumerator SpawnTargets()
    {
        while (true)
        {
            if (currentTargets < maxTargets)
            {
                Vector3 spawnPos = new Vector3(
                    Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                    Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                    Random.Range(spawnAreaMin.z, spawnAreaMax.z)
                );

                Quaternion randomYRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
                GameObject newTarget = Instantiate(targetPrefab, spawnPos, randomYRotation); Target targetScript = newTarget.GetComponent<Target>();
                targetScript.spawner = this;
                currentTargets++;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void TargetDestroyed()
    {
        currentTargets--;
    }
}
