using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;

    public static float musicVolume = 1.0f; // Volumen de música
    public static float sfxVolume = 1.0f; // Volumen de efectos de sonido
    public static float masterVolume = 1.0f; // Volumen maestro

    public void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    public void SetMusicVolume()
    {
        musicVolume = musicSlider.value;
        //myMixer.SetFloat("Music", Mathf.Log10(musicVolume) * 20);  // Ajusta la música de fondo y de batalla
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
    }

    public void SetSFXVolume()
    {
        sfxVolume = SFXSlider.value;
        //myMixer.SetFloat("SFX", Mathf.Log10(sfxVolume) * 20);  // Ajusta los efectos de sonido
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        SetMusicVolume();
        SetSFXVolume();
    }
}
