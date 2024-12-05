using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class playerMovement1 : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    public float speed;
    public float kickForce = 10f;
    public bool isPossessed = false;

    public BallHolder ballHolder;    
    public Rigidbody rbPlayer;
    private Quaternion lastRotation;
    private Vector3 movement;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        lastRotation = transform.rotation;

    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        movement = new Vector3(horizontal, 0, vertical);

        rbPlayer.velocity += movement * speed * Time.deltaTime;

        if (movement.magnitude > 0.1f)
        {
            lastRotation = Quaternion.LookRotation(movement);
        }
        transform.rotation = lastRotation;

        if (ballHolder != null)
        {
            if (ballHolder.isPossessed == true && Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("FFFFF");
                KickBall();
            }
        }

    }

    void KickBall()
    {
        if (ballHolder != null)
        {
            Vector3 kickDirection = transform.forward;
            ballHolder.KickTheBall(kickDirection, kickForce);
        }
    }


}
