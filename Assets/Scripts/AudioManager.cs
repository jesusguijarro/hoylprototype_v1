using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("--- Audio Sources ---")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("--- Audio Clips ---")]
    public AudioClip backgroundMusic;
    public AudioClip battleMusic;
    public AudioClip portal;

    private AudioSource battleMusicSource;
    public bool isBattleMusicPlaying = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    private void Start()
    {
        InitializeMusicSources();
        PlayBackgroundMusic();
    }

    private void Update()
    {
        UpdateAudioVolumes();
    }

    private void InitializeMusicSources()
    {
        musicSource ??= gameObject.AddComponent<AudioSource>();
        SFXSource ??= gameObject.AddComponent<AudioSource>();

        musicSource.clip = backgroundMusic;
        musicSource.loop = true;

        battleMusicSource = gameObject.AddComponent<AudioSource>();
        battleMusicSource.clip = battleMusic;
        battleMusicSource.loop = true;
        battleMusicSource.volume = 0f;
    }

    private void UpdateAudioVolumes()
    {
        musicSource.volume = VolumeSettings.musicVolume * VolumeSettings.masterVolume;
        battleMusicSource.volume = VolumeSettings.musicVolume * VolumeSettings.masterVolume;
        SFXSource.volume = VolumeSettings.sfxVolume * VolumeSettings.masterVolume;
    }

    public void PlayBackgroundMusic()
    {
        if (!musicSource.isPlaying)
            musicSource.Play();
    }

    public IEnumerator SwitchToBattleMusic()
    {
        if (isBattleMusicPlaying) yield break;

        Debug.Log("Switching to battle music.");
        yield return StartCoroutine(FadeOutAudio(musicSource, 1.0f));

        battleMusicSource.volume = 0f;
        battleMusicSource.Play();

        yield return StartCoroutine(FadeInAudio(battleMusicSource, 1.0f));
        isBattleMusicPlaying = true;
    }

    public IEnumerator SwitchToBackgroundMusic()
    {
        if (isBattleMusicPlaying)
        {
            Debug.Log("Switching back to background music.");
            yield return StartCoroutine(FadeOutAudio(battleMusicSource, 1.0f));
            battleMusicSource.Stop();
        }

        musicSource.Stop();
        musicSource.volume = 0f;
        musicSource.Play();

        Debug.Log("Playing background music.");
        yield return StartCoroutine(FadeInAudio(musicSource, 1.0f));
        isBattleMusicPlaying = false;
    }

    public IEnumerator FadeInAudio(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;
        audioSource.volume = 0;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, startVolume, t / duration);
            yield return null;
        }

        audioSource.volume = startVolume;
    }

    public IEnumerator FadeOutAudio(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / duration);
            yield return null;
        }

        audioSource.volume = 0;
        audioSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip)
            SFXSource.PlayOneShot(clip);
    }

    public void PlayPortalSound()
    {
        if (portal)
        {
            Debug.Log("Playing portal sound and exiting battle zone.");
            PlaySFX(portal);
        }
    }
}
