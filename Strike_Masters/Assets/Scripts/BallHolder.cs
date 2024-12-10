using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHolder : MonoBehaviour
{
    public Transform[] currentHolder;
    public GameObject currentPlayer;    
    public GameObject[] players;    
    private Rigidbody rb;

    public float followSpeed = 10f;
    public float possessionRange = 2f;
    public bool isPossessed = false;
    public float possessionSwitchDelay = 0.5f;
    private float lastPossessionTime = -0.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (isPossessed && currentPlayer != null)
        {
            Transform holderTransform = GetCurrentHolderTransform();
            if (holderTransform != null)
            {
                transform.position = Vector3.Lerp(transform.position, holderTransform.position, followSpeed * Time.fixedDeltaTime);
                transform.rotation = holderTransform.rotation;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (collision.gameObject == players[i])
            {
                TryAssignPossession(players[i], currentHolder[i]);
                break;
            }
        }
    }
    private void TryAssignPossession(GameObject player, Transform holder)
    {
        if (Time.time >= lastPossessionTime + possessionSwitchDelay)
        {
            AssignPossession(player, holder);
        }
    }
    private void AssignPossession(GameObject player, Transform holder)
    {
        isPossessed = true;
        currentPlayer = player;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = holder.position;
        transform.rotation = holder.rotation;

        lastPossessionTime = Time.time;
    }
    private Transform GetCurrentHolderTransform()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == currentPlayer)
            {
                return currentHolder[i];
            }
        }       
        return null;

    }
    public void KickTheBall(Vector3 kickDirection, float kickForce)
    {
        ReleaseBall();
        rb.AddForce(kickDirection.normalized * kickForce, ForceMode.Impulse);
    }
    private void ReleaseBall()
    {
        isPossessed = false;
        currentPlayer = null;
    }
}
