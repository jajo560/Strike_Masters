using TMPro;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static TargetManager instance;

    public int player1Score = 0;
    public int player2Score = 0;
    public int targetScore = 10;
    public float timeLimit = 180f;

    public TMP_Text player1ScoreText;
    public TMP_Text player2ScoreText;

    private bool levelCompleted = false;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (levelCompleted) return;
    }

    public void AddScore(bool isPlayer2, int amount)
    {
        if (levelCompleted) return;

        if (isPlayer2)
        {
            player2Score += amount;
        }
        else
        {
            player1Score += amount;
        }

        player1ScoreText.text = player1Score.ToString();
        player2ScoreText.text = player2Score.ToString();

        if (player1Score >= targetScore || player2Score >= targetScore)
        {
            CompleteLevel();
        }
    }

    void CompleteLevel()
    {
        levelCompleted = true;

        if (player1Score > player2Score)
            GameManager.Instance.GoalScored(1);
        else if (player2Score > player1Score)
            GameManager.Instance.GoalScored(2);
        else
            Debug.Log("Empate en el minijuego, no se otorga punto.");
    }
}
