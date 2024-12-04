using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHolder : MonoBehaviour
{
    public Transform currentHolder;
    public float followSpeed = 10f;
    private Rigidbody rb;
    public float possessionRange = 0.5f;
    public bool isPossessed = false;
    public GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= possessionRange && currentHolder != null)
        {
            PossessBall();
        }
        else if (isPossessed == false)
        {
            ReleaseBall();
        }
    }

    private void PossessBall()
    {
        isPossessed = true;
        transform.SetParent(currentHolder);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    private void ReleaseBall()
    {
        isPossessed = false;
        transform.SetParent(null);
        
    }

    public void KickTheBall(Vector3 kickDirection, float kickForce)
    {
        ReleaseBall();
        rb.AddForce(kickDirection.normalized * kickForce, ForceMode.Impulse);
    }
}
