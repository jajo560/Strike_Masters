using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class playerMovement2 : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    public float speed;
    public Rigidbody rb;
    private Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        movement = new Vector3(horizontal, 0, vertical);

        rb.velocity += movement * speed * Time.deltaTime;

        transform.rotation = Quaternion.LookRotation(movement);


    }
}
