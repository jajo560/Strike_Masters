using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHolder : MonoBehaviour
{
    public Transform[] currentHolder;
    public GameObject currentPlayer;
    public float followSpeed = 10f;
    private Rigidbody rb;
    public float possessionRange = 2f;
    public bool isPossessed = false;
    public GameObject[] players;
    public float possessionSwitchDelay = 0.5f;
    private float lastPossessionTime = -1f;
    private bool canBePossessed = true;
    public float possessionCooldown = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        UpdatePossession();

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

    private void UpdatePossession()
    {
        float distanceToPlayer1 = Vector3.Distance(transform.position, players[0].transform.position);
        float distanceToPlayer2 = Vector3.Distance(transform.position, players[1].transform.position);

        bool player1InRange = distanceToPlayer1 <= possessionRange;
        bool player2InRange = distanceToPlayer2 <= possessionRange;

        if (canBePossessed && Time.time >= lastPossessionTime + possessionSwitchDelay)
        {
            if (player1InRange && player2InRange)
            {
                if (distanceToPlayer1 < distanceToPlayer2)
                {
                    AssignPossession(players[0], currentHolder[0]);
                }
                else
                {
                    AssignPossession(players[1], currentHolder[1]);
                }
            }
            else if (player1InRange)
            {
                AssignPossession(players[0], currentHolder[0]);
            }
            else if (player2InRange)
            {
                AssignPossession(players[1], currentHolder[1]);
            }
            else
            {
                ReleaseBall();
            }
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

    private void ReleaseBall()
    {
        isPossessed = false;
        currentPlayer = null;

    }

    public void KickTheBall(Vector3 kickDirection, float kickForce)
    {
        ReleaseBall();
        rb.AddForce(kickDirection.normalized * kickForce, ForceMode.Impulse);
        StartCoroutine(StartPossessionCooldown());
    }

    private Transform GetCurrentHolderTransform()
    {
        if (currentPlayer != null)
        {
            int index = System.Array.IndexOf(players, currentPlayer);
            if (index >= 0 && index < currentHolder.Length)
            {
                return currentHolder[index];
            }
        }
        return null;
    }

    private IEnumerator StartPossessionCooldown()
    {
        canBePossessed = false;
        yield return new WaitForSeconds(possessionCooldown);
        canBePossessed = true;
    }
}
