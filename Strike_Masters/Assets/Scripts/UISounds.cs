using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonSoundAndSceneChange : MonoBehaviour
{
    public AudioClip buttonSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundAndChangeScene(string sceneName)
    {
        if (audioSource != null && buttonSound != null)
        {
            audioSource.PlayOneShot(buttonSound);
            StartCoroutine(LoadSceneAfterSound(sceneName));
        }
    }

    private IEnumerator LoadSceneAfterSound(string sceneName)
    {
        yield return new WaitForSeconds(buttonSound.length);
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        audioSource.PlayOneShot(buttonSound);
        Application.Quit();

    }

}
