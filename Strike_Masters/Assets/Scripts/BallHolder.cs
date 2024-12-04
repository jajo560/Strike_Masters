using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHolder : MonoBehaviour
{
    public Transform currentHolder;
    public float followSpeed = 10f;
    private Rigidbody rb;
    public float possessionRange = 1.5f;
    public bool isPossessed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isPossessed == true && currentHolder != null) { 

            Vector3 targetPosition = currentHolder.transform.position;
            rb.MovePosition(Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.fixedDeltaTime));
        }
    }

    public void KickTheBall(Vector3 kickDirection, float kickForce)
    {
        currentHolder = null;
        rb.isKinematic = false;
        isPossessed = false;
        rb.AddForce(kickDirection.normalized * kickForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            isPossessed = true;   
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            isPossessed = false;
        }
    }

}
