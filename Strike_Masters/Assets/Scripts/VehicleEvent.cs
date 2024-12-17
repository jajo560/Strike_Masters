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

    void Start()
    {
        InvokeRepeating("SpawnVehicle", 1f, 3f);  // Llamar a SpawnVehicle cada 3 segundos
    }

    void SpawnVehicle()
    {
        int spawnIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
        int destinationIndex = UnityEngine.Random.Range(0, destinationPoints.Length);

        // Crear el veh�culo en el punto de spawn
        GameObject vehicle = Instantiate(vehiclePrefab, spawnPoints[spawnIndex].position, Quaternion.Euler(0, 180, 0));

        Rigidbody rb = vehicle.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Calcular la direcci�n hacia el destino
            Vector3 directionToDestination = (destinationPoints[destinationIndex].position - vehicle.transform.position).normalized;

            // Aplicar una fuerza constante en la direcci�n hacia el destino
            rb.AddForce(directionToDestination * speed, ForceMode.VelocityChange);
        }

        // Destruir el veh�culo despu�s de un tiempo determinado
        Destroy(vehicle, lifeTime);
    }
}
