using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Goal1 : MonoBehaviour
{
    public GameObject ball;
    public GameObject[] players;
    public Transform[] initialPlayerPositions;
    public Transform centerFieldPosition;
    public BallHolder ballHolder;

    public GameManager gameManager;
    public AudioClip goalSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            Debug.Log("GOOOOOLLLL");
            gameManager.GoalScored(2);
            ResetBallAndPlayers();
            if (goalSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(goalSound);
            }
        }

    }

    private void ResetBallAndPlayers()
    {
        ballHolder.ResetBallPosition();
        ballHolder.ReleaseBall();

        ball.transform.position = centerFieldPosition.position;

        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = initialPlayerPositions[i].position;
        }
    }


}
