using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHolder : MonoBehaviour
{
    public Transform[] currentHolder;
    public float followSpeed = 10f;
    private Rigidbody rb;
    public float possessionRange;
    public bool isPossessed = false;
    public bool hasBall1 = false;
    public bool hasBall2 = false;
    public GameObject[] player;

    private bool canBePossessed = true;
    public float possessionCooldown = 0.5f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player[0].transform.position);
        float distanceToPlayer2 = Vector3.Distance(transform.position, player[1].transform.position);

        if (canBePossessed && distanceToPlayer <= possessionRange && currentHolder != null)
        {
            PossessBall();
            hasBall1 = true;
        }
        else if (distanceToPlayer > possessionRange && currentHolder != null)
        {
            hasBall1 = false;
        }

        if (canBePossessed && distanceToPlayer2 <= possessionRange && currentHolder != null)
        {
            PossessBall();
            hasBall2 = true;
        }
        else if (distanceToPlayer2 > possessionRange && currentHolder != null)
        {
            hasBall2 = false;
        }

        if (isPossessed == false)
        {
            ReleaseBall();
        }

    }

    private void PossessBall()
    {
        isPossessed = true;

        if (hasBall1)
        {
            transform.SetParent(currentHolder[0]);
        }

        if (hasBall2)
        {
            transform.SetParent(currentHolder[1]);
        }

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

    }

    private void ReleaseBall()
    {
        Debug.Log("SALIO");
        isPossessed = false;
        transform.SetParent(null);
        
    }

    public void KickTheBall(Vector3 kickDirection, float kickForce)
    {
        ReleaseBall();
        rb.AddForce(kickDirection.normalized * kickForce, ForceMode.Impulse);
        StartCoroutine(StartPossessionCooldown());
    }

    private IEnumerator StartPossessionCooldown()
    {
        canBePossessed = false;
        yield return new WaitForSeconds(possessionCooldown);
        canBePossessed = true;
    }

}
