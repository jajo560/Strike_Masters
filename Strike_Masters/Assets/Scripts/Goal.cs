using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public TMP_Text score;
    private int currentScore = 0;

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

        }

    }

    private void UpdateScoreText()
    {
        score.text = currentScore.ToString();
    }

}
