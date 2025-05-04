using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public AudioClip buttonSound;
    public AudioSource audioSource;
    public GameObject playUI;
    public GameObject menuUI;
    public GameObject optionsUI;
    public GameObject creditsUI;

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        Debug.Log("Bot�n Exit pulsado");
        audioSource.PlayOneShot(buttonSound);
        Application.Quit();

    }

    public void GoPlayMenu()
    {
        if (audioSource != null && buttonSound != null)
        {
            Debug.Log("Bot�n Play pulsado");
            audioSource.PlayOneShot(buttonSound);
            menuUI.SetActive(false);
            playUI.SetActive(true);
        }
    }
    public void GoOptionsMenu()
    {
        if (audioSource != null && buttonSound != null)
        {
            Debug.Log("Bot�n Options pulsado");
            audioSource.PlayOneShot(buttonSound);
            menuUI.SetActive(false);
            optionsUI.SetActive(true);
        }
    }    
    
    public void GoMenu()
    {
        if (audioSource != null && buttonSound != null)
        {
            Debug.Log("Bot�n Menu pulsado");
            audioSource.PlayOneShot(buttonSound);
            menuUI.SetActive(true);
            optionsUI.SetActive(false);
            playUI.SetActive(false);
            creditsUI.SetActive(false);
        }
    }    
    
    public void GoCreditsMenu()
    {
        if (audioSource != null && buttonSound != null)
        {
            Debug.Log("Bot�n Credits pulsado");
            audioSource.PlayOneShot(buttonSound);
            menuUI.SetActive(false);
            creditsUI.SetActive(true);
        }
    }

}