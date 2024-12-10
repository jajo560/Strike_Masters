using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement2 : MonoBehaviour
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
        // Lectura del movimiento del jugador
        horizontal = Input.GetAxisRaw("Horizontal_2");
        vertical = Input.GetAxisRaw("Vertical_2");

        movement = new Vector3(horizontal, 0, vertical);

        rbPlayer.velocity += movement * speed * Time.deltaTime;

        // Actualización de la rotación
        if (movement.magnitude > 0.1f)
        {
            lastRotation = Quaternion.LookRotation(movement);
        }
        transform.rotation = lastRotation;

        // Comprobar si el jugador puede chutar
        if (ballHolder != null && ballHolder.isPossessed && ballHolder.transform.parent == transform)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                KickBall();
            }
        }
    }

    void KickBall()
    {
        if (ballHolder != null)
        {
            // Dirección basada en la orientación del jugador
            Vector3 kickDirection = transform.forward;
            ballHolder.KickTheBall(kickDirection, kickForce);
        }
    }
}
