using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        musicSource.volume = musicSlider.value;
        sfxSource.volume = sfxSlider.value;

        musicSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });
        sfxSlider.onValueChanged.AddListener(delegate { OnSFXVolumeChange(); });
    }

    public void OnMusicVolumeChange()
    {
        musicSource.volume = musicSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void OnSFXVolumeChange()
    {
        sfxSource.volume = sfxSlider.value;
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }
}
