using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int scorePlayer1 = 0;
    public int scorePlayer2 = 0;
    public int winningScore = 3;
    public float matchTimeLimit = 180f;
    private float matchTimer;
    private bool matchEnded = false;

    public TMP_Text scoreTextPlayer1;
    public TMP_Text scoreTextPlayer2;
    public TMP_Text winnerText;
    public TMP_Text timerText;
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
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        matchTimer = matchTimeLimit;
        UpdateScoreUI();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (matchEnded || !isMatchStarted) return;

        matchTimer -= Time.deltaTime;
        timerText.text = Mathf.Ceil(matchTimer).ToString();

        if (matchTimer <= 0)
        {
            EndMatchByTime();
        }
    }

    public void GoalScored(int scorerPlayer)
    {
        if (matchEnded) return;

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
        if (matchEnded) return;

        matchEnded = true;
        Time.timeScale = 0f;
        audioManager.StopMusic();

        if (scorePlayer1 >= winningScore)
            winnerText.text = "¡Jugador 1 ha ganado!";
        else if (scorePlayer2 >= winningScore)
            winnerText.text = "¡Jugador 2 ha ganado!";

        audioSource.PlayOneShot(win);
        finishUI.SetActive(true);
    }

    void EndMatchByTime()
    {
        if (matchEnded) return;

        matchEnded = true;
        Time.timeScale = 0f;
        audioManager.StopMusic();

        if (scorePlayer1 > scorePlayer2)
        {
            winnerText.text = "¡Jugador 1 ha ganado por puntos!";
        }
        else if (scorePlayer2 > scorePlayer1)
        {
            winnerText.text = "¡Jugador 2 ha ganado por puntos!";
        }
        else
        {
            winnerText.text = "¡Empate!";
        }

        audioSource.PlayOneShot(win);
        finishUI.SetActive(true);
    }

    public void StartMatch()
    {
        isMatchStarted = true;
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
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Game")
            SceneManager.LoadScene("Game");
        else if (currentScene == "Game2")
            SceneManager.LoadScene("Game2");

    }
}
