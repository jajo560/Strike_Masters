using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public string horizontalInput = "Horizontal";
    public string verticalInput = "Vertical";
    public KeyCode kickKey = KeyCode.Space;
    public float speed = 5f;
    public float kickForce = 10f;

    private Rigidbody rbPlayer;
    private Quaternion lastRotation;
    private Vector3 movement;
    public Animator animator;

    public BallHolder ballHolder;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
        lastRotation = transform.rotation;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw(horizontalInput);
        float vertical = Input.GetAxisRaw(verticalInput);

        movement = new Vector3(horizontal, 0, vertical).normalized;

        rbPlayer.velocity += movement * speed * Time.deltaTime;

        if (movement.magnitude > 0.1f)
        {
            if (animator != null)
            {
                animator.SetBool("isRunning", true);
            }
            lastRotation = Quaternion.LookRotation(movement);
        }
        else
        {
            if (animator != null)
            {
                animator.SetBool("isRunning", false);
            }
        }
        transform.rotation = lastRotation;

        if (ballHolder != null && ballHolder.isPossessed && ballHolder.currentPlayer == gameObject)
        {
            if (Input.GetKeyDown(kickKey))
            {
                KickBall();
                Debug.Log("CHUT");
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
