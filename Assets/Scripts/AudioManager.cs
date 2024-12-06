using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--- Audio Source ---")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("--- Audio Clip ---")]
    public AudioClip background;
    public AudioClip portal;

    private AudioSource tempSource; // AudioSource temporal
    private bool isMusicPaused = false; // Bandera para saber si la música está pausada

    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlayTemporaryMusic(AudioClip tempClip)
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
            isMusicPaused = true;
        }

        GameObject tempObject = new GameObject("TempAudioSource");
        tempSource = tempObject.AddComponent<AudioSource>();
        tempSource.clip = tempClip;
        tempSource.Play();

        StartCoroutine(WaitForTempMusicToEnd(tempObject));
    }

    private IEnumerator WaitForTempMusicToEnd(GameObject tempObject)
    {
        while (tempSource.isPlaying)
        {
            yield return null;
        }

        Destroy(tempObject);

        if (isMusicPaused)
        {
            ResumeMusic();
        }
    }

    public void PauseMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
            isMusicPaused = true;
        }
    }

    public void ResumeMusic()
    {
        if (isMusicPaused)
        {
            StartCoroutine(FadeInAudio(musicSource, 1.0f));
            isMusicPaused = false;
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    private IEnumerator FadeInAudio(AudioSource source, float duration)
    {
        float startVolume = 0f;
        source.volume = startVolume;
        source.Play();

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            source.volume = Mathf.Lerp(startVolume, 1.0f, t / duration);
            yield return null;
        }

        source.volume = 1.0f;
    }
}
