using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHolder : MonoBehaviour
{
    public Transform currentHolder;
    public float followSpeed = 10f;
    private Rigidbody rb;
    public float possessionRange = 1.5f;
    public playerMovement1 player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        if (player != null)
        {
            float distanceToBall = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToBall <= possessionRange)
            {
                if (currentHolder != null)
                {
                    Vector3 targetPosition = currentHolder.transform.position;
                    rb.MovePosition(Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.fixedDeltaTime));
                    rb.velocity = Vector3.zero;
                }
            }
        }
    }

    public void KickTheBall(Vector3 kickDirection, float kickForce)
    {
        currentHolder = null;
        rb.isKinematic = false;
        rb.AddForce(kickDirection.normalized * kickForce, ForceMode.Impulse);
    }
}
