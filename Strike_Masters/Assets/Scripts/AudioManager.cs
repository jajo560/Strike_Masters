using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip normalMusic;
    public AudioClip intenseMusic;

    private bool hasSwitched = false;

    public void CheckMusic(int score1, int score2, int winningScore)
    {
        if (!hasSwitched && (score1 == winningScore - 1 || score2 == winningScore - 1))
        {
            audioSource.Stop();
            audioSource.clip = intenseMusic;
            audioSource.Play();
            hasSwitched = true;
        }
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

}
