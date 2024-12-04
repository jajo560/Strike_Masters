using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class playerMovement1 : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    public float speed;
    public Rigidbody rbPlayer;
    private Vector3 movement;
    public float kickForce = 10f;
    private BallHolder ballHolder;

    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        movement = (transform.forward * vertical + transform.right * horizontal).normalized;

        rbPlayer.MovePosition(transform.position + movement * speed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        if (Input.GetKeyDown(KeyCode.Space) && ballHolder != null)
        {
            Vector3 kickDirection = transform.forward;
            ballHolder.KickTheBall(kickDirection, kickForce);
        }

    }


}
