using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Goal1 : MonoBehaviour
{
    public TMP_Text score;
    private int currentScore = 0;

    public GameObject ball;
    public GameObject[] players;
    public Transform[] initialPlayerPositions;
    public Transform centerFieldPosition;
    public BallHolder ballHolder;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            Debug.Log("GOOOOOLLLL");
            currentScore++;
            UpdateScoreText();
            ResetBallAndPlayers();
        }

    }

    private void UpdateScoreText()
    {
        score.text = currentScore.ToString();
    }

    private void ResetBallAndPlayers()
    {
        ball.transform.position = centerFieldPosition.position;
        ballHolder.ResetBallPosition();
        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = initialPlayerPositions[i].position;
        }
    }

}
