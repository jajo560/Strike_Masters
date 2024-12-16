using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRebound : MonoBehaviour
{
    private Rigidbody rb;

    public PhysicMaterial bounceMaterial;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("InvisibleWall"))
        {
            Collider ballCollider = GetComponent<Collider>();
            ballCollider.material = bounceMaterial;
        }
    }
}
