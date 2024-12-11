using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [Header("--- Audio Source ---")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("--- Audio Clip ---")]
    public AudioClip background;
    public AudioClip portal;
    public AudioClip battleMusic;  // M�sica de pelea

    private AudioSource tempSource;
    private bool isMusicPaused = false;

    private AudioSource battleMusicSource; // Fuente de audio para m�sica de pelea
    private int activeBattleZones = 0; // Contador de zonas de combate activas

    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();

        // Configurar la fuente de m�sica de pelea
        battleMusicSource = gameObject.AddComponent<AudioSource>();
        battleMusicSource.clip = battleMusic;
        battleMusicSource.loop = true;
        battleMusicSource.volume = 0f; // Inicialmente el volumen es 0
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

    // M�todo para manejar la m�sica de pelea
    public void EnterBattleZone()
    {
        activeBattleZones++;
        if (activeBattleZones > 0)
        {
            // Pausar la m�sica de fondo y activar la m�sica de pelea
            StartCoroutine(HandleBattleMusic(true)); // Activar m�sica de batalla
        }
    }

    public void ExitBattleZone()
    {
        activeBattleZones--;
        if (activeBattleZones == 0)
        {
            // Detener la m�sica de pelea y reactivar la m�sica de fondo
            StartCoroutine(HandleBattleMusic(false)); // Desactivar m�sica de batalla
        }
    }

    // M�todo para manejar la transici�n entre la m�sica de fondo y la m�sica de batalla
    private IEnumerator HandleBattleMusic(bool isEnteringBattle)
    {
        if (isEnteringBattle)
        {
            // Fade out la m�sica de fondo
            yield return StartCoroutine(FadeOutBackgroundMusic(1.0f));

            // Fade in la m�sica de batalla
            battleMusicSource.volume = 0f;
            battleMusicSource.Play();
            yield return StartCoroutine(FadeInBattleMusic(1.0f));
        }
        else
        {
            // Fade out la m�sica de batalla
            yield return StartCoroutine(FadeOutBattleMusic(1.0f));

            // Fade in la m�sica de fondo
            yield return StartCoroutine(FadeInBackgroundMusic(1.0f));
        }
    }

    // Fade In para la m�sica de batalla
    private IEnumerator FadeInBattleMusic(float duration)
    {
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            battleMusicSource.volume = Mathf.Lerp(0f, 1.0f, t / duration);
            yield return null;
        }
        battleMusicSource.volume = 1.0f;
    }

    // Fade Out para la m�sica de batalla
    private IEnumerator FadeOutBattleMusic(float duration)
    {
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            battleMusicSource.volume = Mathf.Lerp(1.0f, 0f, t / duration);
            yield return null;
        }

        battleMusicSource.volume = 0f;
        battleMusicSource.Stop();
    }

    // Fade In para la m�sica de fondo al salir de la zona de batalla
    private IEnumerator FadeInBackgroundMusic(float duration)
    {
        musicSource.volume = 0f;
        musicSource.Play();

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(0f, 1.0f, t / duration);
            yield return null;
        }

        musicSource.volume = 1.0f;
    }

    // Fade Out para la m�sica de fondo al entrar en la zona de batalla
    private IEnumerator FadeOutBackgroundMusic(float duration)
    {
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(1.0f, 0f, t / duration);
            yield return null;
        }

        musicSource.volume = 0f;
        musicSource.Stop();
    }
}
