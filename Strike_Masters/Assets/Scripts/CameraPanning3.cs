using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanning3 : MonoBehaviour
{
    public Transform[] waypoints;
    public float panSpeed = 2f;
    public float waitTime = 1f;
    private bool hasStartedMatch = false;
    private int currentIndex = 0;
    private float waitCounter = 0f;
    private bool isPanning = true;
    public TrainSpawner trainSpawner;
    public TargetSpawner targetSpawner;
    void Update()
    {
        if (isPanning)
        {
            Transform target = waypoints[currentIndex];
            transform.position = Vector3.MoveTowards(transform.position, target.position, panSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * panSpeed);
            ManagerTarget.Instance.isMatchStarted = false;
            if (!hasStartedMatch)
            {
                ManagerTarget.Instance.StartMatch();
                hasStartedMatch = true;
            }
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                waitCounter += Time.deltaTime;
                if (waitCounter >= waitTime)
                {
                    waitCounter = 0f;
                    currentIndex++;
                    if (currentIndex >= waypoints.Length)
                    {
                        isPanning = false;
                        ManagerTarget.Instance.isMatchStarted = true;
                        trainSpawner.StartSpawning();
                        targetSpawner.StartSpawning();
                    }
                }
            }
        }
    }
}
