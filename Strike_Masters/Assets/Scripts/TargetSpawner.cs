using System.Collections;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;
    public float spawnInterval = 2f;
    public Vector3 spawnAreaMin;
    public Vector3 spawnAreaMax;
    public int maxTargets = 2;

    private int currentTargets = 0;
    private bool isSpawning = false;
    private Coroutine spawnCoroutine;
    public AudioClip hitSound;
    private AudioSource audioSource;
    public float minSpawnDistance = 2f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
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
                Vector3 spawnPos;
                bool validPosition = false;
                int attempts = 0;
                int maxAttempts = 10;

                do
                {
                    attempts++;
                    spawnPos = new Vector3(
                        Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                        Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                        Random.Range(spawnAreaMin.z, spawnAreaMax.z)
                    );

                    validPosition = true;

                    foreach (Target target in FindObjectsOfType<Target>())
                    {
                        if (Vector3.Distance(target.transform.position, spawnPos) < minSpawnDistance)
                        {
                            validPosition = false;
                            break;
                        }
                    }

                } while (!validPosition && attempts < maxAttempts);

                if (validPosition)
                {
                    Quaternion randomYRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
                    GameObject newTarget = Instantiate(targetPrefab, spawnPos, randomYRotation);
                    Target targetScript = newTarget.GetComponent<Target>();
                    targetScript.spawner = this;
                    currentTargets++;
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void TargetDestroyed()
    {
        currentTargets--;
    }
    public void PlayHitSound()
    {
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}
