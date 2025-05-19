using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject ball;
    public GameObject[] players;
    public Transform[] initialPlayerPositions;
    public Transform centerFieldPosition;
    public BallHolder ballHolder;
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
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GoalScored(2);
                Debug.Log("GOOOOOLLLL");
            }
            if (ManagerTarget.Instance != null)
            {
                ManagerTarget.Instance.GoalScored(1);
                Debug.Log("GOOOOOLLLL 222");
            }
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
        ballHolder.AutoGol();

        ball.transform.position = centerFieldPosition.position;

        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = initialPlayerPositions[i].position;
        }
    }


}
