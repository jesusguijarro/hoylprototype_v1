using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [Header("--- Audio Sources ---")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("--- Audio Clips ---")]
    public AudioClip backgroundMusic;
    public AudioClip battleMusic;
    public AudioClip portal;

    private AudioSource battleMusicSource;
    private bool isMusicPaused = false;
    private int activeBattleZones = 0;

    private void Start()
    {
        InitializeMusicSources();
        PlayBackgroundMusic();
    }

    private void InitializeMusicSources()
    {
        if (!musicSource)
            musicSource = gameObject.AddComponent<AudioSource>();
        if (!SFXSource)
            SFXSource = gameObject.AddComponent<AudioSource>();

        musicSource.clip = backgroundMusic;
        musicSource.loop = true;

        battleMusicSource = gameObject.AddComponent<AudioSource>();
        battleMusicSource.clip = battleMusic;
        battleMusicSource.loop = true;
        battleMusicSource.volume = 0f;
    }

    private void Update()
    {
        // Actualizar los volúmenes dinámicamente según los ajustes en VolumeSettings
        musicSource.volume = VolumeSettings.musicVolume * VolumeSettings.masterVolume;
        battleMusicSource.volume = VolumeSettings.musicVolume * VolumeSettings.masterVolume; // Control dinámico
        SFXSource.volume = VolumeSettings.sfxVolume * VolumeSettings.masterVolume;
    }

    public void PlayBackgroundMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    public void PauseBackgroundMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
            isMusicPaused = true;
        }
    }

    public void ResumeBackgroundMusic()
    {
        if (isMusicPaused)
        {
            StartCoroutine(FadeInAudio(musicSource, 1.0f));
            isMusicPaused = false;
        }
    }

    public void EnterBattleZone()
    {
        activeBattleZones++;
        if (activeBattleZones == 1)
        {
            StartCoroutine(SwitchToBattleMusic());
        }
    }

    public void ExitBattleZone()
    {
        activeBattleZones--;
        if (activeBattleZones <= 0)
        {
            StartCoroutine(SwitchToBackgroundMusic());
        }
    }

    private IEnumerator SwitchToBattleMusic()
    {
        Debug.Log("Cambiando a música de batalla.");
        yield return StartCoroutine(FadeOutAudio(musicSource, 1.0f));
        battleMusicSource.volume = 0f;
        battleMusicSource.Play();
        yield return StartCoroutine(FadeInAudio(battleMusicSource, 1.0f));
    }

    private IEnumerator SwitchToBackgroundMusic()
    {
        Debug.Log("Cambiando a música de fondo.");
        yield return StartCoroutine(FadeOutAudio(battleMusicSource, 1.0f));
        battleMusicSource.Stop();
        yield return StartCoroutine(FadeInAudio(musicSource, 1.0f));
    }

    private IEnumerator FadeInAudio(AudioSource source, float duration)
    {
        float startVolume = 0f;
        source.volume = startVolume;
        if (!source.isPlaying) source.Play();

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            source.volume = Mathf.Lerp(startVolume, VolumeSettings.musicVolume * VolumeSettings.masterVolume, t / duration);
            yield return null;
        }

        source.volume = VolumeSettings.musicVolume * VolumeSettings.masterVolume;
    }

    private IEnumerator FadeOutAudio(AudioSource source, float duration)
    {
        float startVolume = source.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            source.volume = Mathf.Lerp(startVolume, 0f, t / duration);
            yield return null;
        }

        source.volume = 0f;
        source.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip)
        {
            SFXSource.PlayOneShot(clip);
        }
    }

    public void PlayPortalSound()
    {
        if (portal)
        {
            Debug.Log("Reproduciendo sonido del portal y saliendo de la zona de batalla.");
            PlaySFX(portal);
            ExitBattleZone(); // Llama a ExitBattleZone al reproducir el sonido del portal
        }
    }
}
