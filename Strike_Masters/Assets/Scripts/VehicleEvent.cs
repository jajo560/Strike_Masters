using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleEvent : MonoBehaviour
{
    public float speed = 10f;
    public List<GameObject> vehiclePrefabs;
    public Transform[] spawnPoints;
    public Transform[] destinationPoints;
    public float lifeTime = 10f;
    public AudioSource audioSource;
    public List<AudioClip> hornSounds;

    private AudioClip lastHornSound;

    public void StartSpawning()
    {
        InvokeRepeating("SpawnVehicle", 1f, 2f);
    }

    void SpawnVehicle()
    {
        PlayRandomHornSound();
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        int prefabIndex = Random.Range(0, vehiclePrefabs.Count);

        Transform spawnPoint = spawnPoints[spawnIndex];
        Transform destinationPoint = destinationPoints[spawnIndex];

        GameObject vehicle = Instantiate(vehiclePrefabs[prefabIndex], spawnPoint.position, Quaternion.identity);

        Vector3 directionToDestination = (destinationPoint.position - spawnPoint.position).normalized;

        vehicle.transform.rotation = Quaternion.LookRotation(directionToDestination);

        Rigidbody rb = vehicle.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = directionToDestination * speed;
        }

        Destroy(vehicle, lifeTime);
    }

    void PlayRandomHornSound()
    {
        AudioClip selectedSound = hornSounds[Random.Range(0, hornSounds.Count)];

        while (selectedSound == lastHornSound)
        {
            selectedSound = hornSounds[Random.Range(0, hornSounds.Count)];
        }

        audioSource.PlayOneShot(selectedSound);
        lastHornSound = selectedSound;
    }
}