using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int scorePlayer1 = 0;
    public int scorePlayer2 = 0;
    public int winningScore = 3;

    public TMP_Text scoreTextPlayer1;
    public TMP_Text scoreTextPlayer2;

    public AudioManager audioManager;
    public GameObject startUI;
    public bool isMatchStarted = false;

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        UpdateScoreUI();
    }

    public void GoalScored(int scorerPlayer)
    {
        if (scorerPlayer == 1)
            scorePlayer1++;
        else
            scorePlayer2++;

        UpdateScoreUI();
        audioManager.CheckMusic(scorePlayer1, scorePlayer2, winningScore);

        if (scorePlayer1 >= winningScore || scorePlayer2 >= winningScore)
        {
            EndMatch();
        }
    }

    void UpdateScoreUI()
    {
        scoreTextPlayer1.text = scorePlayer1.ToString();
        scoreTextPlayer2.text = scorePlayer2.ToString();
    }

    void EndMatch()
    {
        Debug.Log("¡Partido terminado!");
        // Aquí pausar el juego, mostrar una pantalla final, etc.
    }

    public void StartMatch()
    {
        StartCoroutine(ShowStartUI());
    }

    IEnumerator ShowStartUI()
    {
        startUI.SetActive(true);
        yield return new WaitForSeconds(3f);
        startUI.SetActive(false);
    }

}
