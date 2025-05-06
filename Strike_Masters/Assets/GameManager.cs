using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int scorePlayer1 = 0;
    public int scorePlayer2 = 0;
    public int winningScore = 3;

    public TMP_Text scoreTextPlayer1;
    public TMP_Text scoreTextPlayer2;
    public TMP_Text winnerText;
    public AudioManager audioManager;
    public GameObject startUI;
    public GameObject finishUI;
    public bool isMatchStarted = false;

    public AudioClip win;
    private AudioSource audioSource;

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
        audioSource = gameObject.GetComponent<AudioSource>();
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
        Time.timeScale = 0f;
        audioManager.StopMusic();

        if (scorePlayer1 >= winningScore)
        {
            winnerText.text = "¡Jugador 1 ha ganado!";
        }
        else if (scorePlayer2 >= winningScore)
        {
            winnerText.text = "¡Jugador 2 ha ganado!";
        }
        audioSource.PlayOneShot(win);

        finishUI.SetActive(true);
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

    public void GoMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }    
    
    public void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }
}
