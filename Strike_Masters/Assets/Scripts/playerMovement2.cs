using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class playerMovement2 : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    public float speed;
    public float kickForce = 10f;
    public bool isPossessed = false;

    private BallHolder ballHolder;
    public Rigidbody rbPlayer;
    private Quaternion lastRotation;
    private Vector3 movement;
    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        lastRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal_2");
        vertical = Input.GetAxisRaw("Vertical_2");

        movement = new Vector3(horizontal, 0, vertical);

        rbPlayer.velocity += movement * speed * Time.deltaTime;

        if (movement.magnitude > 0.1f)
        {
            lastRotation = Quaternion.LookRotation(movement);
        }
        transform.rotation = lastRotation;

        if (Input.GetKeyDown(KeyCode.Space) && isPossessed)
        {
            Debug.Log("FFFFF");
            KickBall();
        }

    }

    void KickBall()
    {
        if (ballHolder != null && ballHolder.currentHolder == transform)
        {
            Vector3 kickDirection = transform.forward;
            ballHolder.KickTheBall(kickDirection, kickForce);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            isPossessed = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            isPossessed = false;
        }
    }

}
