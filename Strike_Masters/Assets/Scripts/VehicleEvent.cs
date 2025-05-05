using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleEvent : MonoBehaviour
{
    public float speed = 10f;
    public GameObject vehiclePrefab;
    public Transform[] spawnPoints;
    public Transform[] destinationPoints;
    public float lifeTime = 10f;
    public void StartSpawning()
    {
        InvokeRepeating("SpawnVehicle", 1f, 2f);
    }
    void SpawnVehicle()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);

        Transform spawnPoint = spawnPoints[spawnIndex];
        Transform destinationPoint = destinationPoints[spawnIndex];

        GameObject vehicle = Instantiate(vehiclePrefab, spawnPoint.position, Quaternion.identity);

        Vector3 directionToDestination = (destinationPoint.position - spawnPoint.position).normalized;

        vehicle.transform.rotation = Quaternion.LookRotation(directionToDestination);

        Rigidbody rb = vehicle.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = directionToDestination * speed;
        }

        Destroy(vehicle, lifeTime);
    }


}
